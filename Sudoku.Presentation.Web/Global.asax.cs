using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using Sudoku.Application;

namespace Sudoku.Presentation.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = BuildContainer();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private ILifetimeScope BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(typeof(IDomain).Assembly);

            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            return builder.Build();
        }
    }
}