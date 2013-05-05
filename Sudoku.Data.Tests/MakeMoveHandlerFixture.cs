using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Sudoku.Application;

namespace Sudoku.Data.Tests
{
    [TestFixture]
    public class MakeMoveHandlerFixture
    {
        [Test]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void CannotExecute_NotFound()
        {
            //arrange
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(x => x.Query<GameRecord>()).Returns(new GameRecord[0].AsQueryable());

            var command = new MakeMove(Guid.NewGuid(), 0, 0, 0);
            var handler = new MakeMoveHandler(repoMock.Object);

            //act
            handler.Execute(command);

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
                Moves = new[]{
                    new GameMoveRecord
                    { 
                        X = 0,
                        Y = 0
                    }
                },
            };

            var repoMock = new Mock<IRepository>();
            repoMock.Setup(x => x.Query<GameRecord>()).Returns(new[] { gameRecord }.AsQueryable());
            repoMock.Setup(x => x.Update(It.IsAny<GameMoveRecord>())).Callback((object y) =>
            {
                var updateItem = y as GameMoveRecord;

                Assert.IsNotNull(updateItem);
                Assert.AreEqual(0, updateItem.Value);
            })
            .Verifiable();

            var command = new MakeMove(gameRecord.Id, 0, 0, 0);
            var handler = new MakeMoveHandler(repoMock.Object);

            //act
            handler.Execute(command);

            //assert
            repoMock.VerifyAll();
        }
    }
}
