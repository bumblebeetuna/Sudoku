using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Sudoku.Application;

namespace Sudoku.Data.Tests
{
    [TestFixture]
    public class CreateGameHandlerFixture
    {
        [Test]
        [ExpectedException(typeof(DuplicateEntityException))]
        public void CannotExecute_AlreadyExists()
        {
            //arrange
            var gameRecord = new GameRecord
            {
                Id = Guid.NewGuid(),
            };

            var repoMock = new Mock<IRepository>();
            repoMock.Setup(x => x.Query<GameRecord>()).Returns(new[] { gameRecord }.AsQueryable());

            var command = new CreateGame(gameRecord.Id);
            var handler = new CreateGameHandler(repoMock.Object);

            //act
            handler.Execute(command);

            //assert
            Assert.Fail();
        }

        [Test]
        public void CanExecute()
        {
            //arrange
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(x => x.Query<GameRecord>()).Returns(new GameRecord[0].AsQueryable());
            repoMock.Setup(x => x.Add(It.IsAny<GameRecord>())).Callback((object x) =>
            {
                var addItem = x as GameRecord;

                Assert.IsNotNull(addItem);
                Assert.AreEqual(9 * 9, addItem.Moves.Count());
            });

            var command = new CreateGame(Guid.NewGuid());
            var handler = new CreateGameHandler(repoMock.Object);

            //act
            handler.Execute(command);

            //assert
            repoMock.VerifyAll();
        }
    }
}
