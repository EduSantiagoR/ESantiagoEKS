using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52796/api/usuario");
                var taskResponse = client.GetAsync($"?email={email}&password={password}");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll", "Producto");
                }
                else
                {
                    return View();
                }
            }
        }
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52796/api/");
                var taskResponse = client.PostAsJsonAsync("usuario", usuario);
                taskResponse.Wait();
                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Usuario");
                }
                else
                {
                    return View(usuario);
                }
            }
        }
    }
}