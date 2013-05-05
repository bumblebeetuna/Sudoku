using System;
using Autofac;
using Moq;
using NUnit.Framework;

namespace Sudoku.Application.Tests
{
    [TestFixture]
    public class DomainFixture
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotExecute_Command()
        {
            //arrange
            var builder = new ContainerBuilder();
            var container = builder.Build();

            var domain = new Domain(container, new Mock<IDataSource>().Object);

            //act
            domain.Execute(new CommandStub());

            //assert
            Assert.Fail();
        }

        [Test]
        public void CanExecute_Command()
        {
            //arrange
            var commandHandlerMock = new Mock<CommandHandler<CommandStub>>();
            commandHandlerMock.Setup(x => x.Execute(It.IsAny<CommandStub>())).Verifiable();

            var builder = new ContainerBuilder();
            builder.RegisterInstance(commandHandlerMock.Object).AsImplementedInterfaces();

            var container = builder.Build();

            var domain = new Domain(container, new Mock<IDataSource>().Object);

            //act
            domain.Execute(new CommandStub());

            //assert
            commandHandlerMock.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotExecute_Query()
        {
            //arrange
            var builder = new ContainerBuilder();
            var container = builder.Build();

            var domain = new Domain(container, new Mock<IDataSource>().Object);

            //act
            domain.Execute(new QueryStub());

            //assert
            Assert.Fail();
        }

        [Test]
        public void CanExecute_Query()
        {
            //arrange
            var queryHandlerMock = new Mock<QueryHandler<QueryStub, object>>();
            queryHandlerMock.Setup(x => x.Execute(It.IsAny<QueryStub>())).Verifiable();

            var builder = new ContainerBuilder();
            builder.RegisterInstance(queryHandlerMock.Object).AsImplementedInterfaces();

            var container = builder.Build();

            var domain = new Domain(container, new Mock<IDataSource>().Object);

            //act
            domain.Execute(new QueryStub());

            //assert
            queryHandlerMock.VerifyAll();
        }

        public class CommandStub : ICommand { }
        public class QueryStub : IQuery<object> { }
    }
}
