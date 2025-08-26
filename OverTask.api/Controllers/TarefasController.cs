using Microsoft.AspNetCore.Mvc;
using OverTask.api.Repositories.Interfaces;
using OverTask.Shared.Models.Dtos.Tarefas;

namespace OverTask.api.Controllers;

[ApiController]
[Route("api/tarefas")]
public class TarefasController : ControllerBase
{
    private readonly ITarefasRepository _tarefasRepository;

    public TarefasController(ITarefasRepository tarefasRepository)
    {
        _tarefasRepository = tarefasRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TarefaReadDto>> GetTarefas()
    {
        var tarefas = _tarefasRepository.GetTarefas();
        return Ok(tarefas);
    }

    [HttpGet("{id}")]
    public ActionResult<TarefaReadDto> GetTarefa(int id)
    {
        var tarefa = _tarefasRepository.GetTarefa(id);

        if (tarefa == null)
        {
            return NotFound();
        }

        return Ok(tarefa);
    }

    [HttpPost]
    public ActionResult<TarefaReadDto> PostTarefa(TarefaCreateDto tarefaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var tarefaCriada = _tarefasRepository.PostTarefa(tarefaDto);
            
            if (tarefaCriada == null)
            {
                return BadRequest("Erro ao criar tarefa");
            }

            return CreatedAtAction(nameof(GetTarefa), new { id = tarefaCriada.Id }, tarefaCriada);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult PutTarefa(int id, TarefaUpdateDto tarefaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = _tarefasRepository.PutTarefa(id, tarefaDto); // Passa o ID da URL
            
            if (result == null)
            {
                return NotFound("Tarefa não encontrada");
            }

            return Ok(result); // Retorna a tarefa atualizada
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteTarefa(int id)
    {
        try
        {
            var result = _tarefasRepository.DeletarTarefa(id);
            
            if (!result)
            {
                return NotFound("Tarefa não encontrada");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }
}