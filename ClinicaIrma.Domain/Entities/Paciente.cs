namespace ClinicaIrma.Domain.Entities;
public class Paciente
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string NomeCompleto { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    
    // Regra de Negócio: O valor negociado com este paciente específico
    public decimal ValorSessaoAcordado { get; set; } 
    
    // Dados clínicos base
    public string ObservacoesIniciais { get; set; } = string.Empty;

    // Relacionamentos (1:N)
    // Inicializamos as listas para evitar exceções de NullReference
    public ICollection<Sessao> Sessoes { get; set; } = new List<Sessao>();
    public ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}