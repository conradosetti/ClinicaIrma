using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Domain.Repositories;
using ClinicaIrma.Infrastructure.Auth;
using ClinicaIrma.Infrastructure.Data;
using ClinicaIrma.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicaIrma.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Configura o Banco de Dados
        services.AddDbContext<ClinicaDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // 2. Registra os Repositórios
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<ISessaoRepository, SessaoRepository>();
        services.AddScoped<IPagamentoRepository, PagamentoRepository>();
        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}