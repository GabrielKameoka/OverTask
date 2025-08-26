using Microsoft.EntityFrameworkCore;
using OverTask.api.Data;
using OverTask.api.Data.Models;
using OverTask.api.Repositories.Interfaces;
using OverTask.Shared.Models.Dtos.Tarefas;

namespace OverTask.api.Repositories;

public class TarefasRepository : ITarefasRepository
{
    private readonly OverTaskDbContext _context;

    public TarefasRepository(OverTaskDbContext context)
    {
        _context = context;
    }

    public IEnumerable<TarefaReadDto> GetTarefas()
    {
        var tarefas = _context.Tarefas
            .Include(t => t.Usuarios)
            .ToList();

        return tarefas.Select(MapToReadDto).ToList();
    }

    public TarefaReadDto GetTarefa(int id)
    {
        var tarefa = _context.Tarefas
            .Include(t => t.Usuarios)
            .FirstOrDefault(t => t.Id == id);

        if (tarefa == null)
            return null;

        return MapToReadDto(tarefa);
    }

    public TarefaReadDto PostTarefa(TarefaCreateDto tarefaDto)
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

        // Buscar a tarefa com usuário incluído para retornar o ReadDto completo
        var tarefaCriada = _context.Tarefas
            .Include(t => t.Usuarios)
            .FirstOrDefault(t => t.Id == tarefa.Id);

        return MapToReadDto(tarefaCriada);
    }

    public TarefaReadDto PutTarefa(int id, TarefaUpdateDto tarefaDto)
    {
        var tarefa = _context.Tarefas
            .Include(t => t.Usuarios)
            .FirstOrDefault(t => t.Id == id); // Usa o ID da URL
        
        if (tarefa == null)
            return null;

        tarefa.Titulo = tarefaDto.Titulo;
        tarefa.Descricao = tarefaDto.Descricao;
        tarefa.DataVencimento = tarefaDto.DataVencimento;
        tarefa.Situacao = (OverTask.api.Data.Models.Enums.Situacao)tarefaDto.Situacao;
        tarefa.Categoria = (OverTask.api.Data.Models.Enums.Categoria)tarefaDto.Categoria;

        _context.Tarefas.Update(tarefa);
        _context.SaveChanges();

        return MapToReadDto(tarefa);
    }

    public bool DeletarTarefa(int id)
    {
        var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);
        
        if (tarefa == null)
            return false;

        _context.Tarefas.Remove(tarefa);
        _context.SaveChanges();

        return true;
    }

    private TarefaReadDto MapToReadDto(Tarefas tarefa)
    {
        if (tarefa == null)
            return null;

        return new TarefaReadDto
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            Descricao = tarefa.Descricao,
            DataVencimento = tarefa.DataVencimento,
            Situacao = (OverTask.Shared.Models.Situacao)tarefa.Situacao,
            Categoria = (OverTask.Shared.Models.Categoria)tarefa.Categoria,
            NomeUsuario = tarefa.Usuarios?.Nome
        };
    }
}