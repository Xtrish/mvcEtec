using Microsoft.AspNetCore.Mvc;

namespace LTMSistem.Models
{
    public class UsuarioGenericModel
    {
        public List<UsuarioModel> Usuarios { get; set; }
        public UsuarioModel Usuario { get; set; }
        [FromQuery]
        public string Nome { get; set; }
        [FromQuery]
        public string Email { get; set; }
    }
}
