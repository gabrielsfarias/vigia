using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using vigia.Data;
using vigia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace vigia.Pages.Documentos;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Usuario> _userManager;

    public CreateModel(ApplicationDbContext context, UserManager<Usuario> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public Documento Documento { get; set; } = null!;

    [BindProperty]
    public string? NovoTipoDocumentoNome { get; set; }

    public SelectList TiposDocumentosSelectList { get; set; } = null!;

    public async Task OnGetAsync()
    {
        TiposDocumentosSelectList = new SelectList(await _context.TiposDocumentos.ToListAsync(), "Id", "Nome");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(NovoTipoDocumentoNome) && Documento.TipoDocumentoId == 0)
        {
            ModelState.AddModelError("Documento.TipoDocumentoId", "Selecione um tipo existente ou informe um novo tipo.");
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
    foreach (var error in errors)
    {
        Console.WriteLine(error.ErrorMessage); // Veja o erro no console
    }
            TiposDocumentosSelectList = new SelectList(await _context.TiposDocumentos.ToListAsync(), "Id", "Nome");
            return Page();
        }

        if (!string.IsNullOrWhiteSpace(NovoTipoDocumentoNome))
        {
            var tipoExistente = await _context.TiposDocumentos
                .FirstOrDefaultAsync(t => t.Nome.ToLower() == NovoTipoDocumentoNome.ToLower());

            if (tipoExistente != null)
            {
                Documento.TipoDocumentoId = tipoExistente.Id;
            }
            else
            {
                var novoTipo = new TipoDocumento { Nome = NovoTipoDocumentoNome.Trim() };
                _context.TiposDocumentos.Add(novoTipo);
                await _context.SaveChangesAsync();
                Documento.TipoDocumentoId = novoTipo.Id;
            }
        }

        var usuario = await _userManager.GetUserAsync(User);
        Documento.UsuarioId = usuario!.Id;

        _context.Documentos.Add(Documento);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Dashboard");
    }
}
