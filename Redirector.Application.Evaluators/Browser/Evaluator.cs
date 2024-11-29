using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Redirector.Application.Interfaces;
using UAParser;

namespace Redirector.Application.Evaluators
{
    public class BrowserEvaluator : IRedirectRulesEvaluator
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly Parser _uaParser;
        private readonly string _field = "browser";
        private readonly ILogger<BrowserEvaluator> _logger;

        public BrowserEvaluator(IHttpContextAccessor accessor, ILogger<BrowserEvaluator> logger)
        {
            _accessor = accessor;
            _uaParser = Parser.GetDefault();
            _logger = logger;
        }

        public bool Evaluate(IDictionary<string, object> args)
        {
            var userAgent = _accessor.HttpContext?.Request?.Headers["User-Agent"] ?? default;
            var clientInfo = _uaParser.Parse(userAgent);
            var browser = clientInfo?.UA.Family.ToLower();

            if (!args.TryGetValue(_field, out var value))
                return false;

            if (browser != value.ToString()?.ToLower())
                return false;

            _logger.LogInformation(@$"Evaluated rule: ""{_field}"", value ""{value}""");
            return true;
        }

        private StringValues GetHeaderValues(string headerName) => _accessor.HttpContext?.Request?.Headers[headerName] ?? default;
    }
}
