using ClinicaIrma.Domain.Entities;

namespace ClinicaIrma.Application.Pacientes.Commands.Create;

public class CreatePacienteCommand
{
    // Campos que virão do JSON do Frontend
    public string NomeCompleto { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Profissao { get; set; } = string.Empty;
    public string Raca { get; set; } = string.Empty;
    public string ContatoEmergencia { get; set; } = string.Empty;
    public decimal ValorSessaoAcordado { get; set; }

    // Campos contingentes (opcionais na hora da criação)
    public string? QuemResideNaCasa { get; set; }
    public string? Genero { get; set; }
    public string? OrientacaoSexual { get; set; }
    public string? Religiao { get; set; }

    // O método de fábrica fica mais robusto
    public Paciente ToEntity()
    {
        return new Paciente
        {
            NomeCompleto = NomeCompleto,
            DataNascimento = DataNascimento,
            Cpf = Cpf,
            Endereco = Endereco,
            Profissao = Profissao,
            Raca = Raca,
            ContatoEmergencia = ContatoEmergencia,
            ValorSessaoAcordado = ValorSessaoAcordado,
            
            QuemResideNaCasa = QuemResideNaCasa,
            Genero = Genero,
            OrientacaoSexual = OrientacaoSexual,
            Religiao = Religiao,
            
            // O prontuário e os anexos começam vazios por padrão na criação
            Prontuario = null,
            AnexosCadastroUrl = new List<string>()
        };
    }
}