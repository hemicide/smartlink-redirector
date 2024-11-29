using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redirector.Application.Interfaces
{
    public interface IRedirectRulesEvaluator
    {
        public bool Evaluate(IDictionary<string, object> args);
    }
}
