using FrontMVC.Services;
using NuGet.Common;
using System.Net.Http.Headers;

namespace FrontMVC.Helpers
{
    public class ClientHelpers : HttpClient
    {
        private readonly TokenService tokenService;
        HttpClient client = new HttpClient();
        public ClientHelpers(TokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        public HttpClient gerarClienComToken(string uri)
        {            
            string token = tokenService.GetToken();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;

        }
        public HttpClient gerarClienComTokenPost()
        {
            string token = tokenService.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
    }
}
