using LTMSistem.DATA;
using LTMSistem.Helper;
using LTMSistem.Models;

namespace LTMSistem.Repositorio
{
    public class PacienteRepositorio : IPacienteRepositorio
    {
    private readonly BancoContext _bancoContext;
        private readonly ISessao _sessao;
        public PacienteRepositorio(BancoContext bancoContext,ISessao sessao)
        {
            _bancoContext=bancoContext;
            _sessao=sessao;
        }
        public PacienteModel Adicionar(PacienteModel paciente)
        {
            paciente.DataDeCadastro = DateTime.Now;
            _bancoContext.Pacientes.Add(paciente);
            _bancoContext.SaveChanges();
            return paciente;
        }

        public bool Apagar(long id)
        {
            PacienteModel paciente = _bancoContext.Pacientes.FirstOrDefault(x => x.Id == id);
            if (paciente == null) throw new Exception("Houve um erro ao apagar cadastro");
            _bancoContext.Pacientes.Remove(paciente);
            _bancoContext.SaveChanges();
            return true;
        }

        public PacienteModel Atualizar(PacienteModel paciente)
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            PacienteModel pacienteDB = BuscarPorId(paciente.Id);
            if (pacienteDB == null) throw new Exception("Houve um erro na atualização do cadastro");
            pacienteDB.IdUsuarioAlteracao = usuarioLogado.Id;
            pacienteDB.Nome=paciente.Nome;
            pacienteDB.Nascimento = paciente.Nascimento;
            pacienteDB.Email = paciente.Email;
            pacienteDB.Telefone = paciente.Telefone;
            pacienteDB.Rua=paciente.Rua;
            pacienteDB.Bairro = paciente.Bairro;
            pacienteDB.Numero = paciente.Numero;
            pacienteDB.Complemento = paciente.Complemento;
            pacienteDB.Cidade = paciente.Cidade;
            pacienteDB.Estado=paciente.Estado;
            pacienteDB.Observacao = paciente.Observacao;
            pacienteDB.DataAlteração = DateTime.Now;

            _bancoContext.Pacientes.Update(pacienteDB);
            _bancoContext.SaveChanges();
            return pacienteDB;
        }

        public PacienteModel BuscarPorId(long id)
        {
            PacienteModel paciente = _bancoContext.Pacientes.FirstOrDefault(x => x.Id == id);
            return paciente;
        }

        public List<PacienteModel> BuscarTodos()
        {
            return _bancoContext.Pacientes.ToList();
        }
        public List<PacienteModel> Pesquisar(string nome, string email)
        {
            return _bancoContext.Pacientes
                .Where(x =>
                (string.IsNullOrWhiteSpace(nome) || x.Nome.ToUpper().Contains(nome.ToUpper()))
                && (string.IsNullOrWhiteSpace(email) || x.Email.ToUpper() == email.ToUpper()))
                .ToList();
        }
    }
}
