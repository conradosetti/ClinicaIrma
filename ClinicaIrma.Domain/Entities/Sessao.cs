namespace ClinicaIrma.Domain.Entities;
public class Sessao
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PacienteId { get; set; } 
    
    public DateTime DataHora { get; set; }
    
    // Pode ser: "Realizada", "Falta", "CanceladaPeloPaciente"
    public string StatusComparecimento { get; set; } = "Realizada"; 
    
    // Prontuário / Evolução
    public string NotasEvolucao { get; set; } = string.Empty; 
    public string AnexosUrl { get; set; } = string.Empty;

    // Propriedade de Navegação do Entity Framework
    public Paciente? Paciente { get; set; }
}