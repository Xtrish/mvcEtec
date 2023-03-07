using LTMSistem.DATA;
using LTMSistem.Helper;
using LTMSistem.Models;
using Microsoft.EntityFrameworkCore;

namespace LTMSistem.Repositorio
{
    public class LembreteRepositorio : ILembreteRepositorio
    {
        private readonly BancoContext _bancoContext;
        private readonly ISessao _sessao;
        public LembreteRepositorio(BancoContext bancoContext, ISessao sessao)
        {
            _bancoContext = bancoContext;
            _sessao = sessao;
        }
        public LembreteModel Adicionar(LembreteModel lembreteModel)
        {           
            
            lembreteModel.DataDeCriacao = DateTime.Now;
            lembreteModel.Status = false;
            _bancoContext.Lembretes.Add(lembreteModel);
            _bancoContext.SaveChanges();
            return lembreteModel;
        }

        public bool Apagar(long id)
        {
            LembreteModel lembrete = _bancoContext.Lembretes.FirstOrDefault(x => x.Id == id);
            if (lembrete == null) throw new Exception("Houve um erro ao apagar cadastro");
            _bancoContext.Lembretes.Remove(lembrete);
            _bancoContext.SaveChanges();
            return true;
        }

        public LembreteModel Atualizar(LembreteModel lembreteModel, long usuarioId)
        {           
            LembreteModel lembreteDB = BuscarPorId(lembreteModel.Id);
            if (lembreteDB == null) throw new Exception("Houve um erro na atualização do lembrete");
            lembreteDB.UsuarioId = usuarioId;
            lembreteDB.Assunto = lembreteModel.Assunto;
            lembreteDB.Descricao = lembreteModel.Descricao;
            lembreteDB.Resumo = lembreteModel.Resumo;
            lembreteDB.DataDoLembrete = lembreteModel.DataDoLembrete;

            _bancoContext.Lembretes.Update(lembreteDB);
            _bancoContext.SaveChanges();
            return lembreteDB;
        }

        public LembreteModel BuscarPorId(long id)
        {
            LembreteModel lembrete = _bancoContext.Lembretes.FirstOrDefault(x => x.Id == id);
            return lembrete;
        }

        public List<LembreteModel> BuscarTodos(long usuarioId)
        {
            return _bancoContext.Lembretes.Where
                (x => (x.UsuarioId == usuarioId)&&(x.DataDoLembrete.Date<=DateTime.Now)&&(x.Status==false))
                .OrderBy(x=> x.DataDoLembrete).ToList();
        }

        public List<LembreteModel> BuscarTodosDoDia( long usuarioId)
        {

            return _bancoContext.Lembretes.Where(x => (x.UsuarioId == usuarioId) && (x.DataDoLembrete.Date <= DateTime.Now) && (x.Status == false))
                .OrderBy(x => x.DataDoLembrete).ToList();
        }

        public bool Concluir(long id)
        {
            LembreteModel lembrete = BuscarPorId(id);
            if (lembrete == null) throw new Exception("Houve  um erro, não conseguimos concluir este lembrete");
            lembrete.Status = true;
            _bancoContext.Lembretes.Update(lembrete);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
