using Microsoft.EntityFrameworkCore;
using OverTask.api.Data;
using OverTask.api.Data.Models;
using OverTask.api.Repositories.Interfaces;
using OverTask.Shared.Models.Dtos.Usuarios;
using BCryptNet = BCrypt.Net.BCrypt;
using OverTask.Shared.Models.Dtos.Tarefas;

namespace OverTask.api.Repositories;

public class UsuariosRepository : IUsuariosRepository
{
    private readonly OverTaskDbContext _context;

    public UsuariosRepository(OverTaskDbContext context)
    {
        _context = context;
    }

    public List<UsuarioReadDto> GetUsuario()
    {
        var usuarios = _context.Usuarios
            .Include(u => u.TarefasList)
            .ToList();

        return usuarios.Select(MapToReadDto).ToList();
    }

    public UsuarioReadDto GetUsuario(int id)
    {
        var usuario = _context.Usuarios
            .Include(u => u.TarefasList)
            .FirstOrDefault(u => u.Id == id);

        if (usuario == null)
            return null;

        return MapToReadDto(usuario);
    }

    public UsuarioReadDto PostUsuario(UsuarioCreateDto usuarioDto)
    {
        var usuario = new Usuarios
        {
            Nome = usuarioDto.Nome,
            Email = usuarioDto.Email,
            Senha = BCryptNet.HashPassword(usuarioDto.Senha)
        };

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        // Retorna o DTO de leitura com o ID gerado
        return new UsuarioReadDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            TarefasList = new List<TarefaResumoDto>()
        };
    }

    public UsuarioUpdateDto PutUsuario(int id, UsuarioUpdateDto usuarioDto)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
        
        if (usuario == null)
            return null;

        usuario.Nome = usuarioDto.Nome;
        usuario.Email = usuarioDto.Email;
        usuario.Senha = BCryptNet.HashPassword(usuarioDto.Senha);

        _context.Usuarios.Update(usuario);
        _context.SaveChanges();

        return usuarioDto;
    }

    public bool DeletarUsuario(int id)
    {
        var usuario = _context.Usuarios
            .Include(u => u.TarefasList)
            .FirstOrDefault(u => u.Id == id);

        if (usuario == null)
            return false;

        if (usuario.TarefasList.Any())
            throw new InvalidOperationException("Não é possível excluir um usuário com tarefas associadas.");

        _context.Usuarios.Remove(usuario);
        _context.SaveChanges();

        return true;
    }

    private UsuarioReadDto MapToReadDto(Usuarios usuario)
    {
        return new UsuarioReadDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            TarefasList = usuario.TarefasList.Select(t => new TarefaResumoDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Situacao = (OverTask.Shared.Models.Situacao)t.Situacao
            }).ToList()
        };
    }
}