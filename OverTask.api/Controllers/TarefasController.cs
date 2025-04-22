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
[Route("api/tarefas")]
public class TarefasController : ControllerBase
{
    private readonly OverTaskDbContext _context;

    public TarefasController(OverTaskDbContext context)
    {
        _context = context;
    }

    private TarefaReadDto MapToReadDto(Tarefas tarefa)
    {
        return new TarefaReadDto
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            Descricao = tarefa.Descricao,
            DataVencimento = tarefa.DataVencimento,
            Situacao = (SituacaoDto)tarefa.Situacao,
            Categoria = (CategoriaDto)tarefa.Categoria,
            NomeUsuario = tarefa.Usuarios?.Nome // usando resumo do usuário
        };
    }

    // GET: api/tarefas
    [HttpGet]
    public ActionResult<IEnumerable<TarefaReadDto>> GetTarefas()
    {
        var tarefas = _context.Tarefas.Include(t => t.Usuarios).ToList();

        var tarefasReadDto = tarefas.Select(MapToReadDto).ToList();

        return Ok(tarefasReadDto);
    }


    // GET: api/tarefas/id
    [HttpGet("{id}")]
    public ActionResult<TarefaReadDto> GetTarefa(int id)
    {
        var tarefa = _context.Tarefas.Include(t => t.Usuarios).FirstOrDefault(t => t.Id == id);

        if (tarefa == null)
        {
            return NotFound();
        }

        return Ok(MapToReadDto(tarefa));
    }

    //POST: api/tarefas
    [HttpPost]
    public ActionResult<TarefaCreateDto> PostTarefa(TarefaCreateDto tarefaDto)
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

        var tarefaReadDto = new TarefaReadDto
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            Descricao = tarefaDto.Descricao,
            DataVencimento = tarefaDto.DataVencimento,
            Situacao = (SituacaoDto)tarefa.Situacao,
            Categoria = (CategoriaDto)tarefaDto.Categoria,
            NomeUsuario = tarefa.Usuarios?.Nome
        };

        return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefaReadDto);
    }

    //PUT: api/tarefas/id
    [HttpPut("{id}")]
    public IActionResult PutTarefa(int id, TarefaUpdateDto tarefaDto)
    {

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
    
    //DELETE: api/tarefas/id
    [HttpDelete("{id}")]
    public IActionResult DeleteTarefa(int id)
    {
        var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);
        
        if (tarefa == null)
            return NotFound();
        
        _context.Tarefas.Remove(tarefa);
        _context.SaveChanges();

        return NoContent();
    }
}