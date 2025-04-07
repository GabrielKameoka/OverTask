using System.ComponentModel.DataAnnotations;

namespace OverTask.Shared.Models.Dtos.Tarefas;

public class TarefaCreateDto
{
    [Required]
    public required string Titulo { get; set; }

    [Required]
    public required string Descricao { get; set; }

    public DateOnly DataVencimento { get; set; }

    [Required]
    public Situacao Situacao { get; set; }

    [Required]
    public Categoria Categoria { get; set; }

    [Required]
    public int UsuarioId { get; set; }
}