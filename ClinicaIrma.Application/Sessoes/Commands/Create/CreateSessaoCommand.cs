using ClinicaIrma.Domain.Entities;

namespace ClinicaIrma.Application.Sessoes.Commands.Create;

public class CreateSessaoCommand
{
    // O ID do paciente é obrigatório para vincularmos no banco
    public Guid PacienteId { get; set; } 
    public DateTime DataHora { get; set; }
    public string StatusComparecimento { get; set; } = "Agendada"; 
    public string NotasEvolucao { get; set; } = string.Empty;
    public string AnexosUrl { get; set; } = string.Empty;

    public Sessao ToEntity()
    {
        return new Sessao
        {
            PacienteId = PacienteId,
            DataHora = DataHora,
            StatusComparecimento = StatusComparecimento,
            NotasEvolucao = NotasEvolucao,
            AnexosUrl = AnexosUrl
        };
    }
}