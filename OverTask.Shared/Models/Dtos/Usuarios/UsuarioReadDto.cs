using OverTask.Shared.Models.Dtos.Tarefas;


namespace OverTask.Shared.Models.Dtos.Usuarios;

public class UsuarioReadDto
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public List<TarefaResumoDto> TarefasList { get; set; } = new();
}