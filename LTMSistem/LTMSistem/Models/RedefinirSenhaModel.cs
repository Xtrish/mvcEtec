using System.ComponentModel.DataAnnotations;

namespace LTMSistem.Models
{
    public class RedefinirSenhaModel
    {

        [Required(ErrorMessage = "Email Obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Nome Obrigatório")]
        public string Nome { get; set; }
    }
}
