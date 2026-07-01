using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Domain.Repositories;

namespace ClinicaIrma.Application.Pagamentos.Commands.Create;

public class CreatePagamentoCommandHandler : IHandler<Guid, CreatePagamentoCommand>
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IPacienteRepository _pacienteRepository;

    public CreatePagamentoCommandHandler(
        IPagamentoRepository pagamentoRepository, 
        IPacienteRepository pacienteRepository)
    {
        _pagamentoRepository = pagamentoRepository;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Guid> HandleAsync(CreatePagamentoCommand item, CancellationToken cancellationToken)
    {
        // 1. Regra de Negócio: O paciente tem de existir
        var paciente = await _pacienteRepository.GetByIdAsync(item.PacienteId) ?? throw new InvalidOperationException("Não é possível lançar um pagamento para um paciente inexistente.");

        // 2. Converter para Entidade
        var pagamento = item.ToEntity();

        // 3. Salvar na base de dados
        await _pagamentoRepository.AddAsync(pagamento);

        // 4. Devolver o ID
        return pagamento.Id;
    }
}