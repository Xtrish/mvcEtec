using LTMSistem.Models;

namespace LTMSistem.Repositorio
{
    public interface ILembreteRepositorio
    {
        LembreteModel BuscarPorId(long id);
        List<LembreteModel> BuscarTodos(long usuarioId);
        List<LembreteModel> BuscarTodosDoDia( long usuarioId);
        LembreteModel Adicionar(LembreteModel paciente);
        LembreteModel Atualizar(LembreteModel paciente, long usuarioId);
        bool Apagar(long id);
        bool Concluir(long id);
    }
}
