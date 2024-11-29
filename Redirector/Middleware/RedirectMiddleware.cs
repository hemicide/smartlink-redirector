using Microsoft.AspNetCore.Http;
using Redirector.Application.Interfaces;
using Redirector.Application.Services;
using System.Net;
using System.Net.Http;

namespace Redirector.Middleware
{
    public class RedirectMiddleware : IMiddleware
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RequestDelegate _next;
        private readonly ISmartLinkRedirectService _smartLinkRedirectService;
        private readonly ILogger _logger;

        public RedirectMiddleware(ISmartLinkRedirectService smartLinkRedirectService, ILogger<RedirectMiddleware> logger)
        {
            _smartLinkRedirectService = smartLinkRedirectService;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        { 
            var path = context.Request.Path.Value?.TrimStart('/');

            _logger.LogInformation(@$"Get link ""{path}""");
            var host = await _smartLinkRedirectService.Evaluate();
            if (host != null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TemporaryRedirect;
                context.Response.Redirect(host);
                _logger.LogInformation(@$"Redirect to ""{host}""");
            } else {
                await next(context);
            }
        }
    }
}
