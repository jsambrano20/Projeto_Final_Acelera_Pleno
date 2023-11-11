using AceleraPlenoTrabalhoFinal.Mvc.Data.Interface;
using AceleraPlenoTrabalhoFinal.Mvc.Models;
using AceleraPlenoTrabalhoFinal.Mvc.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AceleraPlenoTrabalhoFinal.Mvc.Controllers
{
    public class PedidoController : Controller
    {
        private string urlApi = ConfigurationManager.AppSettings["apiPedido"];
        private string urlApiMesa = ConfigurationManager.AppSettings["apiMesa"];
        private string urlApiPrato = ConfigurationManager.AppSettings["apiPrato"];
        //private readonly IRepositoryPedido<Pedido> _iRepositoryPedido;
        //private readonly TokenService _tokenService;

        public PedidoController()
        {
        }

        /*public PedidoController(IRepositoryPedido<Pedido> iRepositoryPedido TokenService tokenService)
        {
            //_iRepositoryPedido = iRepositoryPedido;
            _tokenService = tokenService;
        }*/

        // GET: Pedido
        public async Task<ActionResult> Index()
        {
            try
            {
                string token = GetToken();

                List<Pedido> listaPedido = new List<Pedido>();
                List<Pedido> listaPedidoCompleto = new List<Pedido>();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    using (var response = httpClient.GetAsync(urlApi + "/Listar").Result)
                    {
                        string apiResponse = response.Content.ReadAsStringAsync().Result;
                        listaPedido = JsonConvert.DeserializeObject<List<Pedido>>(apiResponse);
                    }
                }

                foreach (var pedido in listaPedido)
                {
                    pedido.Mesa = RetornaDescricaoMesa(pedido.MesaId);
                    pedido.Prato = RetornaDescricaoPrato(pedido.PratoId);
                    listaPedidoCompleto.Add(pedido);
                }
                return View(listaPedidoCompleto);

                /*var pedidos = _iRepositoryPedido.Listar();
                return View(pedidos);*/
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

            string api = $"{urlApi}/FiltrarPorId/{id}";
            try
            {
                string token = GetToken();
                Pedido pedido = new Pedido();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    using (var response = httpClient.GetAsync(api).Result)
                    {
                        string apiResponse = response.Content.ReadAsStringAsync().Result;
                        pedido = JsonConvert.DeserializeObject<Pedido>(apiResponse);
                    }
                }
                return View(pedido);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<ActionResult> Preparando(Guid id)
        {
            Pedido pedido = FiltrarPorId(id);

            //pedido.StatusPedido = Models.Enuns.StatusPedido.Recebido;
            string token = GetToken();
            string api = $"{urlApi}/Preparando/{id}";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");
                using (var response = httpClient.PutAsync(api, content).Result)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    //pedido = JsonConvert.DeserializeObject<Pedido>(apiResponse);
                }
            }
            TempData["MsgSucesso"] = "Alterado com Sucesso";  //Transportar valor de MsgSucesso para função de alertify
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Disponivel(Guid id)
        {
            Pedido pedido = FiltrarPorId(id);

            //pedido.StatusPedido = Models.Enuns.StatusPedido.Recebido;
            string token = GetToken();
            string api = $"{urlApi}/Disponivel/{id}";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");
                using (var response = httpClient.PutAsync(api, content).Result)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    //pedido = JsonConvert.DeserializeObject<Pedido>(apiResponse);
                }
            }
            TempData["MsgSucesso"] = "Alterado com Sucesso";  //Transportar valor de MsgSucesso para função de alertify
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Cancelado(Guid id)
        {
            Pedido pedido = FiltrarPorId(id);

            //pedido.StatusPedido = Models.Enuns.StatusPedido.Recebido;
            string token = GetToken();
            string api = $"{urlApi}/Cancelado/{id}";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");
                using (var response = httpClient.PutAsync(api, content).Result)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    //pedido = JsonConvert.DeserializeObject<Pedido>(apiResponse);
                }
            }
            TempData["MsgSucesso"] = "Alterado com Sucesso";  //Transportar valor de MsgSucesso para função de alertify
            return RedirectToAction("Index");
        }

        public ViewResult Criar() => View();
        [HttpPost]
        public async Task<ActionResult> Criar(Pedido pedido)
        {
            pedido.StatusPedido = Models.Enuns.StatusPedido.Recebido;
            string token = GetToken();
            string api = $"{urlApi}/Incluir";

            Pedido pedidoNovo = new Pedido();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");
                using (var response = httpClient.PostAsync(api, content).Result)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    pedidoNovo = JsonConvert.DeserializeObject<Pedido>(apiResponse);
                }
            }
            return RedirectToAction("Criar");
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

        public Pedido FiltrarPorId(Guid id)
        {
            if (id == null)
            {
                return null;
            }

            string api = $"{urlApi}/FiltrarPorId/{id}";
            try
            {
                string token = GetToken();
                Pedido pedido = new Pedido();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    using (var response = httpClient.GetAsync(api).Result)
                    {
                        string apiResponse = response.Content.ReadAsStringAsync().Result;
                        pedido = JsonConvert.DeserializeObject<Pedido>(apiResponse);
                    }
                }
                return pedido;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public string RetornaDescricaoMesa(Guid id)
        {
            string api = $"{urlApiMesa}/FiltrarPorId/{id}";
            try
            {
                string token = GetToken();
                Mesa mesa = new Mesa();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    using (var response = httpClient.GetAsync(api).Result)
                    {
                        string apiResponse = response.Content.ReadAsStringAsync().Result;
                        mesa = JsonConvert.DeserializeObject<Mesa>(apiResponse);
                        return mesa.Descricao;

                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string RetornaDescricaoPrato(Guid id)
        {
            string api = $"{urlApiPrato}/FiltrarPorId/{id}";
            try
            {
                string token = GetToken();
                Prato prato = new Prato();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    using (var response = httpClient.GetAsync(api).Result)
                    {
                        string apiResponse = response.Content.ReadAsStringAsync().Result;
                        prato = JsonConvert.DeserializeObject<Prato>(apiResponse);
                        return prato.Titulo;

                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}