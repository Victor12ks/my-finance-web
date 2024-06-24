using MyFinanceWeb.Application.Applications;
using MyFinanceWeb.Application.Services;
using MyFinanceWeb.Domain.Interfaces.Applications;
using MyFinanceWeb.Domain.Interfaces.Repositories;
using MyFinanceWeb.Domain.Interfaces.Services;
using MyFinanceWeb.Infra.Repositories;

namespace MyFinanceWeb.Server.Configuration
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IPlanoContaRepository, PlanoContaRepository>();
            services.AddScoped<IPlanoContaService, PlanoContaService>();
            services.AddScoped<ITransacaoService, TransacaoService>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            services.AddScoped<IPlanoContaApplication, PlanoContaApplication>();
            services.AddScoped<ITransacaoApplication, TransacaoApplication>();
            services.AddScoped<IDashboardApplication, DashboardApplication>();

        }
    }
}