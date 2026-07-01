using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Domain.Repositories;

namespace ClinicaIrma.Application.Sessoes.Commands.Update;

public class RegistrarEvolucaoCommandHandler : IHandler<bool, RegistrarEvolucaoCommand>
{
    private readonly ISessaoRepository _repository;

    public RegistrarEvolucaoCommandHandler(ISessaoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> HandleAsync(RegistrarEvolucaoCommand item, CancellationToken cancellationToken)
    {
        // 1. Busca a sessão agendada no banco de dados
        var sessao = await _repository.GetByIdAsync(item.SessaoId);
        
        if (sessao == null)
            return false; // Sessão não existe

        // 2. Atualiza os dados clínicos (Prontuário)
        sessao.StatusComparecimento = item.StatusComparecimento;
        sessao.NotasEvolucao = item.NotasEvolucao;
        
        // Só atualiza os anexos se for enviado algo
        if (!string.IsNullOrWhiteSpace(item.AnexosUrl))
        {
            sessao.AnexosUrl = item.AnexosUrl;
        }

        // 3. Salva as alterações
        await _repository.UpdateAsync(sessao);
        
        return true;
    }
}