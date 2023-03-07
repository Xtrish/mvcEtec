using LTMSistem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using LTMSistem.Helper;
using LTMSistem.Repositorio;

namespace LTMSistem.ViewComponents
{
    public class Menu : ViewComponent
    {
        private readonly ILembreteRepositorio _lembreteRepositorio;
        public Menu(ILembreteRepositorio lembreteRepositorio)
        {
            _lembreteRepositorio=lembreteRepositorio;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

            List<LembreteModel> lembretes = _lembreteRepositorio.BuscarTodos(usuario.Id);
            usuario.Lembretes=lembretes;

            return View(usuario);
        }
    }
}
