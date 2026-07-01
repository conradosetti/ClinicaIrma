using ClinicaIrma.Domain.Entities;
using ClinicaIrma.Domain.Repositories;
using ClinicaIrma.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaIrma.Infrastructure.Repositories;

public class PacienteRepository : IPacienteRepository
{
    private readonly ClinicaDbContext _context;

    public PacienteRepository(ClinicaDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(Paciente paciente)
    {
        // Prepara o objeto para ser inserido
        await _context.Pacientes.AddAsync(paciente);
        
        // Efetiva a transação no PostgreSQL
        await _context.SaveChangesAsync();
        
        // O Entity Framework preenche automaticamente o ID gerado (se for o caso)
        return paciente.Id;
    }

    public async Task<bool> CpfExisteAsync(string cpf)
    {
        // O AnyAsync é extremamente performático. 
        // Ele não traz os dados do paciente para a memória, apenas roda um "SELECT EXISTS" direto no banco.
        return await _context.Pacientes.AnyAsync(p => p.Cpf == cpf);
    }

    public async Task<Paciente?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Pacientes
            .AsNoTracking() // Melhora a performance em consultas de leitura
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}