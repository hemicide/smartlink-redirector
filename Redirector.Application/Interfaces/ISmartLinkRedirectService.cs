﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redirector.Application.Interfaces
{
    public interface ISmartLinkRedirectService
    {
        Task<string> Evaluate();
    }
}
