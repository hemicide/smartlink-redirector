using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Redirector.Application.Interfaces;
using System.Xml.Linq;
using UAParser;

namespace Redirector.Application.Evaluators
{
    public class LanguageEvaluator : IRedirectRulesEvaluator
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly Parser _uaParser;
        private readonly string _field = "language";
        private readonly ILogger<LanguageEvaluator> _logger;

        public LanguageEvaluator(IHttpContextAccessor accessor, ILogger<LanguageEvaluator> logger)
        {
            _accessor = accessor;
            _uaParser = Parser.GetDefault();
            _logger = logger;
        }

        public bool Evaluate(IDictionary<string, object> args)
        {
            var acceptLanguage = _accessor.HttpContext?.Request?.Headers["Accept-Language"] ?? default;

            if (!args.TryGetValue(_field, out var value))
                return false;

            if (!acceptLanguage.ToString().Contains(value!.ToString()!.ToLower()))
                return false;

            _logger.LogInformation(@$"Evaluated rule: ""{_field}"", value ""{value}""");
            return true;
        }

        private StringValues GetHeaderValues(string headerName) => _accessor.HttpContext?.Request?.Headers[headerName] ?? default;
    }
}
