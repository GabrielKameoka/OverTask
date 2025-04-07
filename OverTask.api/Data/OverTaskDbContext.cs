using Microsoft.EntityFrameworkCore;
using OverTask.api.Data.Models;

namespace OverTask.api.Data;

public class OverTaskDbContext : DbContext
{
    public OverTaskDbContext(DbContextOptions<OverTaskDbContext> options) : base(options) { }

    public DbSet<Tarefas> Tarefas { get; set; }
    public DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relacionamento: 1 Usuário -> Muitas Tarefas
        modelBuilder.Entity<Tarefas>()
            .HasOne(t => t.Usuarios)
            .WithMany(u => u.TarefasList)
            .HasForeignKey(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict); // Evita cascade delete (opcional)

        // Configurações adicionais (opcional)
        modelBuilder.Entity<Usuarios>()
            .HasIndex(u => u.Email)
            .IsUnique(); // Exemplo: força e-mail único

        base.OnModelCreating(modelBuilder);
    }
}