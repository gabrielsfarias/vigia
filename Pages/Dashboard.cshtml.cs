using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using vigia.Data;
using vigia.Models;

namespace vigia.Pages;

[Authorize]
public class DashboardModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Usuario> _userManager;

    public DashboardModel(ApplicationDbContext context, UserManager<Usuario> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public Documento? Documento { get; set; } = null!;

    public IList<Documento> Documentos { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        var usuario = await _userManager.GetUserAsync(User);

        if (usuario is null)
        {
            return NotFound();
        }

        Documentos = await _context
            .Documentos.Include(d => d.TipoDocumento)
            .Where(d => d.UsuarioId == usuario.Id)
            .ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnGetListDocumentsPartialAsync()
    {
        var usuario = await _userManager.GetUserAsync(User);

        if (usuario is null)
        {
            return NotFound();
        }

        var documentos = await _context
            .Documentos.Include(d => d.TipoDocumento)
            .Where(d => d.UsuarioId == usuario.Id)
            .ToListAsync();

        return Partial("_DocumentosTableBody", documentos);
    }

    public async Task<IActionResult> OnPostDeleteAsync(long id)
    {
        var usuario = await _userManager.GetUserAsync(User);

        if (usuario is null)
        {
            return NotFound();
        }

        var documento = await _context.Documentos.FindAsync(id);

        if (documento == null)
        {
            return NotFound();
        }

        if (documento.UsuarioId != usuario.Id)
        {
            return Forbid();
        }

        _context.Documentos.Remove(documento);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Documento apagado com sucesso!";

        return RedirectToPage();
    }
}
