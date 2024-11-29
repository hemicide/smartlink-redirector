using Microsoft.AspNetCore.Http;
using Redirector.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redirector.Application.Services
{
    public class SmartLink(IHttpContextAccessor accessor) : ISmartLink
    {
        public string GetLink() => accessor.HttpContext!.Request.Path.Value?.TrimStart('/');
    }
}
