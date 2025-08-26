using OverTask.Shared.Models.Dtos.Tarefas;

namespace OverTask.api.Repositories.Interfaces;

public interface ITarefasRepository
{
    public IEnumerable<TarefaReadDto> GetTarefas();
    public TarefaReadDto GetTarefa(int id);
    public TarefaReadDto PostTarefa(TarefaCreateDto tarefaDto);
    public TarefaReadDto PutTarefa(int id, TarefaUpdateDto tarefaDto);
    public bool DeletarTarefa(int id);
}