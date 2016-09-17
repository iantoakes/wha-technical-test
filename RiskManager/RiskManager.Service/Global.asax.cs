using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

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

            RegisterAutofacModules(builder);

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterAutofacModules(ContainerBuilder builder)
        {
            var executingUri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string executingPath = Path.GetDirectoryName(executingUri.LocalPath);

            var assemblies = new List<Assembly> {Assembly.GetExecutingAssembly()};
            var enumerateFiles = Directory.EnumerateFiles(executingPath, "*.dll", SearchOption.AllDirectories).ToList();
            assemblies.AddRange(
                enumerateFiles
                    .Where(filename => Regex.IsMatch(filename, @"RiskManager\.[A-Za-z]+\.dll"))
                    .Select(Assembly.LoadFrom)
                );

            assemblies.ForEach(a => builder.RegisterAssemblyModules(a));
        }
    }
}
