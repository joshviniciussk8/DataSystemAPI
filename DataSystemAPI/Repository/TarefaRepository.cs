using DataSystemAPI.Models;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Primitives;

namespace DataSystemAPI.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public TarefaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public async Task<bool> AddTarefaAsync(TarefaRequest tarefa)
        {
            string sql = @"insert into Tarefa(Titulo,Descricao,DataCriacao,DataConclusao,Status)values(@Titulo,@Descricao,@DataCriacao,@DataConclusao,@Status)";
            using var con = new SqlConnection(connectionString);
            return await con.ExecuteAsync(sql, tarefa) > 0;
        }

        public async Task<bool> DeleteTarefaByIDAsync(int id)
        {
            string sql = @"delete from Tarefa where Id = @Id ";
            var parametros = new DynamicParameters();

            using var con = new SqlConnection(connectionString);
            return await con.ExecuteAsync(sql, new { Id = id }) > 0;
        }

        public async Task<IEnumerable<TarefaRequest>> GetAllTarefasAsync()
        {
            string sql = @"select * from Tarefa with(nolock)";
            using var con = new SqlConnection(connectionString);
            return await con.QueryAsync<TarefaRequest>(sql);
        }

        public async Task<TarefaRequest> GetTarefaByIdAsync(int id)
        {
            string sql = @"select * from Tarefa with(nolock) where Id = @Id";
            using var con = new SqlConnection(connectionString);
            var tarefa = await con.QueryFirstOrDefaultAsync<TarefaRequest>(sql, new { Id = id });
            return tarefa; 
            
        }

        public async Task<bool> UpdateTarefaAsync(TarefaResponse tarefa, int Id)
        {
            string sql = @"update Tarefa set Titulo = @Titulo, Descricao =@Descricao, DataConclusao = @DataConclusao, Status = @Status where Id = @Id";
            var parametros = new DynamicParameters();
            parametros.Add("Id", Id);
            parametros.Add("Titulo", tarefa.Titulo);
            parametros.Add("Descricao", tarefa.Descricao);
            parametros.Add("DataConclusao", tarefa.DataConclusao);
            parametros.Add("Status", tarefa.status);
            using var con = new SqlConnection(connectionString);
            return await con.ExecuteAsync(sql, parametros)> 0;
        }
    }
}
