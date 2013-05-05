using Autofac;

namespace Sudoku.Application
{
    /// <summary>
    /// Autofac module for the Bookkeepr.Application assembly
    /// </summary>
    public sealed class ApplicationModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ApplicationModule).Assembly).AsImplementedInterfaces();
            builder.RegisterType<Domain>();
        }
    }
}
