namespace vigia.Models;

public class Documento
{
    public long Id { get; set; }
    public DateTime DataValidade { get; set; }
    public int TipoDocumentoId { get; set; }
    public required TipoDocumento TipoDocumento { get; set; }
    public required string UsuarioId { get; set; }
    public required Usuario Usuario { get; set; }
}
