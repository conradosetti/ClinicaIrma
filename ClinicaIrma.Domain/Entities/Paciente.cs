namespace ClinicaIrma.Domain.Entities;

public class Paciente
{
    public Guid Id { get; set; }

    // --- Dados Obrigatórios Iniciais ---
    public string NomeCompleto { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Profissao { get; set; } = string.Empty;
    public string Raca { get; set; } = string.Empty;
    public string ContatoEmergencia { get; set; } = string.Empty;
    public decimal ValorSessaoAcordado { get; set; }

    // --- Dados Contingentes (Podem ser preenchidos depois) ---
    public string? QuemResideNaCasa { get; set; }
    public string? Genero { get; set; }
    public string? OrientacaoSexual { get; set; }
    public string? Religiao { get; set; }

    // --- Documentação Macro ---
    // O texto único de evolução geral do caso
    public string? Prontuario { get; set; } 
    
    // O PostgreSQL do Entity Framework suporta listas nativamente como Arrays de Texto (text[])
    public List<string> AnexosCadastroUrl { get; set; } = new();

    // --- Relacionamentos de Navegação ---
    public ICollection<Sessao> Sessoes { get; set; } = new List<Sessao>();
    public ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}