using System.ComponentModel.DataAnnotations;

namespace LTMSistem.Models
{
    public class LembreteModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Assunto Obrigatório")]
        public string Assunto { get; set; }
        [Required(ErrorMessage = "Data Obrigatória")]

        public DateTime DataDoLembrete { get; set; }
        public string? Resumo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataDeCriacao { get; set; }
        public bool? Status { get; set; }
        public long UsuarioId { get; set; }
        public virtual UsuarioModel? Usuario { get; set; }
    }
}
