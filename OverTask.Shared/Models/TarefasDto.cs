using System;
using OverTask.Shared.Models;

namespace OverTask.Shared.Models;

public class TarefasDto
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public DateOnly DataVencimento { get; set; }
    public Situacao Situacao { get; set; }
    public Categoria Categoria { get; set; }
    public int UsuarioId { get; set; }
    public UsuariosDto? Usuarios { get; set; }
}