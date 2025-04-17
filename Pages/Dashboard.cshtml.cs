using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using vigia.Data;
using vigia.Models;
using Microsoft.AspNetCore.Mvc;

namespace vigia.Pages;

[Authorize]
public class DashboardModel(ApplicationDbContext context, UserManager<Usuario> userManager) : PageModel
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

        Documentos = await _context.Documentos
            .Include(d => d.TipoDocumento)
            .Where(d => d.UsuarioId == usuario.Id)
            .ToListAsync();

        return Page();
    }
}
