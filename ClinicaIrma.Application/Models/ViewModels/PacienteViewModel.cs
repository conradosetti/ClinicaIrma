using ClinicaIrma.Domain.Entities;

namespace ClinicaIrma.Application.Models.ViewModels;

public class PacienteViewModel
{
    public Guid Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public decimal ValorSessaoAcordado { get; set; }
    public string? Prontuario { get; set; }

    // Método estático para converter a Entidade neste ViewModel
    public static PacienteViewModel FromEntity(Paciente paciente)
    {
        return new PacienteViewModel
        {
            Id = paciente.Id,
            NomeCompleto = paciente.NomeCompleto,
            Cpf = paciente.Cpf,
            // Podemos formatar ou ocultar dados aqui se precisarmos
            ValorSessaoAcordado = paciente.ValorSessaoAcordado,
            Prontuario = paciente.Prontuario
        };
    }
}