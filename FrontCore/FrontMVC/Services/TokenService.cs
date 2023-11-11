using FrontMVC.Models.Token;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;
using FrontMVC.Models.Prato;

namespace FrontMVC.Services
{
    public class TokenService
    {
        private IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetToken()
        {
            TokenModel tokenModel = new TokenModel();
            tokenModel.clienteId = configuration["AuthenticacaoDEV:ClienteId"];
            tokenModel.clienteSecret = configuration["AuthenticacaoDEV:ClienteSecret"];
            string path = configuration["EndPointsDEV:Token"];
            string token = "";
            HttpClient clientToken = new HttpClient();
            clientToken.DefaultRequestHeaders.Accept.Clear();
            clientToken.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage respToken = clientToken.PostAsJsonAsync(path, tokenModel).Result;

            if (respToken.StatusCode == HttpStatusCode.OK)
            {
                token = respToken.Content.ReadAsStringAsync().Result;
                 return JsonConvert.DeserializeObject<string>(token);

            }
            else
            {
                return "";
            }
        }
    }
}
