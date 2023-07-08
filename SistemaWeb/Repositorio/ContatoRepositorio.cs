using SistemaContatos.Repositorio;
using SistemaWeb.Data;
using SistemaWeb.Models;

namespace SistemaContatos.Repository
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public List<ContatoModel> BuscarTodos()
        {
            return _bancoContext.Contatos.ToList();
        }
        public ContatoModel ListarPorId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.id == id);
        }
        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.id);

            if (contatoDB == null) throw new System.Exception("Houve um erro ao atualizar o contato!");

            contatoDB.nome = contato.nome;
            contatoDB.email = contato.email;
            contatoDB.telefone = contato.telefone;

            _bancoContext.Contatos.Update(contatoDB);
            _bancoContext.SaveChanges();

            return contatoDB;
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDB = ListarPorId(id);

            if (contatoDB == null) throw new System.Exception("Houve um erro ao excluir o contato!");

            _bancoContext.Remove(contatoDB);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}
