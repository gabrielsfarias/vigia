using Microsoft.EntityFrameworkCore;
using vigia.Models;

namespace vigia.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }
}