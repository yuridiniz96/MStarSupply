using Microsoft.AspNetCore.Mvc;
using MStarSupply.Data.Repositories;
using MStarSupply.Models;
using Newtonsoft.Json;

namespace MStarSupply.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmailAndSenha(model.Email, model.Senha);

                    if (usuario != null)
                    {
                        return RedirectToAction("Dashboard", "Dashboard");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Acesso negado. Usuário inválido.";
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha: {e.Message}";
                }
            }

            return View();
        }

    }
}
