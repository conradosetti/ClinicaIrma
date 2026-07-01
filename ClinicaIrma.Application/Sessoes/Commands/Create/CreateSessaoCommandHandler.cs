using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Domain.Repositories;

namespace ClinicaIrma.Application.Sessoes.Commands.Create;

public class CreateSessaoCommandHandler : IHandler<Guid, CreateSessaoCommand>
{
    private readonly ISessaoRepository _sessaoRepository;
    private readonly IPacienteRepository _pacienteRepository;

    // Injetamos os dois repositórios, pois precisamos checar o paciente antes de salvar a sessão
    public CreateSessaoCommandHandler(ISessaoRepository sessaoRepository, IPacienteRepository pacienteRepository)
    {
        _sessaoRepository = sessaoRepository;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Guid> HandleAsync(CreateSessaoCommand item, CancellationToken cancellationToken)
    {
        // 1. Validação: O paciente existe?
        var paciente = await _pacienteRepository.GetByIdAsync(item.PacienteId, cancellationToken);
        if (paciente == null)
        {
            throw new InvalidOperationException("Não é possível registrar uma sessão para um paciente inexistente.");
        }

        // 2. Transforma o comando em entidade
        var sessao = item.ToEntity();

        // 3. Salva a sessão no banco de dados
        await _sessaoRepository.AddAsync(sessao);

        // 4. Retorna o ID gerado
        return sessao.Id;
    }
}