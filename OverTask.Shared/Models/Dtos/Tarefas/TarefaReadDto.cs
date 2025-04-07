namespace OverTask.Shared.Models.Dtos.Tarefas;

public class TarefaReadDto
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public DateOnly DataVencimento { get; set; }
    public Situacao Situacao { get; set; }
    public Categoria Categoria { get; set; }
    public string? NomeUsuario { get; set; } // resumo
}