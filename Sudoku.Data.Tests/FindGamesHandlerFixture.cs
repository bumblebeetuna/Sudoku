using System.Linq;
using Moq;
using NUnit.Framework;
using Sudoku.Application;

namespace Sudoku.Data.Tests
{
    [TestFixture]
    public class FindGamesHandlerFixture
    {
        [Test]
        public void CanExecute()
        {
            //arrange
            var gameRecords = new[] { 
                new GameRecord
                {
                    Moves = new[] { new GameMoveRecord{ Value = 0 } },
                },
                new GameRecord
                {
                    Moves = new[] { new GameMoveRecord() },
                },
            };

            var repoMock = new Mock<IRepository>();
            repoMock.Setup(x => x.Query<GameRecord>()).Returns(gameRecords.AsQueryable());

            var query = new FindGames();
            var handler = new FindGamesHandler(repoMock.Object);

            //act
            var result = handler.Execute(query);

            //assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(100, result.First().PercentageComplete);
            Assert.AreEqual(0, result.Last().PercentageComplete);
        }
    }
}
