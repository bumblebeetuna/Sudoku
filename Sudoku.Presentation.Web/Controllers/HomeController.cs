using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using Sudoku.Application;

namespace Sudoku.Presentation.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDomain domain;

        public HomeController(IDataSource dataSource, IDomainFactory domainFactory)
        {
            Contract.Requires(dataSource != null);
            Contract.Requires(domainFactory != null);

            domain = domainFactory.Create(dataSource);
        }

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(domain != null);
        }

        [ImportModelStateFromTempData]
        public ActionResult Index()
        {
            var games = domain.FindGames();

            return View(games);
        }

        [ExportModelStateToTempData]
        public ActionResult Play(Guid id)
        {
            try
            {
                var game = domain.LoadGame(id);

                return View(game);
            }
            catch (EntityNotFoundException)
            {
                ModelState.AddModelError("", "Game could not be found");

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult NewGame()
        {
            var gameId = Guid.NewGuid();

            domain.Execute(new CreateGame(gameId));

            return RedirectToAction("Play", new { id = gameId });
        }

        public ActionResult Moves(Guid gameId, Guid moveId)
        {
            var game = domain.LoadGame(gameId);
            var move = game.Moves.First(x => x.Id == moveId);
            var possibleMoves = move.PossibleValues(game).ToList();
            var moves = Enumerable.Range(1, 9)
                .Select(x => new { Value = x, Enabled = x == move.Value || possibleMoves.Contains(x) })
                .ToList();

            return Json(moves, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MakeMove(Guid gameId, Guid moveId, int value)
        {
            var game = domain.LoadGame(gameId);
            var move = game.Moves.First(x => x.Id == moveId);

            domain.Execute(new MakeMove(gameId, move.X, move.Y, value));

            return RedirectToAction("Play", new { id = gameId });
        }
    }
}
