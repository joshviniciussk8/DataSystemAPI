using System.ComponentModel.DataAnnotations;

namespace DataSystemAPI.Models
{
    public class TarefaResponse
    {
        
        [MaxLength(100, ErrorMessage ="Titulo deve ter no máximo 100 caracteres")]
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataConclusao { get; set; }
        public Status status { get; set; }
    }
    public enum Status
    {
        Pendente,
        EmProgresso,
        Concluido
    }
}
