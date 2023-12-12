using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            producto.Productos = new List<object>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52796/api/");
                var taskResponse = client.GetAsync("producto");
                taskResponse.Wait();
                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach(var item in readTask.Result.Objects)
                    {
                        ML.Producto produtoResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(item.ToString());
                        producto.Productos.Add(produtoResult);
                    }
                }
            }
            return View(producto);
        }
        [HttpGet]
        public ActionResult Form(int? idProducto)
        {
            ML.Producto producto = new ML.Producto();
            if(idProducto != null)
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:52796/api/");
                    var taskResponse = client.GetAsync($"producto/{idProducto}");
                    taskResponse.Wait();
                    var resultService = taskResponse.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        var readTask = resultService.Content.ReadAsAsync<ML.Result>(); 
                        readTask.Wait();
                        producto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(readTask.Result.Object.ToString());
                    }
                }
            }
            else
            {
                producto.Marca = new ML.Marca();
            }
            producto.Marca.Marcas = BL.Marca.GetAll();
            return View(producto);
        }
        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            if(producto.IdProducto == 0)
            {
                using(var client = new HttpClient())
                {
                    ML.Result result = new ML.Result();
                    client.BaseAddress = new Uri("http://localhost:52796/api/");
                    var taskResponse = client.PostAsJsonAsync("producto", producto);
                    taskResponse.Wait();
                    var resultService = taskResponse.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result = readTask.Result;
                    }
                    ViewBag.Mensaje = result.Message;
                }
            }
            else
            {
                using(var client = new HttpClient())
                {
                    ML.Result result = new ML.Result();
                    client.BaseAddress = new Uri("http://localhost:52796/api/");
                    var taskResponse = client.PutAsJsonAsync($"producto/{producto.IdProducto}", producto);
                    taskResponse.Wait();
                    var resultService = taskResponse.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result = readTask.Result;
                    }
                    ViewBag.Mensaje = result.Message;
                }
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int idProducto)
        {
            using(var client = new HttpClient())
            {
                ML.Result result = new ML.Result();
                client.BaseAddress = new Uri("http://localhost:52796/api/");
                var taskResponse = client.DeleteAsync($"producto/{idProducto}");
                taskResponse.Wait();
                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    result = readTask.Result;
                }
                ViewBag.Mensaje = result.Message;
            }
            return PartialView("Modal");
        }
    }
}