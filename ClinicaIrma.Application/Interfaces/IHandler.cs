namespace ClinicaIrma.Application.Interfaces;

/// <summary>
/// Interface genérica para padronizar todos os Handlers (Commands e Queries) da aplicação.
/// </summary>
/// <typeparam name="TResponse">O tipo de dado que será retornado (ex: Guid, bool, PacienteDto)</typeparam>
/// <typeparam name="TItem">O tipo de dado que está entrando (ex: CreatePacienteCommand)</typeparam>
public interface IHandler<TResponse, in TItem>
{
    Task<TResponse> HandleAsync(TItem item, CancellationToken cancellationToken);
}