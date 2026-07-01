using ClinicaIrma.Domain.Entities;

namespace ClinicaIrma.Domain.Repositories;

public interface ISessaoRepository
{
    // Adiciona uma nova sessão e retorna o ID gerado
    Task<Guid> AddAsync(Sessao sessao);
    Task<List<Sessao>> GetByPacienteIdAsync(Guid pacienteId);
    Task<Sessao?> GetByIdAsync(Guid id);
    Task UpdateAsync(Sessao sessao);
}