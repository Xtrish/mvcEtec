using LTMSistem.DATA;
using LTMSistem.Enums;
using LTMSistem.Enuns;
using LTMSistem.Helper;
using LTMSistem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LTMSistem.Repositorio
{
    public class ConsultaRepositorio : IConsultaRepositorio
    {
        private readonly BancoContext _bancoContext;
        private readonly ISessao _sessao;

        public ConsultaRepositorio(BancoContext bancoContext, ISessao sessao)
        {
            _bancoContext = bancoContext;
            _sessao = sessao;
        }

        public bool Adicionar(ConsultaModel consulta)
        {

            CalculaTempoConsulta(consulta, out var duracaoConsulta);            
            DateTime temp = consulta.DataConsulta.Date + duracaoConsulta;

            if (VerificaDisponibilidade(consulta))
            {
                _bancoContext.Consultas.Add(consulta);
                _bancoContext.SaveChanges();
                return true;
            }
           
            return false;

        }





        public List<ConsultaModel> ConsultasDoDia(DateTime data)
        {
         UsuarioModel usuario=  _sessao.BuscarSessaoDoUsuario();

            if (usuario.Perfil == LTMSistem.Enuns.PerfilEnum.Administrador || usuario.Perfil == LTMSistem.Enuns.PerfilEnum.Secretaria)
            {

                List<ConsultaModel> consultas = _bancoContext.Consultas
                .Where(x => (x.DataConsulta.Date == data.Date) && (x.Status == false))
                .Include(x => x.Dentista)
                .Include(x => x.Paciente)
                .OrderBy(x => x.DataConsulta)
                .ToList();

                return consultas;
            }
            else
            {
                List<ConsultaModel> consultas = _bancoContext.Consultas
                               .Where(x => (x.DataConsulta.Date == data.Date) && (x.Status == false)&& (x.DentistaId==usuario.Id))
                               .Include(x => x.Dentista)
                               .Include(x => x.Paciente)
                               .OrderBy(x => x.DataConsulta)
                               .ToList();

                return consultas;
            }
        }

        public List<UsuarioModel> ListaDentistas()
        {
            List<UsuarioModel> dentistas = _bancoContext.Usuarios.Where(x => x.Perfil == PerfilEnum.Dentista).ToList();

            if (dentistas is null)
            {
                throw new Exception("Não há dentistas cadastrados");
            }

            ConsultaModel consulta = new ConsultaModel();
            consulta.Dentistas = dentistas;
            return dentistas;
        }

        public List<PacienteModel> ListaPacientes()
        {
            List<PacienteModel> pacientes = _bancoContext.Pacientes.ToList();
            return pacientes;
        }
        public ConsultaModel MostraConsulta(long id)
        {
            ConsultaModel consulta = _bancoContext.Consultas
                .Where(x => x.Id == id)
                .Include(x => x.Dentista)
                .Include(x => x.Paciente)
                .SingleOrDefault();

            return consulta;
        }

        public bool ConcluirConsulta(long id)
        {
            ConsultaModel consulta = _bancoContext.Consultas.Where(consulta => consulta.Id == id).SingleOrDefault();
            if (consulta == null) throw new Exception("Houve um erro ao apagar a consulta");
            if(consulta.Status == true) throw new Exception("Não é possivel apagar a consulta,");
            _bancoContext.Consultas.Remove(consulta);
            _bancoContext.SaveChanges();
            return true;
        }

        public bool Alterar(ConsultaModel consulta)
        {

            if (VerificaDisponibilidadeDeConsulta(consulta)==false) throw new Exception("Horario Não disponivel"); ;

            ConsultaModel consultaDB = MostraConsulta(consulta.Id);
            if (consultaDB == null) throw new Exception("Consulta não encontrada");


            consultaDB.DentistaId = consulta.DentistaId;
            consultaDB.DataConsulta = consulta.DataConsulta;
            consultaDB.DataFinal = consulta.DataFinal;
            consultaDB.DuracaoConsulta = consulta.DuracaoConsulta;
            consultaDB.PacienteId = consulta.PacienteId;
            consultaDB.Procedimento = consulta.Procedimento;
            consultaDB.Observacao = consulta.Observacao;
            _bancoContext.Consultas.Update(consultaDB);
            _bancoContext.SaveChanges();
            return true;



        }
        public bool Apagar(long id)
        {
            ConsultaModel consulta = _bancoContext.Consultas.FirstOrDefault(x => x.Id == id);
            if (consulta == null) throw new Exception("Houve um erro ao apagar cadastro");
            if (consulta.Status==true) throw new Exception("Consulta não pode ser apagada");
            _bancoContext.Consultas.Remove(consulta);
            _bancoContext.SaveChanges();
            return true;
        }

        public bool CalculaTempoConsulta(ConsultaModel consulta, out TimeSpan duracaoConsulta)
        {
            int min = consulta.DuracaoConsulta.Minute;
            string hor = consulta.DuracaoConsulta.Hour.ToString();
            TimeSpan horas = TimeSpan.ParseExact(hor, "%h",
                                                    null, TimeSpanStyles.None);
            duracaoConsulta = new TimeSpan(horas.Hours, min, 0);
            return duracaoConsulta != null;
        }

        public bool VerificaDisponibilidade(ConsultaModel consulta)
        {

            List<ConsultaModel> listaConsultas = _bancoContext.Consultas
                .Where(consultaDB => (consultaDB.DataConsulta <= consulta.DataFinal)
                && (consultaDB.DataFinal >= consulta.DataConsulta)&&(consultaDB.DentistaId==consulta.DentistaId)).ToList();
            if (listaConsultas.Any()) return false;
            return true;
        }

        public bool VerificaDisponibilidadeDeConsulta(ConsultaModel consulta)
        {

            List<ConsultaModel> listaConsultas = _bancoContext.Consultas
                .Where(consultaDB => (consultaDB.DataConsulta < consulta.DataFinal)
                && (consultaDB.DataFinal > consulta.DataConsulta)
                &&consultaDB.Id!=consulta.Id).ToList();
            if (listaConsultas.Any()) return false;
            return true;
        }

       
    }
}

