using LTMSistem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LTMSistem.Repositorio
{
    public interface IConsultaRepositorio
    {
        List<UsuarioModel> ListaDentistas();

        List<PacienteModel> ListaPacientes();

        bool Adicionar(ConsultaModel consulta);

        List<ConsultaModel> ConsultasDoDia(DateTime data);
        ConsultaModel MostraConsulta(long id);

        bool CalculaTempoConsulta(ConsultaModel consulta, out TimeSpan duracaoConsulta);
        bool Alterar(ConsultaModel consulta);
        bool ConcluirConsulta(long id);
        bool Apagar (long id);


    }
}