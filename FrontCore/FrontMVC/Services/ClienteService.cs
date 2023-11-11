using FrontMVC.Helpers;
using FrontMVC.Interfaces;
using FrontMVC.Models.Cliente;
using FrontMVC.Models.Prato;
using Newtonsoft.Json;

namespace FrontMVC.Services
{
    public class ClienteService : IServiceCliente<ClienteModel>
    {
        private readonly ClientHelpers _client;
        private IConfiguration _configuration;
        public ClienteService(IConfiguration configuration, ClientHelpers client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<IEnumerable<ClienteModel>> Listar()
        {
            List<ClienteModel>? list = new List<ClienteModel>();

            HttpResponseMessage response = await _client.gerarClienComToken(_configuration["EndPointsDEV:API_Cliente"]).GetAsync("Listar");

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<ClienteModel>>(dados);
            }
            return list;
        }

        public async Task<IEnumerable<ClienteModel>> ListarLike(string like)
        {
            List<ClienteModel>? list = new List<ClienteModel>();

            HttpResponseMessage response = await _client.gerarClienComToken(_configuration["EndPointsDEV:API_Cliente"]).GetAsync("Listar");

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<ClienteModel>>(dados);
            }
            IEnumerable<ClienteModel> enumerable = list.Where(c => c.CPF.Contains(like) || c.Nome.Contains(like));
            return enumerable;
        }

        public async Task<ClienteModel> Adicionar(ClienteModel cliente)
        {
            string json = JsonConvert.SerializeObject(cliente);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.gerarClienComToken(_configuration["EndPointsDEV:API_Cliente"]).PostAsync("Incluir", httpContent);

            if (response.IsSuccessStatusCode)
            {
                string sc = "Sucesso";
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                cliente = JsonConvert.DeserializeObject<ClienteModel>(apiResponse);
            }

            return cliente;
        }

        public async Task<ClienteModel> Atualizar(ClienteModel cliente, Guid id)
        {
            string json = JsonConvert.SerializeObject(cliente);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.gerarClienComToken(_configuration["EndPointsDEV:API_Cliente"])
                .PutAsync($"Alterar/{id}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                string sc = "Sucesso";
                //cliente = FiltrarId(id);
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                cliente = JsonConvert.DeserializeObject<ClienteModel>(apiResponse);
            }

            return cliente;
        }

        public async Task<bool> Excluir(Guid id)
        {
            HttpResponseMessage response = await _client.gerarClienComToken(_configuration["EndPointsDEV:API_Cliente"]).DeleteAsync($"Deletar/{id}");

            bool delete = false;
            if (response.IsSuccessStatusCode)
            {
                delete = true;
            }

            return delete;
        }

        public async Task<ClienteModel> FiltrarId(Guid id)
        {
            ClienteModel cliente = new ClienteModel();
            string json = JsonConvert.SerializeObject(id);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.gerarClienComToken(_configuration["EndPointsDEV:API_Cliente"]).
                GetAsync($"FiltrarPorId/{id}");


            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;
                cliente = JsonConvert.DeserializeObject<ClienteModel>(dados);
            }
            return cliente;
        }

        public async Task<ClienteModel> FiltrarPorCpf(string cpf)
        {
            ClienteModel cliente = new ClienteModel();
            string json = JsonConvert.SerializeObject(cpf);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.gerarClienComToken(_configuration["EndPointsDEV:API_Cliente"]).
                GetAsync($"FiltrarPorCpf/{cpf}");


            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;
                cliente = JsonConvert.DeserializeObject<ClienteModel>(dados);
            }
            return cliente;
        }
    }
}
