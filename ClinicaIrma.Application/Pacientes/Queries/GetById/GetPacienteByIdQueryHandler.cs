using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Application.Models.ViewModels;
using ClinicaIrma.Domain.Repositories;

namespace ClinicaIrma.Application.Pacientes.Queries.GetById;

// Recebe a Query e devolve o ViewModel (ou nulo, se não achar)
public class GetPacienteByIdQueryHandler : IHandler<PacienteViewModel?, GetPacienteByIdQuery>
{
    private readonly IPacienteRepository _repository;

    public GetPacienteByIdQueryHandler(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<PacienteViewModel?> HandleAsync(GetPacienteByIdQuery item, CancellationToken cancellationToken)
    {
        var paciente = await _repository.GetByIdAsync(item.Id);

        if (paciente == null)
            return null;

        return PacienteViewModel.FromEntity(paciente);
    }
}