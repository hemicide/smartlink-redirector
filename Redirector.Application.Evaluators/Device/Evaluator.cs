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
    public class DeviceEvaluator : IRedirectRulesEvaluator
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly Parser _uaParser;
        private readonly string _field = "device";
        private readonly ILogger<DeviceEvaluator> _logger;

        public DeviceEvaluator(IHttpContextAccessor accessor, ILogger<DeviceEvaluator> logger)
        {
            _accessor = accessor;
            _uaParser = Parser.GetDefault();
            _logger = logger;
        }

        public bool Evaluate(IDictionary<string, object> args)
        {
            var userAgent = _accessor.HttpContext?.Request?.Headers["User-Agent"] ?? default;
            var clientInfo = _uaParser.Parse(userAgent);
            var device = clientInfo?.OS.Family.ToLower();

            if (!args.TryGetValue(_field, out var value))
                return false;

            if (device != value.ToString()?.ToLower())
                return false;

            _logger.LogInformation(@$"Evaluated rule: ""{_field}"", value ""{value}""");
            return true;
        }

        private StringValues GetHeaderValues(string headerName) => _accessor.HttpContext?.Request?.Headers[headerName] ?? default;
    }
}
