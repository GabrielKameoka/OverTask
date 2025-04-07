using System.ComponentModel.DataAnnotations;

namespace OverTask.Shared.Models.Dtos.Usuarios;

public class UsuarioCreateDto
{
    [Required]
    public required string Nome { get; set; }

    [Required, EmailAddress]
    public required string Email { get; set; }

    [Required, MinLength(6)]
    public required string Senha { get; set; }
}