using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vigia.Models;

namespace vigia.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
    }
}