using ClinicaIrma.Application.Interfaces;
using ClinicaIrma.Application.Models.ViewModels;
using ClinicaIrma.Application.Pacientes.Commands.Create;
using ClinicaIrma.Application.Pacientes.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaIrma.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PacientesController(IHandler<Guid, CreatePacienteCommand> createPacienteHandler,
                           IHandler<PacienteViewModel?, GetPacienteByIdQuery> getPacienteByIdHandler) : ControllerBase
{
    // A dependência é explícita: este controller precisa de alguém que saiba
    // processar um CreatePacienteCommand e devolver um Guid.
    private readonly IHandler<Guid, CreatePacienteCommand> _createPacienteHandler = createPacienteHandler;
    private readonly IHandler<PacienteViewModel?, GetPacienteByIdQuery> _getPacienteByIdHandler = getPacienteByIdHandler;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePacienteCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // O controle passa para a camada de Application
            var id = await _createPacienteHandler.HandleAsync(command, cancellationToken);
            
            // Retorna o status HTTP 201 (Created) e o cabeçalho "Location" 
            // apontando para onde o recurso recém-criado pode ser acessado.
            return CreatedAtAction(nameof(GetByIdAsync), new { id = id }, new { id = id });
        }
        catch (InvalidOperationException ex)
        {
            // Captura as violações de regras de negócio (ex: CPF já existe) 
            // e converte graciosamente para um HTTP 400 (Bad Request)
            return BadRequest(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            // Tratamento genérico de segurança para não expor stack trace em caso de falha de banco
            return StatusCode(500, new { erro = "Ocorreu um erro interno ao processar a solicitação.", detalhe = ex.Message });
        }
    }

    // Rota de leitura (Query) criada como placeholder temporário para que 
    // o método CreatedAtAction acima funcione corretamente e retorne a URL válida.
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPacienteByIdQuery(id);
        var result = await _getPacienteByIdHandler.HandleAsync(query, cancellationToken);

        if (result == null)
            return NotFound(new { erro = "Paciente não encontrado." });

        return Ok(result);
    }
}