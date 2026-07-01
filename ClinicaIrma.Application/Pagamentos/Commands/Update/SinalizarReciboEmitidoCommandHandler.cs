using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Domain.Repositories;

namespace ClinicaIrma.Application.Pagamentos.Commands.Update;

public class SinalizarReciboEmitidoCommandHandler(IPagamentoRepository repository) : IHandler<bool, SinalizarReciboEmitidoCommand>
{
    private readonly IPagamentoRepository _repository = repository;

    public async Task<bool> HandleAsync(SinalizarReciboEmitidoCommand item, CancellationToken cancellationToken)
    {
        var pagamento = await _repository.GetByIdAsync(item.PagamentoId);
        
        if (pagamento == null)
            return false;

        // Regra: Sinaliza a emissão fiscal
        pagamento.ReciboReceitaFederalEmitido = true;
        
        if (!string.IsNullOrWhiteSpace(item.UrlReciboGerado))
        {
            pagamento.UrlReciboGerado = item.UrlReciboGerado;
        }

        await _repository.UpdateAsync(pagamento);
        
        return true;
    }
}