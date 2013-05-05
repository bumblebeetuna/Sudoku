using Autofac;

namespace Sudoku.Data
{
    public sealed class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(DataModule).Assembly).AsImplementedInterfaces();
        }
    }
}
