using AceleraPlenoTrabalhoFinal.Mvc.Models;
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
    public class PratoController : Controller
    {
        private string urlApi = ConfigurationManager.AppSettings["apiPrato"];

        public PratoController()
        {
        }

        // GET: Prato
        public async Task<ActionResult> Index()
        {
            try
            {
                string token = GetToken();

                List<Prato> listaPrato = new List<Prato>();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    using (var response = httpClient.GetAsync(urlApi + "/Listar").Result)
                    {
                        string apiResponse = response.Content.ReadAsStringAsync().Result;
                        listaPrato = JsonConvert.DeserializeObject<List<Prato>>(apiResponse);
                        listaPrato = listaPrato.FindAll(x => x.Status == true).ToList();
                    }
                }

                foreach (var imagem in listaPrato)
                {
                    imagem.ConverterBase64ParaJpg();
                }

                return View(listaPrato);

                /*var pedidos = _iRepositoryPedido.Listar();
                return View(pedidos);*/
            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
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