using Redirector.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redirector.Application.Services
{
    public class SmartLinkRedirectService : ISmartLinkRedirectService
    {
        private readonly ISmartLinkRedirectRulesRepository _repository;
        private readonly IEnumerable<IRedirectRulesEvaluator> _evaluators;

        public SmartLinkRedirectService(ISmartLinkRedirectRulesRepository repository, IEnumerable<IRedirectRulesEvaluator> evaluators) 
        {
            _repository = repository;
            _evaluators = evaluators;
        }

        public async Task<string> Evaluate()
        {
            var redirectRules = await _repository.GetRules();
            if (redirectRules == null)
                return null;

            foreach (var redirecRule in redirectRules)
                if (_evaluators.Any(e => e.Evaluate(redirecRule.Args)))
                    return redirecRule.RedirectTo;

            return null;
        }
    }
}
