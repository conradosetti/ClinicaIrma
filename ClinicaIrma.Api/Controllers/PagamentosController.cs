using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Application.Models.ViewModels;
using ClinicaIrma.Application.Pagamentos.Commands.Create;
using ClinicaIrma.Application.Pagamentos.Commands.Update;
using ClinicaIrma.Application.Pagamentos.Queries.GetByPacienteId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaIrma.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PagamentosController(IHandler<Guid, CreatePagamentoCommand> createPagamentoHandler,
    IHandler<List<PagamentoViewModel>, GetPagamentosByPacienteIdQuery> getPagamentosHandler,
    IHandler<bool, MarcarPagamentoComoPagoCommand> marcarComoPagoHandler,
    IHandler<bool, SinalizarReciboEmitidoCommand> sinalizarReciboEmitidoHandler) : ControllerBase
{
    private readonly IHandler<Guid, CreatePagamentoCommand> _createPagamentoHandler = createPagamentoHandler;
    private readonly IHandler<List<PagamentoViewModel>, GetPagamentosByPacienteIdQuery> _getPagamentosHandler = getPagamentosHandler;
    private readonly IHandler<bool, MarcarPagamentoComoPagoCommand> _marcarComoPagoHandler = marcarComoPagoHandler;
    private readonly IHandler<bool, SinalizarReciboEmitidoCommand> _sinalizarReciboEmitidoHandler = sinalizarReciboEmitidoHandler;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePagamentoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var id = await _createPagamentoHandler.HandleAsync(command, cancellationToken);
            
            // Retorna a indicação de sucesso com o ID gerado
            return Created($"api/Pagamentos/{id}", new { id = id });
        }
        catch (InvalidOperationException ex)
        {
            // Bloqueia tentativas de associar pagamentos a pacientes que não existem
            return BadRequest(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = "Ocorreu um erro interno ao registar o pagamento.", detalhe = ex.Message });
        }
    }

    // Rota explícita para obter o extrato financeiro do paciente
    [HttpGet("paciente/{pacienteId:guid}")]
    public async Task<IActionResult> GetByPaciente(Guid pacienteId, CancellationToken cancellationToken)
    {
        var query = new GetPagamentosByPacienteIdQuery(pacienteId);
        var result = await _getPagamentosHandler.HandleAsync(query, cancellationToken);
        
        return Ok(result);
    }
    [HttpPatch("{id:guid}/pagar")]
    public async Task<IActionResult> MarcarComoPago(Guid id, CancellationToken cancellationToken)
    {
        var command = new MarcarPagamentoComoPagoCommand(id);
        var sucesso = await _marcarComoPagoHandler.HandleAsync(command, cancellationToken);

        if (!sucesso)
            return NotFound(new { erro = "Pagamento não encontrado." });

        // Status 204 indica que a ação foi bem-sucedida, mas não há um JSON novo para devolver no corpo da resposta
        return NoContent(); 
    }
    // Rota focada em sinalizar que o recibo foi emitido e enviado
    [HttpPatch("{id:guid}/recibo")]
    public async Task<IActionResult> SinalizarRecibo(Guid id, [FromBody] SinalizarReciboEmitidoCommand command, CancellationToken cancellationToken)
    {
        command.PagamentoId = id;
        var sucesso = await _sinalizarReciboEmitidoHandler.HandleAsync(command, cancellationToken);

        if (!sucesso)
            return NotFound(new { erro = "Pagamento não encontrado." });

        return NoContent(); // Status 204
    }
}