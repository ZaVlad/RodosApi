using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RodosApi.Installers
{
    public static class InstallerExstension
    {
        public static void InstallServices(IServiceCollection services,IConfiguration configuration)
        {
            var instalServices = typeof(Startup).Assembly
                .GetExportedTypes()
                .Where(s => typeof(IInstaller).IsAssignableFrom(s) && !s.IsAbstract&& !s.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();
            instalServices.ForEach(s=>s.InstallServices(services,configuration));
        }
    }
}
