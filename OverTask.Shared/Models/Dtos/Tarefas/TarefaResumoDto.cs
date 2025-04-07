namespace OverTask.Shared.Models.Dtos.Tarefas;

public class TarefaResumoDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = "";
    public Situacao Situacao { get; set; }
}
