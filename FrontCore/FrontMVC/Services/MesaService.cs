using AutoMapper;
using FrontMVC.Helpers;
using FrontMVC.Interfaces;
using FrontMVC.Models.Mesa;
using FrontMVC.Models.Prato;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FrontMVC.Services
{
    public class MesaService : IServiceMesa<MesaModel>
    {
        private readonly ClientHelpers _client;
        private IConfiguration configuration;
        private readonly IMapper _mapper;

        public MesaService(ClientHelpers client, IConfiguration configuration, IMapper mapper)
        {
            _client = client;
            this.configuration = configuration;
            _mapper= mapper;
        }

        public async Task<MesaModel> Adicionar(MesaModel entity)
        {     
            MesaIncluir mesaIncluir= _mapper.Map<MesaIncluir>(entity);
            HttpResponseMessage response = await _client.gerarClienComTokenPost().PostAsJsonAsync(configuration["EndPointsDEV:API_Mesa"] + "Incluir", mesaIncluir);
            if (response.IsSuccessStatusCode)
            {
                string sc = "Sucesso";
            }
            return entity;
        }
      

        public async Task<MesaModel> Atualizar(MesaModel entity, Guid id)
        {
            MesaAlterar mesaAlterar = _mapper.Map<MesaAlterar>(entity);
            HttpResponseMessage response = await _client.gerarClienComTokenPost().PutAsJsonAsync(configuration["EndPointsDEV:API_Mesa"] + "Alterar/"+id, mesaAlterar);
            if (response.IsSuccessStatusCode)
            {
                return entity;
            }
            throw new Exception("error");
        }

        public async Task<bool> Excluir(Guid id)
        {
            HttpResponseMessage response = await _client.gerarClienComToken(configuration["EndPointsDEV:API_Mesa"]).DeleteAsync($"Deletar/{id}");

            bool delete = false;
            if (response.IsSuccessStatusCode)
            {
                delete = true;
            }

            return delete;
        }

        public async Task<MesaModel> FiltrarId(Guid id)
        {
            HttpResponseMessage response = await _client.gerarClienComToken(configuration["EndPointsDEV:API_Mesa"]).GetAsync($"FiltrarPorId/{id}");
            MesaModel mesa = new MesaModel();
            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;
                mesa = JsonConvert.DeserializeObject<MesaModel>(dados);
            }
            return mesa;
        }

        public async Task<IEnumerable<MesaModel>> Listar()
        {
            List<MesaModel>? list = new List<MesaModel>();

            HttpResponseMessage response = await _client.gerarClienComToken(configuration["EndPointsDEV:API_Mesa"]).GetAsync("Listar");

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<MesaModel>>(dados);
            }
            return list;
        }

        public async Task<OcuparMesa> OcuparMesa(OcuparMesa ocuparMesa)
        {
            OcuparMesaPost Post = new OcuparMesaPost
            {
                ClienteId = ocuparMesa.Clientes.Id,
                MesaId = ocuparMesa.Id
            };

            HttpResponseMessage response = await _client.gerarClienComTokenPost().PutAsJsonAsync(configuration["EndPointsDEV:API_Mesa"] + "OcuparMesa/", Post);
            if (response.IsSuccessStatusCode)
            {
                return ocuparMesa;
            }
            throw new Exception("error");
        }

        public async Task<bool> DesocuparMesa(Guid id)
        {
            HttpResponseMessage response = await _client.gerarClienComToken(configuration["EndPointsDEV:API_Mesa"]).PutAsJsonAsync($"DesocuparMesa/{id}", id);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception("error");
        }
    }
}
