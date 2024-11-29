using Newtonsoft.Json;
using Redirector.Application.Interfaces;
using Redirector.Application.DTO;
using Microsoft.Extensions.Logging;

namespace Redirector.Application.Services
{
    public class SmartLinkRedirectRulesRepository(IGetSmartLinksRepository repository, ILogger<SmartLinkRedirectRulesRepository> logger) : ISmartLinkRedirectRulesRepository
    {
        public async Task<IEnumerable<RedirectRule>> GetRules()
        {
            var smartlinks = await repository.GetSmartLink();
            if (smartlinks == null)
                return null;

            logger.LogInformation($"Smartlink found: {smartlinks.Link}");
            return JsonConvert.DeserializeObject<IEnumerable<RedirectRule>>(smartlinks.Rules);
        }
    }
}
