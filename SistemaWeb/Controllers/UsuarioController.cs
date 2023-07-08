using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Models;
using SistemaContatos.Repositorio;
using SistemaContatos.Repository;
using SistemaWeb.Models;

namespace SistemaContatos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
            
        }
        public IActionResult Criar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possivel cadastrar o usuário, tente novamente! {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = _usuarioRepositorio.ListarPorId(usuarioSemSenhaModel.id);

                if (usuario == null)
                {
                    throw new Exception("Usuário não encontrado!");
                }

                usuario.nome = usuarioSemSenhaModel.nome;
                usuario.login = usuarioSemSenhaModel.login;
                usuario.email = usuarioSemSenhaModel.email;
                usuario.Perfil = usuarioSemSenhaModel.Perfil;

                _usuarioRepositorio.Atualizar(usuario);

                TempData["MensagemSucesso"] = "Usuário editado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível editar o usuário. Erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }


        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuário excluido com sucesso!";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possivel excluir o usuário, tente novamente! {erro.Message}";
                return RedirectToAction("Index");
            }

        }
        
    }
}
