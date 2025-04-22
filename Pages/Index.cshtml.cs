using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace vigia.Pages;

public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;

    public IActionResult OnGet()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            _logger.LogInformation(
                "Usuário autenticado redirecionado para Dashboard: {User}",
                User.Identity.Name
            );
            return RedirectToPage("/Dashboard");
        }

        _logger.LogInformation("Visitante acessou a landing page.");
        return Page();
    }
}
