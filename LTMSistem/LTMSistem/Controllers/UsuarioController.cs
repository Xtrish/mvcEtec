using LTMSistem.Filters;
using LTMSistem.Models;
using LTMSistem.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace LTMSistem.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public IActionResult Home()
        {
            
            return View();
        }

        [AutoValidateAntiforgeryToken]
        public IActionResult Index(UsuarioGenericModel usuarioGenericModel)
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.Pesquisar(usuarioGenericModel.Nome,usuarioGenericModel.Email);
            UsuarioGenericModel model = new UsuarioGenericModel();
            model.Usuarios=usuarios;
            return View(model);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(long id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }
        public IActionResult Mostrar(long id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }
        public IActionResult ApagarConfirmacao(long id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);

            return View(usuario);
        }
        public IActionResult Apagar(long Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool apagado = _usuarioRepositorio.Apagar(Id);
                    if (apagado)
                    {
                        TempData["MensagemSucesso"] = "Usuário deletado com sucesso";
                        return RedirectToAction("Index");
                    }
                    return View();
                }
                return RedirectToAction("Index");

            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, erro ao deletar o usuário, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (!_usuarioRepositorio.VerificaDisponibilidadeDoEmail(usuario.Email)) throw new Exception("Email Já cadastrado");

                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, erro ao cadastrar o usuário, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");

            }

        }
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Nascimento = usuarioSemSenhaModel.Nascimento,
                        Email = usuarioSemSenhaModel.Email,
                        Telefone = usuarioSemSenhaModel.Telefone,
                        Rua = usuarioSemSenhaModel.Rua,
                        Bairro = usuarioSemSenhaModel.Bairro,
                        Numero = usuarioSemSenhaModel.Numero,
                        Complemento = usuarioSemSenhaModel.Complemento,
                        Cidade = usuarioSemSenhaModel.Cidade,
                        Estado = usuarioSemSenhaModel.Estado,
                        Perfil = usuarioSemSenhaModel.Perfil                       
                    };
                   usuario= _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso";
                    return RedirectToAction("Index");
                }

                UsuarioModel user = new UsuarioModel()
                {
                    Id = usuarioSemSenhaModel.Id,
                    Nome = usuarioSemSenhaModel.Nome,
                    Nascimento = usuarioSemSenhaModel.Nascimento,
                    Email = usuarioSemSenhaModel.Email,
                    Telefone = usuarioSemSenhaModel.Telefone,
                    Rua = usuarioSemSenhaModel.Rua,
                    Bairro = usuarioSemSenhaModel.Bairro,
                    Numero = usuarioSemSenhaModel.Numero,
                    Complemento = usuarioSemSenhaModel.Complemento,
                    Cidade = usuarioSemSenhaModel.Cidade,
                    Estado = usuarioSemSenhaModel.Estado,
                    Perfil = usuarioSemSenhaModel.Perfil
                };
                
                return View("Editar", user);
            }
            catch (Exception erro)
            {


                TempData["MensagemErro"] = $"Ops, erro ao alterar o usuário, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
