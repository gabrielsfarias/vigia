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
public class CreateModel(ApplicationDbContext context, UserManager<Usuario> userManager) : PageModel
{
    [BindProperty]
    public Documento Documento { get; set; } = null!;

    [BindProperty]
    public string? NovoTipoDocumentoNome { get; set; }

    public SelectList TiposDocumentosSelectList { get; set; } = null!;

    public async Task OnGetAsync()
    {
        TiposDocumentosSelectList = new SelectList(
            await context.TiposDocumentos.ToListAsync(),
            "Id",
            "Nome"
        );
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(NovoTipoDocumentoNome) && Documento.TipoDocumentoId == 0)
        {
            ModelState.AddModelError(
                "Documento.TipoDocumentoId",
                "Selecione um tipo existente ou informe um novo tipo."
            );
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage); // Veja o erro no console
            }
            TiposDocumentosSelectList = new SelectList(
                await context.TiposDocumentos.ToListAsync(),
                "Id",
                "Nome"
            );
            return Page();
        }

        if (!string.IsNullOrWhiteSpace(NovoTipoDocumentoNome))
        {
            var tipoExistente = await context.TiposDocumentos.FirstOrDefaultAsync(t =>
                t.Nome.ToLower() == NovoTipoDocumentoNome.ToLower()
            );

            if (tipoExistente != null)
            {
                Documento.TipoDocumentoId = tipoExistente.Id;
            }
            else
            {
                var novoTipo = new TipoDocumento { Nome = NovoTipoDocumentoNome.Trim() };
                context.TiposDocumentos.Add(novoTipo);
                await context.SaveChangesAsync();
                Documento.TipoDocumentoId = novoTipo.Id;
            }
        }

        var usuario = await userManager.GetUserAsync(User);
        Documento.UsuarioId = usuario!.Id;

        context.Documentos.Add(Documento);
        await context.SaveChangesAsync();

        return RedirectToPage("/Dashboard");
    }
}
