using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Application.Models.ViewModels;
using ClinicaIrma.Domain.Repositories;

namespace ClinicaIrma.Application.Pagamentos.Queries.GetByPacienteId;

public class GetPagamentosByPacienteIdQueryHandler : IHandler<List<PagamentoViewModel>, GetPagamentosByPacienteIdQuery>
{
    private readonly IPagamentoRepository _repository;

    public GetPagamentosByPacienteIdQueryHandler(IPagamentoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PagamentoViewModel>> HandleAsync(GetPagamentosByPacienteIdQuery item, CancellationToken cancellationToken)
    {
        var pagamentos = await _repository.GetByPacienteIdAsync(item.PacienteId);

        // Mapeia a lista de Entidades para a lista de ViewModels de retorno
        return pagamentos.Select(PagamentoViewModel.FromEntity).ToList();
    }
}