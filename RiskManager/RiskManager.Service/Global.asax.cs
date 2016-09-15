using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using RiskManager.DomainLogic;
using RiskManager.Repository;

namespace RiskManager.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<SettledBetRepository>().As<ISettledBetRepository>();
            builder.RegisterType<CustomerRiskService>().AsSelf();
            builder.RegisterType<SettledBetService>().AsSelf();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
