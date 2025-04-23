using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using vigia.Data;
using vigia.Models;

namespace vigia.Pages.Documentos
{
    public class CreateModel(ApplicationDbContext context) : PageModel
    {
        // Propriedade para o formulário (bind dos dados do documento)
        [BindProperty]
        public required Documento Documento { get; set; }

        // Propriedade para o novo tipo informado (bind do input texto)
        [BindProperty]
        public string NovoTipo { get; set; } = string.Empty;

        // Propriedade para popular o dropdown na view (não bind, só leitura)
        public required List<TipoDocumento> TiposDocumentos { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Carrega os tipos para o dropdown
            TiposDocumentos = await context.TiposDocumentos.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Documento.UsuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            TiposDocumentos = await context.TiposDocumentos.ToListAsync();

            bool tipoExistentePreenchido = Documento.TipoDocumentoId > 0;
            bool novoTipoPreenchido = !string.IsNullOrWhiteSpace(NovoTipo);

            if (!tipoExistentePreenchido && !novoTipoPreenchido)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Selecione um tipo existente OU informe um novo tipo."
                );
            }
            else if (tipoExistentePreenchido && novoTipoPreenchido)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Escolha apenas uma opção: tipo existente OU novo tipo."
                );
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (novoTipoPreenchido)
            {
                var novoTipoDocumento = new TipoDocumento { Nome = NovoTipo.Trim() };
                context.TiposDocumentos.Add(novoTipoDocumento);
                await context.SaveChangesAsync();
                Documento.TipoDocumentoId = novoTipoDocumento.Id;
            }

            context.Documentos.Add(Documento);
            await context.SaveChangesAsync();

            return RedirectToPage("/Dashboard");
        }
    }
}
