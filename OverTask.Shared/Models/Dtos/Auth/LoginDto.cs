using System.ComponentModel.DataAnnotations;

namespace OverTask.Shared.Models.Dtos.Auth;

public class LoginDto
{
    [Required] public string Nome { get; set; }
    [Required] public string Senha { get; set; }
}