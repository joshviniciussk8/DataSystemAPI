using DataSystemFront.Domain.Models;
using DataSystemFront.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace DataSystemFront.Repository
{
    public class RepositoryTarefa : ITarefa
    {
        private readonly string uprApi = "https://localhost:7138/api/Tarefas";
        public string AddTarefaAsync(TarefaModel tarefa)
        {
            string Resposta_API;
            try
            {
                using var cliente = new HttpClient();
                string jsonObjeto = JsonConvert.SerializeObject(tarefa);
                var content = new StringContent(jsonObjeto, Encoding.UTF8, "application/json");
                var resposta = cliente.PostAsync(uprApi + "/NovaTarefas", content);
                resposta.Wait();
                var retorno = resposta.Result.Content.ReadAsStringAsync();
                Resposta_API = retorno.Result;
                return Resposta_API;


            }
            catch
            {
                throw;
            }
        }

        public Task<bool> DeleteTarefaByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TarefaModel> GetAllTarefasAsync()
        {
            var retorno = new List<TarefaModel>();
            var retornoErro = new List<TarefaModel>();
            try
            {
                using var cliente = new HttpClient();
                var resposta = cliente.GetStringAsync(uprApi+"/TodasTarefas");
                resposta.Wait();
                retorno = JsonConvert.DeserializeObject<TarefaModel[]>(resposta.Result).ToList();
                
            }
            catch
            {
                return retornoErro;
            }
            return retorno;
        }

        public TarefaModel GetTarefaByIdAsync(int id)
        {
            var retorno = new TarefaModel();
            try
            {
                string teste = uprApi + "/id?id=" + id;
                using var cliente = new HttpClient();
                var resposta = cliente.GetStringAsync(uprApi + "/TarefaPorID/" + id);
                resposta.Wait();
                retorno = JsonConvert.DeserializeObject<TarefaModel>(resposta.Result);
                retorno.Id = id;
            }
            catch
            {
                throw;
            }
            return retorno;
        }

        public string UpdateTarefaAsync(TarefaModel tarefa, int Id)
        {
            string Resposta_API;
            try
            {
                using var cliente = new HttpClient();
                string jsonObjeto = JsonConvert.SerializeObject(tarefa);
                var content = new StringContent(jsonObjeto, Encoding.UTF8, "application/json");
                var resposta = cliente.PutAsync(uprApi + "/AlteraTarefa?Id=" + Id, content);
                resposta.Wait();
                var retorno = resposta.Result.Content.ReadAsStringAsync();
                Resposta_API = retorno.Result;
                return Resposta_API;
            }
            catch
            {
                throw;
            }
        }
        public TarefaModel DeletaTarefaAsync(int id)
        {
            var tarefaAlterada = new TarefaModel();
            tarefaAlterada.Id = id;
            try
            {
                using var cliente = new HttpClient();

                var resposta = cliente.DeleteAsync(uprApi + "/DeletaTarefaPorID/" + id);
                resposta.Wait();
                if (resposta.Result.IsSuccessStatusCode)
                {
                    var retorno = resposta.Result.Content.ReadAsStringAsync();
                    //alunoAlterado = JsonConvert.DeserializeObject<AlunoModel>(retorno.Result);
                }
            }
            catch
            {
                throw;
            }
            return tarefaAlterada;
        }
    }
}
