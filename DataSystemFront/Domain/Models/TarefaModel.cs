using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DataSystemFront.Domain.Models
{
    public class TarefaModel
    {
        
        public int Id { get; set; }
        
        [MaxLength(100, ErrorMessage = "Titulo deve ter no máximo 100 caracteres")]
        [Required(ErrorMessage ="Titulo é obrigatório")]
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataConclusao { get; set; }
        public Status status { get; set; }
        public IEnumerable<SelectListItem>? StatusList { get; set; }
    }
    public enum Status
    {
        Pendente,
        EmProgresso,
        Concluido
    }
}
