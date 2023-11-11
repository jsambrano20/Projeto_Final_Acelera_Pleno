using AceleraPlenoTrabalhoFinal.Mvc.Data.Interface;
using AceleraPlenoTrabalhoFinal.Mvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AceleraPlenoTrabalhoFinal.Mvc.Data.Repository
{
    public class RepositoryPedido : IRepositoryPedido<Pedido>
    {
        private string urlApi = ConfigurationManager.AppSettings["apiPedido"];
        /*public RepositoryPedido()
        {
            Console.WriteLine("Criando instância de RepositoryPedido<Pedido>");
        }*/

        public Task<Pedido> Adicionar(Pedido entity)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> AlterarParaCancelado(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> AlterarParaDisponivel(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> AlterarParaEntregue(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> AlterarParaPreparando(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> Atualizar(Pedido entity, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Excluir(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> FiltrarId(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Pedido>> Listar()
        {
            List<Pedido> listaPedido = new List<Pedido>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(urlApi + "/Listar").Result)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    listaPedido = JsonConvert.DeserializeObject<List<Pedido>>(apiResponse);
                }
            }
            return listaPedido;
        }
    }
}