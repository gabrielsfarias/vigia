using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vigia.Models;

namespace vigia.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<Usuario>(options)
    {
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<TipoDocumento> TiposDocumentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Documento>().ToTable("Documentos");
            modelBuilder.Entity<TipoDocumento>().ToTable("TiposDocumento");
        }
    }
}
