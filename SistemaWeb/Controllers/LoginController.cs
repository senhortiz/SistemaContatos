using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Models;
using SistemaContatos.Repositorio;

namespace SistemaContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        
                    }

                    TempData["MensagemErro"] = $"Login ou Senha inválidos, tente novamente!";
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possivel realizar seu login, tente novamente! {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
