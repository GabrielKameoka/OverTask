namespace OverTask.api.Data.Models;

public class Usuarios
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; }
    public List<Tarefas> TarefasList { get; set; } = new List<Tarefas>();
}