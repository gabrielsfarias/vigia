using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using vigia.Models; // Ajuste o namespace conforme a localização dos seus models

namespace vigia.Pages;

[Authorize]
public class DashboardModel : PageModel
{
    // Propriedade pública para ser usada na view
    public IList<Documento> Documentos { get; set; } = new List<Documento>();

    private readonly UserManager<Usuario> _userManager;
    public DashboardModel(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }
    public void OnGet()
    {
        var usuario = _userManager.GetUserAsync(User).Result;
        Documentos = [new() { Id = 1, TipoDocumento = new TipoDocumento { Nome = "Exemplo" }, DataValidade = DateTime.Today.AddDays(10), UsuarioId = usuario.Id, Usuario = usuario }];
    }
}
