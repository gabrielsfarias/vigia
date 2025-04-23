namespace vigia.Models;

public class Documento
{
    public long Id { get; set; }
    public DateTime DataValidade { get; set; }
    public int? TipoDocumentoId { get; set; }
    public string NumeroDocumento { get; set; } = string.Empty;
    public TipoDocumento? TipoDocumento { get; set; }
    public string UsuarioId { get; set; } = string.Empty;
    public Usuario? Usuario { get; set; }
}
