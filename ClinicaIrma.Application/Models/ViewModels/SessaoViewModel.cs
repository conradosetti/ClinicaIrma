using ClinicaIrma.Domain.Entities;

namespace ClinicaIrma.Application.Models.ViewModels;

public class SessaoViewModel
{
    public Guid Id { get; set; }
    public DateTime DataHora { get; set; }
    public string StatusComparecimento { get; set; } = string.Empty;
    public string NotasEvolucao { get; set; } = string.Empty;
    public string AnexosUrl { get; set; } = string.Empty;

    public static SessaoViewModel FromEntity(Sessao sessao)
    {
        return new SessaoViewModel
        {
            Id = sessao.Id,
            DataHora = sessao.DataHora,
            StatusComparecimento = sessao.StatusComparecimento,
            NotasEvolucao = sessao.NotasEvolucao,
            AnexosUrl = sessao.AnexosUrl
        };
    }
}