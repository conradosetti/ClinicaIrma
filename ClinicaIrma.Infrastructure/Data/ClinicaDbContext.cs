using ClinicaIrma.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicaIrma.Infrastructure.Data;

public class ClinicaDbContext : DbContext
{
    public ClinicaDbContext(DbContextOptions<ClinicaDbContext> options) : base(options)
    {
    }

    // Representação das tabelas no banco de dados
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- Configuração da Tabela Paciente ---
        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasIndex(p => p.Cpf).IsUnique(); // Garante CPF único
            entity.Property(p => p.NomeCompleto).IsRequired().HasMaxLength(150);
            
            // Define a precisão monetária (18 dígitos no total, 2 casas decimais)
            entity.Property(p => p.ValorSessaoAcordado).HasColumnType("decimal(18,2)");
        });

        // --- Configuração da Tabela Sessao ---
        modelBuilder.Entity<Sessao>(entity =>
        {
            entity.HasKey(s => s.Id);
            
            // Relacionamento 1:N (Paciente -> Sessões)
            entity.HasOne(s => s.Paciente)
                  .WithMany(p => p.Sessoes)
                  .HasForeignKey(s => s.PacienteId)
                  .OnDelete(DeleteBehavior.Cascade); // Se deletar o paciente, deleta as sessões
        });

        // --- Configuração da Tabela Pagamento ---
        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Valor).HasColumnType("decimal(18,2)");
            
            // Relacionamento 1:N (Paciente -> Pagamentos)
            entity.HasOne(p => p.Paciente)
                  .WithMany(paciente => paciente.Pagamentos)
                  .HasForeignKey(p => p.PacienteId)
                  .OnDelete(DeleteBehavior.Cascade); // Se deletar o paciente, deleta o financeiro
        });
    }
}