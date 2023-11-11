using FrontMVC.Helpers;
using FrontMVC.Interfaces;
using FrontMVC.Models.Log;
using FrontMVC.Models.Prato;
using Newtonsoft.Json;

namespace FrontMVC.Services
{
    public class LogService : IServiceLog<LogModel>
    {
        private readonly ClientHelpers _client;
        private IConfiguration configuration;

        public LogService(IConfiguration configuration, ClientHelpers client)
        {
            this.configuration = configuration;
            _client = client;
        }
        public async Task<IEnumerable<LogModel>> Listar()
        {

            List<LogModel>? list = new List<LogModel>();

            HttpResponseMessage response = await _client.gerarClienComToken(configuration["EndPointsDEV:API_Log"]).GetAsync("Listar");

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<LogModel>>(dados);
            }
            return list;
        }

        public async Task<LogModel> FiltrarId(Guid id)
        {
            LogModel log = new LogModel();
            string json = JsonConvert.SerializeObject(id);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.gerarClienComToken(configuration["EndPointsDEV:API_Log"]).
                GetAsync($"FiltrarPorId/{id}");


            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;
                log = JsonConvert.DeserializeObject<LogModel>(dados);
            }
            return log;

        }
    }
}
