using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RodosApi.Installers
{
    public interface IInstaller
    {
        public void InstallServices(IServiceCollection services,IConfiguration configuration);
    }
}