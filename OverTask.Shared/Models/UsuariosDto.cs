namespace OverTask.Shared.Models;

public class UsuariosDto
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; }
    public List<TarefasDto> TarefasList { get; set; } = new List<TarefasDto>();
}