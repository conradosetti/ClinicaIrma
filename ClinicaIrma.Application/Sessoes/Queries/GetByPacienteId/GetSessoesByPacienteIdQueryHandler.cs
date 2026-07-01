using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Application.Models.ViewModels;
using ClinicaIrma.Domain.Repositories;

namespace ClinicaIrma.Application.Sessoes.Queries.GetByPacienteId;

public class GetSessoesByPacienteIdQueryHandler : IHandler<List<SessaoViewModel>, GetSessoesByPacienteIdQuery>
{
    private readonly ISessaoRepository _repository;

    public GetSessoesByPacienteIdQueryHandler(ISessaoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SessaoViewModel>> HandleAsync(GetSessoesByPacienteIdQuery item, CancellationToken cancellationToken)
    {
        var sessoes = await _repository.GetByPacienteIdAsync(item.PacienteId);

        // Converte a lista de Entidades para a lista de ViewModels
        return sessoes.Select(SessaoViewModel.FromEntity).ToList();
    }
}