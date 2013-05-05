using System;
using System.Collections.Generic;
using Sudoku.Domain;

namespace Sudoku.Application
{
    /// <summary>
    /// 
    /// </summary>
    public static class GameExtensions
    {
        /// <summary>
        /// Finds the games.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns></returns>
        public static IEnumerable<GameInfo> FindGames(this IDomain domain)
        {
            var result = domain.Execute(new FindGames());

            return result ?? new GameInfo[0];
        }

        /// <summary>
        /// Loads the game.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        /// <exception cref="Sudoku.Application.EntityNotFoundException"></exception>
        public static Game LoadGame(this IDomain domain, Guid id)
        {
            var result = domain.Execute(new LoadGame(id));

            if (result == null)
            {
                throw new EntityNotFoundException();
            }

            return result;
        }
    }
}
