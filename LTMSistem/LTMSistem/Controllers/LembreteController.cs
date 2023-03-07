using LTMSistem.Helper;
using LTMSistem.Models;
using LTMSistem.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace LTMSistem.Controllers
{
    public class LembreteController : Controller
    {

        private readonly ILembreteRepositorio _lembreteRepositorio;
        private readonly ISessao _sessao;
        public LembreteController(ILembreteRepositorio lembreteRepositorio,ISessao sessao)
        {
            _lembreteRepositorio = lembreteRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            UsuarioModel usuarioLogado= _sessao.BuscarSessaoDoUsuario();
            List<LembreteModel> lembretes = _lembreteRepositorio.BuscarTodosDoDia(usuarioLogado.Id);
            return View(lembretes);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(long id)
        {
            LembreteModel lembrete = _lembreteRepositorio.BuscarPorId(id);
            return View(lembrete);
        }
        public IActionResult Mostrar(long id)
        {
            LembreteModel lembrete = _lembreteRepositorio.BuscarPorId(id);
            return View(lembrete);
        }
        public IActionResult ApagarConfirmacao(long id)
        {
            LembreteModel lembrete = _lembreteRepositorio.BuscarPorId(id);

            return View(lembrete);
        }

        public IActionResult ConcluirLembrete(long id)
        {
            try
            {
             bool concluido=   _lembreteRepositorio.Concluir(id);
                if (concluido)
                {
                    TempData["MensagemSucesso"] = "Lembrete concluido com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = $"Erro ao concluir o lembrete, tente novamente";

                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, erro ao concluir o lembrete, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }



        }
        public IActionResult Apagar(long Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool apagado = _lembreteRepositorio.Apagar(Id);
                    if (apagado)
                    {
                        TempData["MensagemSucesso"] = "Lembrete deletado com sucesso";
                        return RedirectToAction("Index");
                    }
                    return View();
                }
                return RedirectToAction("Index");

            }
            catch (Exception erro)
            {

                throw;
            }
        }
        [HttpPost]
        public IActionResult Criar(LembreteModel lembrete)
        {
            try
            {
                if (lembrete.DataDoLembrete.Date < DateTime.Now.Date) throw new Exception("Data não pode ser inferior a data atual");
                UsuarioModel usuariologado = _sessao.BuscarSessaoDoUsuario();
                lembrete.UsuarioId = usuariologado.Id;
                if (ModelState.IsValid)
                {
                    
                    _lembreteRepositorio.Adicionar(lembrete);
                    TempData["MensagemSucesso"] = "Lembrete cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(lembrete);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, erro ao cadastrar o lembrete, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");

            }

        }
        public IActionResult Alterar(LembreteModel lembrete)
        {
            try
            {
                if (lembrete.DataDoLembrete.Date < DateTime.Now.Date) throw new Exception("Data não pode ser inferior a data atual");
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    _lembreteRepositorio.Atualizar(lembrete, usuarioLogado.Id);
                    TempData["MensagemSucesso"] = "Lembrete alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {


                TempData["MensagemErro"] = $"Ops, erro ao alterar o lembrete, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}

