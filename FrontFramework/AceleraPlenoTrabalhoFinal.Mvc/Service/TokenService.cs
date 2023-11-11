using AceleraPlenoTrabalhoFinal.Mvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace AceleraPlenoTrabalhoFinal.Mvc.Service
{
    public class TokenService
    {
        

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
                    return JsonConvert.DeserializeObject<string>(token);
                }
            }
            //return token;


            /*HttpClient clientToken = new HttpClient();
            clientToken.DefaultRequestHeaders.Accept.Clear();
            clientToken.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage respToken = clientToken.PostAsync(path, tokenModel).Result;

            if (respToken.StatusCode == HttpStatusCode.OK)
            {
                token = respToken.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<string>(token);

            }
            else
            {
                return "";
            }*/
        }
    }
}