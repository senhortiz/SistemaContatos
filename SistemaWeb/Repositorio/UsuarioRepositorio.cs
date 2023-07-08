using Microsoft.EntityFrameworkCore;
using SistemaContatos.Models;
using SistemaContatos.Repositorio;
using SistemaWeb.Data;
using System.Data;

namespace SistemaContatos.Repository
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios.ToList();
        }
        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.login == login);
        }
        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.id == id);
        }
        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.id);

            if (usuarioDB == null)
            {
                throw new Exception("Houve um erro ao atualizar o usuário!");
            }

            usuarioDB.nome = usuario.nome;
            usuarioDB.email = usuario.email;
            usuarioDB.login = usuario.login;
            usuario.Perfil = usuario.Perfil;
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Entry(usuarioDB).State = EntityState.Modified;
            _bancoContext.SaveChanges();

            return usuarioDB;
        }


        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = ListarPorId(id);

            if (usuarioDB == null) throw new System.Exception("Houve um erro ao excluir o usuário!");

            _bancoContext.Remove(usuarioDB);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}
