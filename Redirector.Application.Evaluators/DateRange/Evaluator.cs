using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Redirector.Application.Interfaces;
using System.Data;

namespace Redirector.Application.Evaluators
{
    public class DateRangeEvaluator : IRedirectRulesEvaluator
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly string _field = "dateRange";
        private readonly ILogger<DateRangeEvaluator> _logger;
        public DateRangeEvaluator(IHttpContextAccessor accessor, ILogger<DateRangeEvaluator> logger)
        {
            _accessor = accessor;
            _logger = logger;
        }

        public bool Evaluate(IDictionary<string, object> args)
        {
            if (!args.TryGetValue(_field, out var value))
                return false;
            
            var nowUtc = GetDatetimeUTC();
            var dateRange = JsonConvert.DeserializeObject<DateRange>(value.ToString());

            if (dateRange.Begin > nowUtc || dateRange.End < nowUtc)
                return false;

            _logger.LogInformation(@$"Evaluated rule: ""{_field}"", value ""{value}""");
            return true;
        }

        private DateTime GetDatetimeUTC() => DateTime.UtcNow;
    }

    [Serializable]
    internal class DateRange
    {
        [JsonProperty("begin")]
        public DateTime? Begin { get; set; }

        [JsonProperty("end")]
        public DateTime? End { get; set; }
    }
}
