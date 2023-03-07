using System.ComponentModel.DataAnnotations;

namespace LTMSistem.Models
{
    public class LoginModel
    {
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+.[a-zA-Z]{2,6}$", ErrorMessage = "Email Inválido")]
        [EmailAddress(ErrorMessage = "Email Inválido")]
        [Required(ErrorMessage = "Email Obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha Obrigatória")]
        public string Senha { get; set; }
    }
}
