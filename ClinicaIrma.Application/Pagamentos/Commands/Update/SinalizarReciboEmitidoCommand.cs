namespace ClinicaIrma.Application.Pagamentos.Commands.Update;

public class SinalizarReciboEmitidoCommand
{
    public Guid PagamentoId { get; set; }
    
    // Opcional: Se o sistema gerar um PDF e salvar na nuvem, guardamos a URL
    public string? UrlReciboGerado { get; set; } 
}