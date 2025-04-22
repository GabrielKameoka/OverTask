using System.Collections.Generic;
using System.Linq;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OverTask.api.Data;
using OverTask.api.Data.Models;
using OverTask.api.Data.Models.Enums;
using OverTask.Shared.Models.Dtos.Tarefas;
using OverTask.Shared.Models.Dtos.Usuarios;
using CategoriaDto = OverTask.Shared.Models.Categoria;
using SituacaoDto = OverTask.Shared.Models.Situacao;


namespace OverTask.api.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly OverTaskDbContext _context;

    public UsuariosController(OverTaskDbContext context)
    {
        _context = context;
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



    private TarefaReadDto MapTarefaToDto(Tarefas tarefa)
    {
        return new TarefaReadDto
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            Descricao = tarefa.Descricao,
            DataVencimento = tarefa.DataVencimento,
            Situacao = (SituacaoDto)tarefa.Situacao,
            Categoria = (CategoriaDto)tarefa.Categoria,
            NomeUsuario = tarefa.Usuarios?.Nome
        };
    }




    //GET: api/usuarios
    [HttpGet]
    public ActionResult<List<UsuarioReadDto>> GetUsuario()
    {
        var usuarios = _context.Usuarios.Include(u => u.TarefasList).ToList();

        var usuariosReadDto = usuarios.Select(MapToReadDto).ToList();

        return Ok(usuariosReadDto);
    }




    //GET: api/usuarios/id
    [HttpGet("{id}")]
    public ActionResult<UsuarioReadDto> GetUsuario(int id)
    {
        var usuario = _context.Usuarios.Include(u => u.TarefasList).FirstOrDefault(u => u.Id == id);

        if (usuario == null)
        {
            return NotFound();
        }

        var usuarioReadDto = new UsuarioReadDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            TarefasList = usuario.TarefasList.Select(t => new TarefaResumoDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Situacao = (SituacaoDto)t.Situacao
            }).ToList()
        };

        return Ok(usuarioReadDto);
    }


    //Post: api/usuarios
    [HttpPost]
    public ActionResult<UsuarioCreateDto> PostUsuario(UsuarioCreateDto usuarioDto)
    {
        var usuario = new Usuarios
        {
            Nome = usuarioDto.Nome,
            Email = usuarioDto.Email,
            Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha)
        };

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        var usuarioReadDto = new UsuarioReadDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            TarefasList = new List<TarefaResumoDto>()
        };

        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuarioReadDto);
    }


    //Put: api/usuarios/id
    [HttpPut("{id}")]
    public ActionResult PutUsuario(int id, UsuarioUpdateDto usuarioDto)
    {

        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);

        if (usuario == null)
            return NotFound("Usuário não encontrado.");

        usuario.Nome = usuarioDto.Nome;
        usuario.Email = usuarioDto.Email;
        usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha);

        _context.Usuarios.Update(usuario);

        try
        {
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar usuário: {ex.Message}");
            return StatusCode(500, "Erro interno ao processar a solicitação.");
        }

        return NoContent();
    }
    
    
    //DELETE: api/usuarios
    [HttpDelete("{id}")]
    public ActionResult DeleteUsuario(int id)
    {
        var usuario = _context.Usuarios.Include(u => u.TarefasList).FirstOrDefault(u => u.Id == id);

        if (usuario == null)
            return NotFound("Usuário não encontrado.");

        if (usuario.TarefasList.Any())  
            return BadRequest("Não é possível excluir um usuário com tarefas associadas.");

        _context.Remove(usuario);

        try
        {
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao excluir usuário: {ex.Message}");
            return StatusCode(500, "Erro interno ao processar a solicitação.");
        }

        return NoContent();
    }

}