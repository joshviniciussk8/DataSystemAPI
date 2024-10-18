using System.ComponentModel.DataAnnotations;

namespace DataSystemAPI.Models
{
    public class TarefaRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Titulo deve ter no máximo 100 caracteres")]
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataConclusao { get; set; }
        public Status status { get; set; }

    }
   
}
