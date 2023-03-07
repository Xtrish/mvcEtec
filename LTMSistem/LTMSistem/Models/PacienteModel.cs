using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.IO.Pipes;

namespace LTMSistem.Models
{
    public class PacienteModel
    {
        public long Id { get; set; }
        [MinLength(11, ErrorMessage = "Nome deve ter no minimo 6 caracteres")]
        [Required(ErrorMessage ="Nome Obrigatório")]
        public string Nome { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+.[a-zA-Z]{2,6}$", ErrorMessage = "Email Inválido")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Data de nascimento Obrigatório")]
        public DateTime Nascimento  { get; set; }
        [MinLength(11,ErrorMessage = "Telefone deve ter no minimo 11 digitos")]
        [MaxLength(20,ErrorMessage = "Telefone deve ter no maxima 20 digitos")]
        [Required(ErrorMessage = "Telefone Obrigatório")]
        public string Telefone { get; set; }
        public string? Rua { get; set; }
        public string? Bairro { get; set; }
        public string? Complemento { get; set; }
       
        public int? Numero { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Observacao { get; set; }
        public DateTime? DataDeCadastro { get; set; }
        public long? IdUsuarioAlteracao { get; set; }

        public DateTime? DataAlteração { get; set; }

        public virtual List<ConsultaModel>? Consultas { get; set; }
    }
}
