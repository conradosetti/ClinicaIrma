using ClinicaIrma.Domain.Entities;

namespace ClinicaIrma.Domain.Repositories;

public interface IPagamentoRepository
{
    Task<Guid> AddAsync(Pagamento pagamento);
    Task<List<Pagamento>> GetByPacienteIdAsync(Guid pacienteId);
    Task<Pagamento?> GetByIdAsync(Guid id);
    Task UpdateAsync(Pagamento pagamento);
}