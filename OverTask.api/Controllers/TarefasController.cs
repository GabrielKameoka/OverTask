using Microsoft.AspNetCore.Mvc;
using OverTask.api.Data;
using OverTask.api.Data.Models;
using OverTask.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OverTask.api.Controllers;

[ApiController]
[Route("api/tarefas")]
public class TarefasController : ControllerBase
{
    private readonly OverTaskDbContext _context;

    public TarefasController(OverTaskDbContext context)
    {
        _context = context;
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
            Usuarios = MapUsuarioToDto(tarefa.Usuarios)
        };
    }

    private UsuariosDto? MapUsuarioToDto(Usuarios? usuario)
    {
        if (usuario == null)
        {
            return null;
        }

        return new UsuariosDto
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
            Senha = usuario.Senha,
            TarefasList = null
        };
    }

    // GET: api/tarefas
    [HttpGet]
    public ActionResult<IEnumerable<TarefasDto>> GetTarefas()
    {
        var tarefas = _context.Tarefas.Include(t => t.Usuarios).ToList();
        return tarefas.Select(MapTarefaToDto).ToList();
    }

    // GET: api/tarefas/id
    [HttpGet("{id}")]
    public ActionResult<TarefasDto> GetTarefa(int id)
    {
        var tarefa = _context.Tarefas.Include(t => t.Usuarios).FirstOrDefault(t => t.Id == id);

        if (tarefa == null)
        {
            return NotFound();
        }

        return MapTarefaToDto(tarefa);
    }

    //POST: api/tarefas
    [HttpPost]
    public ActionResult<TarefasDto> PostTarefa(TarefasDto tarefaDto)
    {
        var tarefa = new Tarefas
        {
            Titulo = tarefaDto.Titulo,
            Descricao = tarefaDto.Descricao,
            DataVencimento = tarefaDto.DataVencimento,
            Situacao = (OverTask.api.Data.Models.Enums.Situacao)tarefaDto.Situacao,
            Categoria = (OverTask.api.Data.Models.Enums.Categoria)tarefaDto.Categoria,
            UsuarioId = tarefaDto.UsuarioId
        };

        _context.Tarefas.Add(tarefa);
        _context.SaveChanges();

        var tarefaDtoRetornado = MapTarefaToDto(tarefa);

        return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefaDtoRetornado);
    }

    //PUT: api/tarefas/id
    [HttpPut("{id}")]
    public IActionResult PutTarefa(int id, TarefasDto tarefaDto)
    {
        if (id != tarefaDto.Id)
            return BadRequest();

        var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);

        if (tarefa == null)
            return NotFound();

        tarefa.Titulo = tarefaDto.Titulo;
        tarefa.Descricao = tarefaDto.Descricao;
        tarefa.Situacao = (Data.Models.Enums.Situacao)tarefaDto.Situacao;

        _context.Tarefas.Update(tarefa);
        _context.SaveChanges();

        return NoContent();
    }
}