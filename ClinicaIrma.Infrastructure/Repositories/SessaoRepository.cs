using ClinicaIrma.Domain.Entities;
using ClinicaIrma.Domain.Repositories;
using ClinicaIrma.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaIrma.Infrastructure.Repositories;

public class SessaoRepository : ISessaoRepository
{
    private readonly ClinicaDbContext _context;

    public SessaoRepository(ClinicaDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(Sessao sessao)
    {
        await _context.Sessoes.AddAsync(sessao);
        await _context.SaveChangesAsync();
        
        return sessao.Id;
    }

    public async Task<List<Sessao>> GetByPacienteIdAsync(Guid pacienteId)
{
    return await _context.Sessoes
        .AsNoTracking() // Mais rápido para leituras
        .Where(s => s.PacienteId == pacienteId)
        .OrderByDescending(s => s.DataHora) 
        .ToListAsync();
}

    public async Task<Sessao?> GetByIdAsync(Guid id)
    {
        return await _context.Sessoes.FindAsync(id);
    }

    public async Task UpdateAsync(Sessao sessao)
    {
        _context.Sessoes.Update(sessao);
        await _context.SaveChangesAsync();
    }
}