using ClinicaIrma.Domain.Entities;

namespace ClinicaIrma.Domain.Repositories;

public interface IPacienteRepository
{
    // Adiciona o paciente e retorna o ID gerado
    Task<Guid> AddAsync(Paciente paciente);
    
    // Essencial para garantirmos que não haverá duplicidade na clínica
    Task<bool> CpfExisteAsync(string cpf); 

    Task<Paciente?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}