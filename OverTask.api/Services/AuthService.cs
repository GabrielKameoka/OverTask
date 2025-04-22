using Microsoft.EntityFrameworkCore;
using OverTask.api.Controllers;
using OverTask.api.Data;
using OverTask.Shared.Models.Dtos.Auth;

namespace OverTask.api.Services // Ou o namespace correto
{
    public class AuthService : IAuthService
    {
        // Dependências que este serviço possa ter (ex: ApplicationDbContext, ITokenService)
        private readonly OverTaskDbContext _context;
        // private readonly ITokenService _tokenService;

        public AuthService(OverTaskDbContext context /*, ITokenService tokenService */)
        {
            _context = context;
            // _tokenService = tokenService;
        }

        public async Task<AuthDto> Authenticate(string username, string password)
        {
            // Sua lógica de autenticação aqui (verificar usuário, senha, gerar token)
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Nome == username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Senha))
            {
                // Supondo que você tenha um serviço para gerar tokens
                var token = "seu_token_aqui"; // _tokenService.GenerateToken(user);
                return new AuthDto { AccessToken = token };
            }
            return null;
        }
    }
}