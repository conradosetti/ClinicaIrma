namespace ClinicaIrma.Application.Sessoes.Commands.Update;

public class RegistrarEvolucaoCommand
{
    // O ID virá da URL da API, mas precisamos dele aqui para o Handler o processar
    public Guid SessaoId { get; set; } 
    
    public string StatusComparecimento { get; set; } = string.Empty; 
    public string NotasEvolucao { get; set; } = string.Empty;
    public string AnexosUrl { get; set; } = string.Empty;
}