using LTMSistem.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LTMSistem.Controllers
{
    public class RestritoController : Controller
    {
        [PaginaParaUsuarioLogado]
        public IActionResult Index()
        {
            return View();
        }
    }
}
