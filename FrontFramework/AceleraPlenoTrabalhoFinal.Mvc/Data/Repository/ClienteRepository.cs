using AceleraPlenoTrabalhoFinal.Mvc.Data.Interface;
using AceleraPlenoTrabalhoFinal.Mvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AceleraPlenoTrabalhoFinal.Mvc.Data.Repository
{
    public class ClienteRepository : IRepository<Cliente>
    {
        private readonly string apiUrl = "https://localhost:7198/api/Cliente/Listar";
        public async Task<IEnumerable<Cliente>> Listar()
        {
            List<Cliente> listaCliente = new List<Cliente>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(apiUrl).Result)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    listaCliente = JsonConvert.DeserializeObject<List<Cliente>>(apiResponse);
                }
            }
            return listaCliente;
        }
        public Task<Cliente> Adicionar(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente> Atualizar(Cliente entity, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Excluir(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente> FiltrarId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}