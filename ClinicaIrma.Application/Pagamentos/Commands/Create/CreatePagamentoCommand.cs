using ClinicaIrma.Domain.Entities;

namespace ClinicaIrma.Application.Pagamentos.Commands.Create;

public class CreatePagamentoCommand
{
    public Guid PacienteId { get; set; } 
    public decimal Valor { get; set; }
    public string Descricao { get; set; } = string.Empty; 
    public DateTime DataVencimento { get; set; }
    
    // Na criação, o pagamento pode já ser feito no momento, ou ficar para depois
    public DateTime? DataPagamento { get; set; } 
    public string Status { get; set; } = "Pendente"; 

    public Pagamento ToEntity()
    {
        return new Pagamento
        {
            PacienteId = PacienteId,
            Valor = Valor,
            Descricao = Descricao,
            DataVencimento = DataVencimento,
            DataPagamento = DataPagamento,
            Status = Status,
        };
    }
}