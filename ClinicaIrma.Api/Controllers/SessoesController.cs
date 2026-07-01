using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Application.Models.ViewModels;
using ClinicaIrma.Application.Sessoes.Commands.Create;
using ClinicaIrma.Application.Sessoes.Commands.Update;
using ClinicaIrma.Application.Sessoes.Queries.GetByPacienteId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaIrma.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SessoesController(IHandler<Guid, CreateSessaoCommand> createSessaoHandler,
IHandler<List<SessaoViewModel>, GetSessoesByPacienteIdQuery> getSessoesByPacienteIdHandler,
IHandler<bool, RegistrarEvolucaoCommand> registrarEvolucaoHandler) : ControllerBase
{
    private readonly IHandler<Guid, CreateSessaoCommand> _createSessaoHandler = createSessaoHandler;
    private readonly IHandler<List<SessaoViewModel>, GetSessoesByPacienteIdQuery> _getSessoesByPacienteIdHandler = getSessoesByPacienteIdHandler;
    private readonly IHandler<bool, RegistrarEvolucaoCommand> _registrarEvolucaoHandler = registrarEvolucaoHandler;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSessaoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var id = await _createSessaoHandler.HandleAsync(command, cancellationToken);
            
            // Usamos o Created de forma mais simples aqui, enquanto não construímos a Query (GET) para as Sessões
            return Created($"api/Sessoes/{id}", new { id = id });
        }
        catch (InvalidOperationException ex)
        {
            // Se o PacienteId for inválido ou não existir, o sistema entra aqui e devolve um erro 400 amigável
            return BadRequest(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = "Ocorreu um erro interno ao tentar registar a sessão.", detalhe = ex.Message });
        }
    }

    [HttpGet("paciente/{pacienteId:guid}")]
    public async Task<IActionResult> GetByPaciente(Guid pacienteId, CancellationToken cancellationToken)
    {
        var query = new GetSessoesByPacienteIdQuery(pacienteId);
        var result = await _getSessoesByPacienteIdHandler.HandleAsync(query, cancellationToken);
        
        // Retornamos um array (mesmo que vazio se o paciente for novo e não tiver sessões)
        return Ok(result);
    }
    // Rota focada em atualizar os dados clínicos de uma sessão
    [HttpPatch("{id:guid}/evolucao")]
    public async Task<IActionResult> RegistrarEvolucao(Guid id, [FromBody] RegistrarEvolucaoCommand command, CancellationToken cancellationToken)
    {
        // Vincula o ID da URL ao comando para o Handler saber qual sessão alterar
        command.SessaoId = id;

        var sucesso = await _registrarEvolucaoHandler.HandleAsync(command, cancellationToken);

        if (!sucesso)
            return NotFound(new { erro = "Sessão não encontrada." });

        return NoContent(); // Status 204: Atualizado com sucesso
    }
}