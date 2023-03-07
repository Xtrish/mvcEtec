using LTMSistem.Models;
using Microsoft.AspNetCore.Components.Web;

namespace LTMSistem.Repositorio
{
    public interface IPacienteRepositorio
    {
        List<PacienteModel> Pesquisar(string nome, string email);
       
        PacienteModel BuscarPorId(long id);
        List<PacienteModel> BuscarTodos();
        PacienteModel Adicionar(PacienteModel paciente);
        PacienteModel Atualizar(PacienteModel paciente);
        bool Apagar(long id);
    }
}
