using System;
using OverTask.api.Data.Models.Enums;

namespace OverTask.api.Data.Models;

public class Tarefas
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public DateOnly DataVencimento { get; set; }
    public Situacao Situacao { get; set; }
    public Categoria Categoria { get; set; }
    public int UsuarioId { get; set; } 
    public Usuarios? Usuarios { get; set; } 
}