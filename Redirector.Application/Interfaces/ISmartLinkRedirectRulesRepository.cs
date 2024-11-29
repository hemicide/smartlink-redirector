using Redirector.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redirector.Application.Interfaces
{
    public interface ISmartLinkRedirectRulesRepository
    {
        public Task<IEnumerable<RedirectRule>> GetRules();
    }
}
