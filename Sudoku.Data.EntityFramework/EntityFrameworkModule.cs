using Autofac;

namespace Sudoku.Data.EntityFramework
{
    public class EntityFrameworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(EntityFrameworkModule).Assembly).AsImplementedInterfaces();
            builder.RegisterType<SudokuDbContext>();
        }
    }
}
