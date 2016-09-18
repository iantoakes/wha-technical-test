using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Autofac.Integration.WebApi;
using NLog;

namespace RiskManager.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private ILogger _logger;
        protected void Application_Start()
        {
            _logger = LogManager.GetLogger("RiskManager");

            _logger.Info(() => "================ RiskManager starting ================ ");

            GlobalConfiguration.Configure(WebApiConfig.Register);


            var builder = new ContainerBuilder();
            builder.RegisterInstance(_logger).As<ILogger>().SingleInstance();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            RegisterAutofacModules(builder);

            var container = builder.Build();

            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            config.Services.Add(typeof (IExceptionLogger),
                new NlogExceptionLogger(new TraceSource("RiskManagerTraceSource", SourceLevels.All), _logger));
        }

        private void RegisterAutofacModules(ContainerBuilder builder)
        {
            _logger.Info(() => "Registering Autofac modules in referenced application assemblies");

            var executingUri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string executingPath = Path.GetDirectoryName(executingUri.LocalPath);

            var assemblies = new List<Assembly> {Assembly.GetExecutingAssembly()};
            var enumerateFiles = Directory.EnumerateFiles(executingPath, "*.dll", SearchOption.AllDirectories).ToList();
            assemblies.AddRange(
                enumerateFiles
                    .Where(filename => Regex.IsMatch(filename, @"RiskManager\.[A-Za-z]+\.dll"))
                    .Select(Assembly.LoadFrom)
                );

            _logger.Info(() => $"{assemblies.Count} application assemblies found");
            assemblies.ForEach(a => builder.RegisterAssemblyModules(a));
        }
    }
}
