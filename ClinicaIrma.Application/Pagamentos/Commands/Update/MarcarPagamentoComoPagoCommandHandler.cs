using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Domain.Repositories;

namespace ClinicaIrma.Application.Pagamentos.Commands.Update;

// Devolve 'bool' para indicar sucesso ou falha (caso o ID não exista)
public class MarcarPagamentoComoPagoCommandHandler : IHandler<bool, MarcarPagamentoComoPagoCommand>
{
    private readonly IPagamentoRepository _repository;

    public MarcarPagamentoComoPagoCommandHandler(IPagamentoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> HandleAsync(MarcarPagamentoComoPagoCommand item, CancellationToken cancellationToken)
    {
        // 1. O pagamento existe?
        var pagamento = await _repository.GetByIdAsync(item.PagamentoId);
        
        if (pagamento == null)
            return false; 

        // 2. Aplicar as Regras de Negócio da Baixa
        pagamento.Status = "Pago";
        pagamento.DataPagamento = DateTime.UtcNow; // Carimba o momento exato

        // 3. Persistir a alteração
        await _repository.UpdateAsync(pagamento);
        
        return true;
    }
}