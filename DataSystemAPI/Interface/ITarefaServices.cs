using DataSystemAPI.Models;

namespace DataSystemAPI.Interface
{
    public interface ITarefaServices
    {
        Task<IEnumerable<TarefaRequest>> GetAllTarefasAsync();
        Task<TarefaRequest> GetTarefaByIdAsync(int id);
        Task<bool> AddTarefaAsync(TarefaRequest tarefa);
        Task<bool> UpdateTarefaAsync(TarefaResponse tarefa, int Id);
        Task<bool> DeleteTarefaByIDAsync(int id);

    }
}
