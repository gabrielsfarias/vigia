using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using vigia.Data;
using vigia.Models;

namespace vigia.Pages;

[Authorize]
public class DashboardModel(ApplicationDbContext context, UserManager<Usuario> userManager)
    : PageModel
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<Usuario> _userManager = userManager;

    public IList<Documento> Documentos { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        var usuario = await _userManager.GetUserAsync(User);

        if (usuario == null)
        {
            return RedirectToPage("/Identity/Account/Login");
        }

        Documentos = await _context
            .Documentos.Include(d => d.TipoDocumento)
            .Where(d => d.UsuarioId == usuario.Id)
            .ToListAsync();

        return Page();
    }
}
