using System.ComponentModel.DataAnnotations;

namespace LTMSistem.Models
{
    public class AlterarSenhaModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Digite a senha")]
        public string SenhaAtual { get; set; }
        [Required(ErrorMessage = "Digite a nova ")]
        public string NovaSenha { get; set; }
        [Required(ErrorMessage = "Confirme a nova senha ")]
        [Compare("NovaSenha", ErrorMessage = "Senha não confere com a nova senha")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
