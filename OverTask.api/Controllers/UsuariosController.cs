using Microsoft.AspNetCore.Mvc;
using OverTask.api.Data;
using OverTask.api.Data.Models;
using OverTask.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

    private UsuariosDto MapUsuarioToDto(Usuarios usuario)
    {
        return new UsuariosDto
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
            Senha = usuario.Senha,
            TarefasList = usuario.TarefasList.Select(MapTarefaToDto).ToList()
        };
    }

    private TarefasDto MapTarefaToDto(Tarefas tarefa)
    {
        return new TarefasDto
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            Descricao = tarefa.Descricao,
            DataVencimento = tarefa.DataVencimento,
            Situacao = (Situacao)tarefa.Situacao,
            Categoria = (Categoria)tarefa.Categoria,
            UsuarioId = tarefa.UsuarioId,
            Usuarios = null
        };
    }


    //GET: api/usuarios
    [HttpGet]
    public ActionResult<IEnumerable<UsuariosDto>> GetUsuarios()
    {
        var usuarios = _context.Usuarios.Include(u => u.TarefasList).ToList();
        return usuarios.Select(MapUsuarioToDto).ToList();
    }


    //GET: api/usuarios/id
    [HttpGet("{id}")]
    public ActionResult<UsuariosDto> GetUsuario(int id)
    {
        var usuario = _context.Usuarios.Include(u => u.TarefasList).FirstOrDefault(u => u.Id == id);

        if (usuario == null)
        {
            return NotFound();
        }

        return MapUsuarioToDto(usuario);
    }


    //Post: api/usuarios
    [HttpPost]
    public ActionResult<UsuariosDto> PostUsuario(UsuariosDto usuarioDto)
    {
        var usuario = new Usuarios
        {
            Nome = usuarioDto.Nome,
            Email = usuarioDto.Email,
            Senha = usuarioDto.Senha
        };

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        var usuarioDtoRetornado = MapUsuarioToDto(usuario);

        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuarioDtoRetornado);
    }


    //Put: api/usuarios
    [HttpPut("{id}")]
    public ActionResult<UsuariosDto> PutUsuario(int id, UsuariosDto usuarioDto)
    {
        if (id != usuarioDto.Id)
            return BadRequest();
        
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
        
        if (usuario == null)
            return NotFound();
        
        usuario.Nome = usuarioDto.Nome;
        usuario.Email = usuarioDto.Email;
        usuario.Senha = usuarioDto.Senha;
        
        _context.Usuarios.Update(usuario);
        _context.SaveChanges();

        return usuarioDto;
    }
    
    
    //DELETE: api/usuarios
    [HttpDelete("{id}")]
    public ActionResult<UsuariosDto> DeleteUsuario(int id)
    {
        
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
        
        if (usuario == null)
            return NotFound();

        _context.Remove(usuario);
        _context.SaveChanges();

        return NoContent();
    }
}