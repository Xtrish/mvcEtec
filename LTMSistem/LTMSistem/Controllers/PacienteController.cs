using LTMSistem.Filters;
using LTMSistem.Models;
using LTMSistem.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace LTMSistem.Controllers
{
    [PaginaParaUsuarioLogado]
    public class PacienteController : Controller
    {
        private readonly IPacienteRepositorio _pacienteRepositorio;
        public PacienteController(IPacienteRepositorio pacienteRepositorio)
        {
            _pacienteRepositorio = pacienteRepositorio;
        }

        public IActionResult Index(PacienteGerenicModel gerenicModel)
        {
            List<PacienteModel> pacientes = _pacienteRepositorio.Pesquisar(gerenicModel.Nome, gerenicModel.Email) ;
            PacienteGerenicModel model= new PacienteGerenicModel();
            model.Pacientes=pacientes ;

            return View(model);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(long id)
        {
            PacienteModel paciente = _pacienteRepositorio.BuscarPorId(id);
            return View(paciente);
        }
        public IActionResult Mostrar(long id)
        {
            PacienteModel paciente = _pacienteRepositorio.BuscarPorId(id);
            return View(paciente);
        }
        public IActionResult ApagarConfirmacao(long id)
        {
            PacienteModel paciente = _pacienteRepositorio.BuscarPorId(id);

            return View(paciente);
        }
        public IActionResult Apagar(long Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool apagado = _pacienteRepositorio.Apagar(Id);
                    if (apagado)
                    {
                        TempData["MensagemSucesso"] = "Paciente deletado com sucesso";
                        return RedirectToAction("Index");
                    }
                    return View();
                }
                return RedirectToAction("Index");

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, erro ao apagar o usuario, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Criar(PacienteModel paciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _pacienteRepositorio.Adicionar(paciente);
                    TempData["MensagemSucesso"] = "Paciente cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(paciente);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, erro ao cadastrar o paciente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");

            }

        }
        public IActionResult Alterar(PacienteModel paciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _pacienteRepositorio.Atualizar(paciente);
                    TempData["MensagemSucesso"] = "Paciente alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar",paciente);
            }
            catch (Exception erro)
            {


                TempData["MensagemErro"] = $"Ops, erro ao alterar o paciente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
