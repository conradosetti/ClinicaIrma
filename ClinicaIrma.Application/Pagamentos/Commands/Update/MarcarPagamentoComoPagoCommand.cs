namespace ClinicaIrma.Application.Pagamentos.Commands.Update;

public class MarcarPagamentoComoPagoCommand(Guid pagamentoId)
{
    public Guid PagamentoId { get; set; } = pagamentoId;
}