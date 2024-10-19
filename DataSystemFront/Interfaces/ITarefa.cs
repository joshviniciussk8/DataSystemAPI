using DataSystemFront.Domain.Models;

namespace DataSystemFront.Interfaces
{
    public interface ITarefa
    {
        IEnumerable<TarefaModel> GetAllTarefasAsync();
        TarefaModel GetTarefaByIdAsync(int id);
        string AddTarefaAsync(TarefaModel tarefa);
        string UpdateTarefaAsync(TarefaModel tarefa, int Id);
        bool DeleteTarefaByIDAsync(int id);
    }
}
