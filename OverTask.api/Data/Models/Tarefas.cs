using System;
using System.ComponentModel.DataAnnotations;
using OverTask.api.Data.Models.Enums;

namespace OverTask.api.Data.Models;

public class Tarefas 
{
    [Key]
    public int Id { get; set; }

    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateOnly DataVencimento { get; set; }
    public Situacao Situacao { get; set; }
    public Categoria Categoria { get; set; }
    
    public int UsuarioId { get; set; } // Chave estrangeira
    public Usuarios Usuario { get; set; } // Propriedade de navegação
}