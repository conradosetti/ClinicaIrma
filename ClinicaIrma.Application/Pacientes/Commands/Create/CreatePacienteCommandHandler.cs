using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Domain.Repositories;

namespace ClinicaIrma.Application.Pacientes.Commands.Create;

// Implementamos a interface nativa que acabamos de criar
public class CreatePacienteCommandHandler : IHandler<Guid, CreatePacienteCommand>
{
    private readonly IPacienteRepository _repository;

    public CreatePacienteCommandHandler(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> HandleAsync(CreatePacienteCommand item, CancellationToken cancellationToken)
    {
        // 1. Validação de Regra de Negócio
        var cpfJaExiste = await _repository.CpfExisteAsync(item.Cpf);
        if (cpfJaExiste)
        {
            throw new InvalidOperationException("Já existe um paciente cadastrado com este CPF.");
        }

        // 2. Converter para Entidade
        var paciente = item.ToEntity();

        // 3. Persistir os dados
        await _repository.AddAsync(paciente);

        // 4. Retornar o ID gerado
        return paciente.Id;
    }
}