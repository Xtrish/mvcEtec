using LTMSistem.Filters;
using LTMSistem.Models;
using LTMSistem.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using System.Globalization;

namespace LTMSistem.Controllers
{
    [PaginaRestritaSomenteSecretaria]
    public class ConsultaController : Controller
    {

        private readonly IConsultaRepositorio _consultaRepositorio;

        public ConsultaController(IConsultaRepositorio consultaRepositorio)
        {
            _consultaRepositorio = consultaRepositorio;
        }

        public IActionResult Index()
        {

            List<ConsultaModel> consultas = _consultaRepositorio.ConsultasDoDia(DateTime.Today);

            ConsultaGerenicModel consultasDB = new ConsultaGerenicModel();
            consultasDB.Consultas = consultas;
            return View(consultasDB);
        }

        public IActionResult Criar()
        {
            ConsultaModel consultaModel = new ConsultaModel();
            consultaModel.Dentistas = _consultaRepositorio.ListaDentistas();
            consultaModel.Pacientes = _consultaRepositorio.ListaPacientes();



            return View(consultaModel);
        }
        public IActionResult Mostrar(long id)
        {
            ConsultaModel consulta = _consultaRepositorio.MostraConsulta(id);
            return View(consulta);
        }
        public IActionResult Editar(long id)
        {
            ConsultaModel consulta = _consultaRepositorio.MostraConsulta(id);
            consulta.Dentistas = _consultaRepositorio.ListaDentistas();
            consulta.Pacientes = _consultaRepositorio.ListaPacientes();

            return View(consulta);
        }



        public IActionResult ApagarConfirmacao(long id)
        {
            ConsultaModel consulta = _consultaRepositorio.MostraConsulta(id);

            return View(consulta);

        }
    

        [HttpPost]
        public IActionResult Criar(ConsultaModel consulta)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    if (consulta.DataConsulta >= DateTime.Today)
                    {



                        _consultaRepositorio.CalculaTempoConsulta(consulta, out var duracaoConsulta);

                        consulta.DataFinal = consulta.DataConsulta + duracaoConsulta;
                        if (_consultaRepositorio.Adicionar(consulta))
                        {
                            TempData["MensagemSucesso"] = "Consulta cadastrada com sucesso";
                            return RedirectToAction("Index", "Consulta");
                        }
                    }
                    if (consulta.DataConsulta < DateTime.Now.Date)
                    {
                        TempData["MensagemErro"] = $"Ops, data menor que data atual";
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Ops, Horário não disponivel";

                    }

                    consulta.Dentistas = _consultaRepositorio.ListaDentistas();
                    consulta.Pacientes = _consultaRepositorio.ListaPacientes();
                    return View("Criar", consulta);

                }
                consulta.Dentistas = _consultaRepositorio.ListaDentistas();
                consulta.Pacientes = _consultaRepositorio.ListaPacientes();
                return View("Criar", consulta);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, erro ao cadastrar o paciente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");

            }

        }
        [HttpPost]
        public IActionResult Alterar(ConsultaModel consulta)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (consulta.DataConsulta < DateTime.Now) throw new Exception("Data menor que data atual");
                    _consultaRepositorio.CalculaTempoConsulta(consulta, out var duracaoConsulta);

                    consulta.DataFinal = consulta.DataConsulta + duracaoConsulta;
                    if (_consultaRepositorio.Alterar(consulta))
                    {
                        TempData["MensagemSucesso"] = "Consulta Alterarda com sucesso";
                        return RedirectToAction("Index", "Consulta");

                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Ops, erro ao alterar consulta";

                    }

                    consulta.Dentistas = _consultaRepositorio.ListaDentistas();
                    consulta.Pacientes = _consultaRepositorio.ListaPacientes();
                    return View("Editar", consulta);
                }
                return View("Editar", consulta);

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, erro ao alterar a consulta, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");

            }
        }

        public IActionResult Apagar(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool apagado = _consultaRepositorio.Apagar(id);
                    if (apagado)
                    {
                        TempData["MensagemSucesso"] = "Consulta deletada com sucesso";
                        return RedirectToAction("Index");
                    }
                    return View();
                }


                return RedirectToAction("Index");

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, erro ao deletar a consulta, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        public IActionResult Concluir(long id)
        {
            try
            {
               
                    bool concluida = _consultaRepositorio.ConcluirConsulta(id);
                    if (concluida)
                    {
                        TempData["MensagemSucesso"] = "Consulta concluida com sucesso";
                        return RedirectToAction("Index");
                    }

                    
                
                return RedirectToAction("Index");

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, erro ao concluir a consulta, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }




    }
}
