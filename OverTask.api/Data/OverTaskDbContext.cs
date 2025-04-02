using Microsoft.EntityFrameworkCore;
using OverTask.api.Data.Models; // Certifique-se de que o namespace está correto

namespace OverTask.api.Data;

public class OverTaskDbContext : DbContext
{
    public OverTaskDbContext(DbContextOptions<OverTaskDbContext> options) : base(options) { }

    public DbSet<Tarefas> Tarefas { get; set; }
    public DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}