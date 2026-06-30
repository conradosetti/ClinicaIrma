using Microsoft.Extensions.DependencyInjection;

namespace ClinicaIrma.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // No futuro, todos os seus Services (ex: PacienteService, ReciboService) 
        // serão registrados automaticamente aqui.
        // services.AddScoped<IPacienteService, PacienteService>();
        
        return services;
    }
}