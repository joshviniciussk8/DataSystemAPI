using DataSystemAPI.Models;
using DataSystemAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DataSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : Controller
    {
        private readonly ITarefaRepository _tarefaRepository;
        public TarefasController(ITarefaRepository tarefaRepository) 
        { 
            _tarefaRepository = tarefaRepository;
        }
        [HttpGet]
        [Route("TodasTarefas")]
        public async Task<IActionResult> TodasTarefas()
        {
            var tarefas = await _tarefaRepository.GetAllTarefasAsync();
            return tarefas.Any() ? Ok(tarefas) : NoContent();
        }

        [HttpGet]
        [Route("TarefaPorID/{id}")]
        public async Task<IActionResult> TarefasPorID(int id)
        {
            var tarefas = await _tarefaRepository.GetTarefaByIdAsync(id);
            return tarefas != null
            ? Ok(tarefas)
            : NotFound("Tarefa não encontrada");
        }

        [HttpPost]
        [Route("NovaTarefas")]
        public async Task<IActionResult> AdicionaTarefa([FromBody] TarefaResponse tarefa)
        {
            
            DateTime DataCriacao = DateTime.Now;
            if (tarefa.DataConclusao == null || tarefa.DataConclusao == DateTime.MinValue) return BadRequest("Data de Conclusão é obrigatória!");
            if (tarefa.DataConclusao < DataCriacao) return BadRequest("Data de Conclusão Não pode ser anterior a data de criação");
            if(tarefa.Titulo.Length>100) return BadRequest("Tarefa não pode ter mais de 100 caracteres!");
            if(String.IsNullOrEmpty(tarefa.Titulo)) return BadRequest("O campo Tarefa é obrigatório");
            if (!Enum.IsDefined(typeof(Status), tarefa.status)) return BadRequest("Status Inválido");

            TarefaRequest NovaTarefa = new TarefaRequest()
            {
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                DataCriacao = DataCriacao,
                DataConclusao = tarefa.DataConclusao,
                status = tarefa.status
            };
            var adicionado = await _tarefaRepository.AddTarefaAsync(NovaTarefa);
            return adicionado
            ? Ok("Tarefa adicionada com sucesso!")
            : BadRequest("Erro ao adicionar Tarefa");

        }
        [HttpPut]
        [Route("AlteraTarefa")]
        public async Task<IActionResult> AlteraTarefa(TarefaResponse TarefaAlterada, int Id)
        {
            if (Id <= 0) return BadRequest("Tarefa Inválida");
            var TarefaAtual = await _tarefaRepository.GetTarefaByIdAsync(Id);
            if (TarefaAtual == null) return NotFound("Tarefa não Encontrada");
            if (string.IsNullOrEmpty(TarefaAlterada.Titulo)) TarefaAlterada.Titulo = TarefaAtual.Titulo;
            if (string.IsNullOrEmpty(TarefaAlterada.Descricao)) TarefaAlterada.Descricao = TarefaAtual.Descricao;
            if (TarefaAlterada.DataConclusao==null || TarefaAlterada.DataConclusao == DateTime.MinValue) TarefaAlterada.DataConclusao = TarefaAtual.DataConclusao;
            if(TarefaAlterada.DataConclusao< TarefaAtual.DataCriacao) return BadRequest("Data de Conclusão Não pode ser anterior a data de criação");
            if (TarefaAlterada.status == null) TarefaAlterada.status = TarefaAtual.status;
            if (!Enum.IsDefined(typeof(Status), TarefaAlterada.status)) return BadRequest("Status Inválido");

            var atualizado = await _tarefaRepository.UpdateTarefaAsync(TarefaAlterada, Id);
            return atualizado
            ? Ok("Tarefa atualizada com sucesso")
            : BadRequest("Erro ao atualizar Tarefa");

        }
        [HttpDelete]
        [Route("DeletaTarefaPorID/{id}")]
        public async Task<IActionResult> DeletaTarefaPorID(int id)
        {
            if (id <= 0) return BadRequest("Tarefa Inválida");
            var TarefaAtual = await _tarefaRepository.GetTarefaByIdAsync(id);
            if (TarefaAtual == null) return NotFound("Tarefa não Encontrada");
            var deletado = await _tarefaRepository.DeleteTarefaByIDAsync(id);
            return deletado
            ? Ok("Tarefa deletada com sucesso")
            : BadRequest("Erro ao deletar Tarefa");
        }
    }
}
