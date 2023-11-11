using FrontMVC.Helpers;
using FrontMVC.Interfaces;
using FrontMVC.Models.Cliente;
using FrontMVC.Models.Mesa;
using FrontMVC.Models.Pedido;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace FrontMVC.Services
{
    public class PedidoService : IServicePedido<PedidosMesa>
    {
        private readonly ClientHelpers _client;
        private IConfiguration _configuration;
        public PedidoService(IConfiguration configuration, ClientHelpers client)
        {
            _configuration = configuration;
            _client = client;
        }

        /*public async Task<IEnumerable<PedidoModel>> ListarPedidoPorMesaECPF(Guid id, string cpf)
        {
            //MesaModel mesa = await _serviceMesa.FiltrarId(id);
            //ClienteModel cliente = await _serviceCliente.FiltrarId(id);

            List<PedidoModel>? pedidos = new List<PedidoModel>();

            using (var response = await _client.gerarClienComToken(_configuration["EndPointsDEV:API_Pedido"]).GetAsync($"FiltrarPorMesaCliente/{cpf}/{id}"))
            {
                var dados = response.Content.ReadAsStringAsync().Result;
                pedidos = JsonConvert.DeserializeObject<List<PedidoModel>>(dados);
            }

            foreach (var item in pedidos)
            {
                await AlterarPedidoParaBaixado(item.Id);
            }
            return pedidos;
        }

        public async Task<PedidoModel> AlterarPedidoParaBaixado(Guid id)
        {
            PedidoModel pedido = new PedidoModel();
            using (var response = await _client.gerarClienComToken(_configuration["EndPointsDEV:API_Pedido"]).PutAsJsonAsync($"Baixado/{id}", id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var dados = response.Content.ReadAsStringAsync().Result;
                    pedido = JsonConvert.DeserializeObject<PedidoModel>(dados);
                }
            }
            return pedido;
        }*/

        public async Task<IEnumerable<PedidosMesa>> BaixarPedidosLiberarMesa(Guid id, string cpf)
        {
            IEnumerable<PedidosMesa> pedidos = new List<PedidosMesa>();
            using (var response = await _client.gerarClienComToken(_configuration["EndPointsDEV:API_Pedido"]).PutAsJsonAsync($"BaixarPedidosLiberarMesa/{cpf}/{id}", id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var dados = response.Content.ReadAsStringAsync().Result;
                    pedidos = JsonConvert.DeserializeObject<List<PedidosMesa>>(dados);
                }
            }
            return pedidos;
        }
    }
}
