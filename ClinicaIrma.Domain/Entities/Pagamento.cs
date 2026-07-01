namespace ClinicaIrma.Domain.Entities;
public class Pagamento
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PacienteId { get; set; } 
    
    public decimal Valor { get; set; }
    
    // Ex: "Pacote de Junho/2026" ou "Sessão Avulsa 15/06"
    public string Descricao { get; set; } = string.Empty; 
    
    public DateTime DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; } // Anulável, pois pode não ter sido pago ainda
    
    // Pode ser: "Pendente", "Pago", "Cancelado"
    public string Status { get; set; } = "Pendente"; 
    // --- Novos campos para controle fiscal ---
    public bool ReciboReceitaFederalEmitido { get; set; } = false;
    public string? UrlReciboGerado { get; set; }

    // Propriedade de Navegação do Entity Framework
    public Paciente? Paciente { get; set; }
}