using LTMSistem.Models;

namespace LTMSistem.Repositorio
{
    public interface IUsuarioRepositorio
    {
        List<UsuarioModel> Pesquisar(string nome, string email);
        UsuarioModel BuscarPorEmailENome(string email, string nome);
        UsuarioModel BuscarPorEmail (string email);
        UsuarioModel BuscarPorId(long id);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel Atualizar(UsuarioModel usuario);
        UsuarioModel AlterarSenha (AlterarSenhaModel alterarSenhaModel);
        bool Apagar(long id);
        bool VerificaDisponibilidadeDoEmail(string email);
    }
}
