using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace OverTask.api.Data.Models;

public class Usuarios
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Senha { get; set; }

    public List<Tarefas> TarefasList { get; set; } = new List<Tarefas>();
}