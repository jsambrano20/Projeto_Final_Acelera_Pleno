using AceleraPlenoTrabalhoFinal.Mvc.Data.Interface;
using AceleraPlenoTrabalhoFinal.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Configuration;

namespace AceleraPlenoTrabalhoFinal.Mvc.Controllers
{
    public class ClienteController : Controller
    {
        //private readonly string apiUrl = "https://localhost:7198/api/Cliente";
        private string apiUrl = ConfigurationManager.AppSettings["apiCliente"];
        public ClienteController()
        {
            //string apiUrl = "https://localhost:7198/";
        }
        
        /*private readonly IRepository<Cliente> _iRepository;

        public ClienteController(IRepository<Cliente> iRepository)
        {
            _iRepository = iRepository;
        }*/

        // GET: Cliente
        public async Task<ActionResult> Index()
        {
            string api = $"{apiUrl}/Listar";
            try
            {
                string token = GetToken();
                List<Cliente> listaCliente = new List<Cliente>();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    using (var response = httpClient.GetAsync(api).Result)
                    {
                        string apiResponse = response.Content.ReadAsStringAsync().Result;
                        listaCliente = JsonConvert.DeserializeObject<List<Cliente>>(apiResponse);
                    }
                }
                return View(listaCliente);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Detalhes(Guid id)
        {
            if (id == null)
            {
                return View();
            }

            string api = $"{apiUrl}/FiltrarPorId/{id}";
            try
            {
                string token = GetToken();
                Cliente cliente = new Cliente();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    using (var response = httpClient.GetAsync(api).Result)
                    {
                        string apiResponse = response.Content.ReadAsStringAsync().Result;
                        cliente = JsonConvert.DeserializeObject<Cliente>(apiResponse);
                    }
                }
                return View(cliente);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ViewResult Criar() => View();
        [HttpPost]
        public async Task<ActionResult> Criar(Cliente cliente)
        {
            string api = $"{apiUrl}/Incluir";
            string token = GetToken();
            Cliente clienteNovo = new Cliente();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
                using (var response = httpClient.PostAsync(api, content).Result)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    clienteNovo = JsonConvert.DeserializeObject<Cliente>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Editar(Guid id)
        {
            string api = $"{apiUrl}/FiltrarPoId/{id}";
            string token = GetToken();

            Cliente cliente = new Cliente();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = httpClient.GetAsync(api).Result)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    cliente = JsonConvert.DeserializeObject<Cliente>(apiResponse);
                }
            }
            return View(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(Cliente cliente)
        {
            string api = $"{apiUrl}/Atualizar/{cliente.Id}";

            string token = GetToken();

            Cliente clienteAtualizado = new Cliente();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
                using (var response = httpClient.PutAsync(api, content).Result)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    clienteAtualizado = JsonConvert.DeserializeObject<Cliente>(apiResponse);
                }
            }

            ViewBag.Result = "Sucesso";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Deletar(Guid id)
        {
            string api = $"{apiUrl}/Deletar/{id}";

            string token = GetToken();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = httpClient.DeleteAsync(api).Result)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                }
            }

            TempData["MsgSucesso"] = "Deletado com Sucesso";  //Transportar valor de MsgSucesso para função de alertify
            return RedirectToAction("Index");
        }


        public string GetToken()
        {
            Token tokenModel = new Token();
            tokenModel.clienteId = ConfigurationManager.AppSettings["clienteId"];
            tokenModel.clienteSecret = ConfigurationManager.AppSettings["clienteSecret"];
            string path = ConfigurationManager.AppSettings["apiToken"];
            string token = "";

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(tokenModel), Encoding.UTF8, "application/json");
                using (var response = httpClient.PostAsync(path, content).Result)
                {
                    token = response.Content.ReadAsStringAsync().Result;
                    return token;//JsonConvert.DeserializeObject<string>(token);
                }
            }
        }
    }
}