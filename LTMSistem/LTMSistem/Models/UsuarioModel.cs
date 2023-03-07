using LTMSistem.Enuns;
using LTMSistem.Helper;
using System.ComponentModel.DataAnnotations;

namespace LTMSistem.Models
{
    public class UsuarioModel
    {
        public long Id { get; set; }
        [MinLength(11, ErrorMessage = "Nome deve ter no minimo 6 caracteres")]
        [Required(ErrorMessage = "Nome Obrigatório")]
        public string Nome { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+.[a-zA-Z]{2,6}$", ErrorMessage = "Email Inválido")]
        [EmailAddress(ErrorMessage = "Email Inválido")]
        [Required(ErrorMessage = "Email Obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Data de nascimento Obrigatória")]
        public DateTime Nascimento { get; set; }
        [MinLength(11, ErrorMessage = "Telefone deve ter no minimo 11 digitos")]
        [MaxLength(20, ErrorMessage = "Telefone deve ter no maxima 20 digitos")]
        [Required(ErrorMessage = "Telefone Obrigatório")]
        public string Telefone { get; set; }
        public string? Rua { get; set; }
        public string? Bairro { get; set; }
        public string? Complemento { get; set; }
        public int? Numero { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public long? IdUsuarioAlteracao { get; set; }
        public DateTime? DataAlteração { get; set; }
        [Required(ErrorMessage = "Pefil Obrigatório")]
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "Senha Obrigatória")]
        public string Senha { get; set; }

        public virtual List<LembreteModel>? Lembretes { get; set; }
        public virtual List<ConsultaModel>? Consultas { get; set; }
       

        public bool SenhaValida(string senha)
        {
          return  BCrypt.Net.BCrypt.Verify(senha, Senha);            

        }
        
        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }
        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }
        public string GerarNovaSenha()
        {
            string novaSenha= Guid.NewGuid().ToString().Substring(0,8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }
    }
}
