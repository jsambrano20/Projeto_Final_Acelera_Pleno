using ProjetoFinal.Model;
using ProjetoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using ProjetoFinal.Model.Enuns;
using Xamarin.Essentials;

namespace ProjetoFinal.Services
{
    public class PedidoService : IRepository<Pedido>
    {
        HttpClient client;

        public PedidoService()
        {
            if (client == null)
            {
                HttpClientHandler insecureHandler = GetInsecureHandler();
                client = new HttpClient(insecureHandler);
                // client = new HttpClient();
                client.BaseAddress = new Uri("https://10.0.2.2:7198/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
        public async Task<IEnumerable<Pedido>> ListarPedidosDisponivel()
        {
            var response = await client.GetAsync("api/Pedido/Listar");

            if (response.IsSuccessStatusCode)
            {
                var resultado = await response.Content.ReadAsStringAsync();


                return await Task.FromResult(JsonConvert.DeserializeObject<Pedido[]>(resultado).
                    ToList().FindAll(x => x.StatusPedido == StatusPedido.Disponivel));
            }
            else
            {
                return null;
            }
        }

        public async Task<Pedido> FiltrarId(Guid id)
        {
            var response = await client.GetAsync($"api/Pedido/FiltrarPorId/{id}");

            if (response.IsSuccessStatusCode)
            {
                var resultado = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Pedido>(resultado);
            }
            else
            {
                return null;
            }
        }
        public async Task<string> AlterarPedidoParaEntregue(Guid id)
        {
            string retorno = "";

            Pedido pedidoDb = await FiltrarId(id);

            if (pedidoDb == null) throw new System.Exception(string.Format("Pedido não encontrado"));

            if (pedidoDb.StatusPedido == StatusPedido.Entregue)
                retorno = "Pedido já foi entregue";

            pedidoDb.StatusPedido = StatusPedido.Entregue;
            pedidoDb.DataAlteracao = DateTime.Now;

            string json = JsonConvert.SerializeObject(pedidoDb);
            HttpContent content = new StringContent(json, Encoding.Unicode, "application/json");
            var response = await client.PutAsync("api/Pedido/Entregue/" + id, content);

            if (response.IsSuccessStatusCode) 
                retorno = "Pedido Entregue com Sucesso";
            else 
                retorno = "Pedido não Entregue";
            
            return retorno;

        }


    }
}