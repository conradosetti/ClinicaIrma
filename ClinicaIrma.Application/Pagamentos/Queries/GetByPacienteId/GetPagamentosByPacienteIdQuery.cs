namespace ClinicaIrma.Application.Pagamentos.Queries.GetByPacienteId;

public class GetPagamentosByPacienteIdQuery
{
    public Guid PacienteId { get; set; }

    public GetPagamentosByPacienteIdQuery(Guid pacienteId)
    {
        PacienteId = pacienteId;
    }
}