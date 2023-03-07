using LTMSistem.DATA;
using LTMSistem.Helper;
using LTMSistem.Models;
using Microsoft.EntityFrameworkCore;

namespace LTMSistem.Repositorio
{
    public class UsuarioRepositorio :IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        private readonly ISessao _sessao;
        
        public UsuarioRepositorio(BancoContext bancoContext,ISessao sessao)
        {
            _bancoContext = bancoContext;
            _sessao = sessao;
        }
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataDeCadastro = DateTime.Now;
            usuario.SetSenhaHash();
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

      

        public bool Apagar(long id)
        {
            UsuarioModel usuario = _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
            if (usuario == null) throw new Exception("Houve um erro ao apagar cadastro");
            _bancoContext.Usuarios.Remove(usuario);
            _bancoContext.SaveChanges();
            return true;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = BuscarPorId(usuario.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualização do cadastro");
            if (_sessao.BuscarSessaoDoUsuario() !=null)
            {
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                usuarioDB.IdUsuarioAlteracao = usuarioLogado.Id;
            }
            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Nascimento = usuario.Nascimento;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Telefone = usuario.Telefone;
            usuarioDB.Rua = usuario.Rua;
            usuarioDB.Bairro = usuario.Bairro;
            usuarioDB.Numero = usuario.Numero;
            usuarioDB.Complemento = usuario.Complemento;
            usuarioDB.Cidade = usuario.Cidade;
            usuarioDB.Estado = usuario.Estado;
            usuarioDB.DataAlteração = DateTime.Now;
            usuarioDB.Perfil = usuario.Perfil;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();
            return usuarioDB;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = BuscarPorId(alterarSenhaModel.Id);
            if (usuarioDB == null) throw new Exception("Houve um erro na atualização da senha, usuario não encontrado");
            if (!usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if (usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual!");

            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataAlteração = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        }

        public UsuarioModel BuscarPorEmail(string email)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper());
           
        }

        public UsuarioModel BuscarPorEmailENome(string email, string nome)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Nome.ToUpper()==nome.ToUpper());
        }

        public UsuarioModel BuscarPorId(long id)
        {
            UsuarioModel usuario = _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
            return usuario;
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios
                .Include(x =>x.Lembretes ).ToList();
        }
        public List<UsuarioModel> Pesquisar(string nome, string email)
        {
            return _bancoContext.Usuarios
                .Where(x =>
                (string.IsNullOrWhiteSpace(nome) || x.Nome.ToUpper().Contains(nome.ToUpper()))
                && (string.IsNullOrWhiteSpace(email) || x.Email.ToUpper() == email.ToUpper()))
                .ToList();
        }

        public bool VerificaDisponibilidadeDoEmail( string email)
        {
           var usuarios= _bancoContext.Usuarios.Where(u=>u.Email == email).ToList();
            if (usuarios.Any()) return false;
            return true;
        }
    }
}
