using Redirector.Application.Interfaces;
using System.Reflection;

namespace Redirector.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddEvaluatorRegistration(this IServiceCollection services)
        {
            var rootPath = Assembly.GetExecutingAssembly().Location;
            var dirRootPath = Path.GetDirectoryName(rootPath);
            var mask = "Redirector.Application.Evaluators.*.dll";
            var files = Directory.EnumerateFiles(dirRootPath, mask, SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                var asm = Assembly.LoadFrom(file);
                var validatorType = asm.ExportedTypes.SingleOrDefault(t => t.Name.EndsWith("Evaluator"));
                if (validatorType != null)
                    services.Add(new ServiceDescriptor(typeof(IRedirectRulesEvaluator), validatorType, ServiceLifetime.Singleton));
            }

            return services;
        }
    }
}
