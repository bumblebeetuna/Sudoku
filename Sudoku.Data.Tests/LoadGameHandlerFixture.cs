using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Sudoku.Application;

namespace Sudoku.Data.Tests
{
    [TestFixture]
    public class LoadGameHandlerFixture
    {
        [Test]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void CannotExecute_NotFound()
        {
            //arrange
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(x => x.Query<GameRecord>()).Returns(new GameRecord[0].AsQueryable());

            var query = new LoadGame(Guid.NewGuid());
            var handler = new LoadGameHandler(repoMock.Object);

            //act
            var result = handler.Execute(query);

            //assert
            Assert.Fail();
        }

        [Test]
        public void CanExecute()
        {
            //arrange
            var gameRecord = new GameRecord
            {
                Id = Guid.NewGuid(),
                Moves = new List<GameMoveRecord>(),
            };

            var repoMock = new Mock<IRepository>();
            repoMock.Setup(x => x.Query<GameRecord>()).Returns(new[] { gameRecord }.AsQueryable());

            var query = new LoadGame(gameRecord.Id);
            var handler = new LoadGameHandler(repoMock.Object);

            //act
            var result = handler.Execute(query);

            //assert
            Assert.IsNotNull(result);
        }
    }
}
