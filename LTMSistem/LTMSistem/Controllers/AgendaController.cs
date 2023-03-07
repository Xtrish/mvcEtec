using LTMSistem.Filters;
using LTMSistem.Models;
using LTMSistem.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace LTMSistem.Controllers
{
    [PaginaParaUsuarioLogado]
    public class AgendaController : Controller
    {

        private readonly IConsultaRepositorio _consultaRepositorio;

        public AgendaController(IConsultaRepositorio consultaRepositorio)
        {
            _consultaRepositorio = consultaRepositorio;
        }

        public IActionResult Index()
        {

            List<ConsultaModel> consultas = _consultaRepositorio.ConsultasDoDia(DateTime.Today);

         
            

            return View(consultas);
        }


        public IActionResult MostraAgenda(string data)
        {
            DateTime date;
            if (data is null || !data.Any())
            {
                date = DateTime.Now;

            }
            else { date = DateTime.Parse(data); }


            List<ConsultaModel> consultas = _consultaRepositorio.ConsultasDoDia(date);

          
            List<UsuarioModel>Dentistas = _consultaRepositorio.ListaDentistas();
            List<PacienteModel>Pacientes = _consultaRepositorio.ListaPacientes();

            AgendaGerenicModel agenda = new AgendaGerenicModel();

            agenda.Consultas = consultas;
            agenda.Dentistas = Dentistas;
            agenda.Pacientes = Pacientes;
            agenda.DataDia = date;
            return View(agenda);
        }







    }
}
