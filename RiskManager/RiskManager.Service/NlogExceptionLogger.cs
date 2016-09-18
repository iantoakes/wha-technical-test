using System.Diagnostics;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace RiskManager.Service
{
    public class NlogExceptionLogger : ExceptionLogger
    {
        private readonly TraceSource _traceSource;
        private readonly ILogger _logger;

        public NlogExceptionLogger(TraceSource traceSource, ILogger logger)
        {
            _traceSource = traceSource;
            _logger = logger;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            _logger.Error(context.Exception, context.Exception.Message);
            base.Log(context);
        }
    }
}