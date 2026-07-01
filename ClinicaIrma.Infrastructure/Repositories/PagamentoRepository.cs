using ClinicaIrma.Domain.Entities;
using ClinicaIrma.Domain.Repositories;
using ClinicaIrma.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaIrma.Infrastructure.Repositories;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly ClinicaDbContext _context;

    public PagamentoRepository(ClinicaDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(Pagamento pagamento)
    {
        await _context.Pagamentos.AddAsync(pagamento);
        await _context.SaveChangesAsync();
        
        return pagamento.Id;
    }

    public async Task<List<Pagamento>> GetByPacienteIdAsync(Guid pacienteId)
    {
        return await _context.Pagamentos
        .AsNoTracking() // Otimização de performance para consultas de leitura
        .Where(p => p.PacienteId == pacienteId)
        .OrderByDescending(p => p.DataVencimento) // Vencimentos mais recentes primeiro
        .ToListAsync();
    }
    public async Task<Pagamento?> GetByIdAsync(Guid id)
{
    return await _context.Pagamentos.FindAsync(id);
}

public async Task UpdateAsync(Pagamento pagamento)
{
    // O Entity Framework deteta a alteração do estado e atualiza apenas os campos necessários
    _context.Pagamentos.Update(pagamento);
    await _context.SaveChangesAsync();
}
}