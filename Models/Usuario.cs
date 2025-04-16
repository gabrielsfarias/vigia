using Microsoft.AspNetCore.Identity;

namespace vigia.Models;

public class Usuario : IdentityUser
{
    public required string Nome { get; set; }
    public required string Senha { get; set; }
}
