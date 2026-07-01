namespace ClinicaIrma.Application.Pacientes.Queries.GetById;

// É apenas um objeto de transporte carregando o ID que queremos buscar
public class GetPacienteByIdQuery
{
    public Guid Id { get; set; }

    public GetPacienteByIdQuery(Guid id)
    {
        Id = id;
    }
}