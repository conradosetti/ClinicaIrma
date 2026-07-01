using Microsoft.Extensions.DependencyInjection;
using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Application.Pacientes.Commands.Create;
using ClinicaIrma.Application.Models.ViewModels;
using ClinicaIrma.Application.Pacientes.Queries.GetById;
using ClinicaIrma.Application.Sessoes.Commands.Create;
using ClinicaIrma.Application.Sessoes.Queries.GetByPacienteId;
using ClinicaIrma.Application.Pagamentos.Commands.Create;
using ClinicaIrma.Application.Pagamentos.Commands.Update;
using ClinicaIrma.Application.Pagamentos.Queries.GetByPacienteId;
using ClinicaIrma.Application.Sessoes.Commands.Update;
using ClinicaIrma.Application.Auth.Commands.Login;

namespace ClinicaIrma.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Injetando o Handler nativamente
        // "Sempre que um Controller pedir um IHandler que receba CreatePacienteCommand e devolva um Guid, entregue o CreatePacienteCommandHandler"
        services.AddScoped<IHandler<Guid, CreatePacienteCommand>, CreatePacienteCommandHandler>();
        services.AddScoped<IHandler<PacienteViewModel?, GetPacienteByIdQuery>, GetPacienteByIdQueryHandler>();
        services.AddScoped<IHandler<Guid, CreateSessaoCommand>, CreateSessaoCommandHandler>();
        services.AddScoped<IHandler<List<SessaoViewModel>, GetSessoesByPacienteIdQuery>, GetSessoesByPacienteIdQueryHandler>();
        services.AddScoped<IHandler<Guid, CreatePagamentoCommand>, CreatePagamentoCommandHandler>();
        services.AddScoped<IHandler<List<PagamentoViewModel>, GetPagamentosByPacienteIdQuery>, GetPagamentosByPacienteIdQueryHandler>();
        services.AddScoped<IHandler<bool, MarcarPagamentoComoPagoCommand>, MarcarPagamentoComoPagoCommandHandler>();
        services.AddScoped<IHandler<bool, RegistrarEvolucaoCommand>, RegistrarEvolucaoCommandHandler>();
        services.AddScoped<IHandler<bool, SinalizarReciboEmitidoCommand>, SinalizarReciboEmitidoCommandHandler>();
        services.AddScoped<IHandler<string, LoginCommand>, LoginCommandHandler>();
        return services;
    }
}