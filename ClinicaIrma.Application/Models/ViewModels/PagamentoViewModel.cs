using ClinicaIrma.Domain.Entities;

namespace ClinicaIrma.Application.Models.ViewModels;

public class PagamentoViewModel
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool ReciboReceitaFederalEmitido { get; set; } = false;
    public string? UrlReciboGerado { get; set; }

    public static PagamentoViewModel FromEntity(Pagamento pagamento)
    {
        return new PagamentoViewModel
        {
            Id = pagamento.Id,
            Valor = pagamento.Valor,
            Descricao = pagamento.Descricao,
            DataVencimento = pagamento.DataVencimento,
            DataPagamento = pagamento.DataPagamento,
            Status = pagamento.Status,
            ReciboReceitaFederalEmitido = pagamento.ReciboReceitaFederalEmitido,
            UrlReciboGerado = pagamento.UrlReciboGerado
        };
    }
}