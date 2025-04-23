using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vigia.Data;
using vigia.Models;

namespace vigia.Pages.Documentos;

[Authorize]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Usuario> _userManager;

    public EditModel(ApplicationDbContext context, UserManager<Usuario> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public Documento Documento { get; set; } = default!;

    public SelectList TiposDocumentos { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(long? id)
    {
        var usuario = await _userManager.GetUserAsync(User);
        if (usuario == null)
            return NotFound();

        if (id == null)
            return NotFound();

        Documento = (
            await _context
                .Documentos.Include(d => d.TipoDocumento)
                .FirstOrDefaultAsync(d => d.Id == id && d.UsuarioId == usuario.Id)
        )!;

        if (Documento == null)
            return NotFound();

        // Preenche o dropdown
        TiposDocumentos = new SelectList(
            await _context.TiposDocumentos.ToListAsync(),
            "Id",
            "Nome",
            Documento.TipoDocumentoId
        );

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var usuario = await _userManager.GetUserAsync(User);
        if (usuario == null)
            return NotFound();

        if (!ModelState.IsValid)
        {
            // Recarrega o dropdown em caso de erro
            TiposDocumentos = new SelectList(
                await _context.TiposDocumentos.ToListAsync(),
                "Id",
                "Nome",
                Documento.TipoDocumentoId
            );
            return Page();
        }

        var documentoExistente = await _context.Documentos.FirstOrDefaultAsync(d =>
            d.Id == Documento.Id && d.UsuarioId == usuario.Id
        );

        if (documentoExistente == null)
            return NotFound();

        documentoExistente.TipoDocumentoId = Documento.TipoDocumentoId;
        documentoExistente.NumeroDocumento = Documento.NumeroDocumento;
        documentoExistente.DataValidade = Documento.DataValidade;

        await _context.SaveChangesAsync();

        return RedirectToPage("/Dashboard");
    }
}
