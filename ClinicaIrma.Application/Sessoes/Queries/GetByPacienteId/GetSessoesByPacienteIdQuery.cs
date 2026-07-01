namespace ClinicaIrma.Application.Sessoes.Queries.GetByPacienteId;

public class GetSessoesByPacienteIdQuery
{
    public Guid PacienteId { get; set; }

    public GetSessoesByPacienteIdQuery(Guid pacienteId)
    {
        PacienteId = pacienteId;
    }
}