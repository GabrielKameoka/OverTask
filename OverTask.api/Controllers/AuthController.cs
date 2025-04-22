using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OverTask.api.Data;
using OverTask.api.Data.Models;
using OverTask.api.Data.Models.Enums;
using OverTask.Shared.Models.Dtos.Auth;
using OverTask.Shared.Models.Dtos.Tarefas;
using OverTask.Shared.Models.Dtos.Usuarios;
using CategoriaDto = OverTask.Shared.Models.Categoria;
using SituacaoDto = OverTask.Shared.Models.Situacao;

namespace OverTask.api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService; // Injeção de dependência do serviço de autenticação (recomendado)

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")] // Define o endpoint específico para login: /api/Auth/login
    [ProducesResponseType(typeof(AuthDto), 200)] // Documenta a resposta de sucesso
    [ProducesResponseType(400)] // Documenta a resposta de erro de validação
    [ProducesResponseType(401)] // Documenta a resposta de erro de autenticação
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var authResult = await _authService.Authenticate(model.Nome, model.Senha);

        if (authResult == null)
        {
            return Unauthorized(); // Retorna 401 se a autenticação falhar
        }

        return Ok(authResult); // Retorna 200 com o token de acesso
    }

    // Outros endpoints de autenticação como registro, logout, etc. podem ser adicionados aqui
}

public interface IAuthService
{
    Task<AuthDto> Authenticate(string username, string password);
}