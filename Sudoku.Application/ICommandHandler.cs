using System.Diagnostics.Contracts;

namespace Sudoku.Application
{
    /// <summary>
    /// Interface for defining the execution logic of a command
    /// </summary>
    [ContractClass(typeof(CommandHandlerContract))]
    public interface ICommandHandler
    {
        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        void Execute(ICommand command);
    }

    /// <summary>
    /// Interface for defining the execution logic of a specific type of command
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to execute.</typeparam>
    [ContractClass(typeof(CommandHandlerContract<>))]
    public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : ICommand
    {
        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        void Execute(TCommand command);
    }

    /// <summary>
    /// Base class for defining the execution logic of a specific command
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : class,ICommand
    {
        #region ICommandHandler<TCommand> Members

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public abstract void Execute(TCommand command);

        #endregion

        #region ICommandHandler Members

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        public void Execute(ICommand command)
        {
            var tCommand = command as TCommand;

            Contract.Assume(tCommand != null);

            Execute(tCommand);
        }

        #endregion
    }

    #region Contracts

    [ContractClassFor(typeof(ICommandHandler))]
    abstract class CommandHandlerContract : ICommandHandler
    {
        #region ICommandHandler Members

        public void Execute(ICommand command)
        {
            Contract.Requires(command != null);
        }

        #endregion
    }

    [ContractClassFor(typeof(ICommandHandler<>))]
    abstract class CommandHandlerContract<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        #region ICommandHandler<TCommand> Members

        public void Execute(TCommand command)
        {
            Contract.Requires(command != null);
        }

        #endregion

        #region ICommandHandler Members

        public void Execute(ICommand command)
        {
        }

        #endregion
    }

    #endregion
}
