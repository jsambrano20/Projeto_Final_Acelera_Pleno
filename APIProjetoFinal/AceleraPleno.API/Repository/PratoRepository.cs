using AceleraPleno.API.Data;
using AceleraPleno.API.Interface;
using AceleraPleno.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using AceleraPleno.API.Models.Enuns;
using AceleraPleno.API.Models.PartialModels;

namespace AceleraPleno.API.Repository
{
    public class PratoRepository : IRepositoryPrato<Prato>
    {
        private readonly DataContext _dataContext;
        private readonly IRepositoryLog<Log> _log;
        public PratoRepository(DataContext dataContext, IRepositoryLog<Log> log)
        {
            _dataContext = dataContext;
            _log = log;
        }
        public async Task<Prato> Adicionar(Prato prato)
        {
            if (prato == null)
                throw new ArgumentNullException("Erro prato");

            prato.DataInclusao = DateTime.Now;

            _dataContext.Pratos.Add(prato);
            await _dataContext.SaveChangesAsync();

            _log.Adicionar("Pratos", prato.Id, "Adicionar", JsonSerializer.Serialize(prato), null);

            return prato;
        }
        public async Task<Prato> Atualizar(Prato prato, Guid id)
        {
            if (prato == null) throw new System.Exception("Erro ao atualizar");

            Prato pratoAtual = await FiltrarId(id);
            if (pratoAtual == null) throw new System.Exception(string.Format("Cliente não encontrado"));

            try
            {
                pratoAtual.Descricao = prato.Descricao == null ? pratoAtual.Descricao : prato.Descricao;
                pratoAtual.Titulo = prato.Titulo == null ? pratoAtual.Titulo : prato.Titulo;
                pratoAtual.Foto = prato.Foto == null ? pratoAtual.Foto : prato.Foto;
                pratoAtual.DataAlteracao = DateTime.Now;
                pratoAtual.Valor = prato.Valor;
                pratoAtual.Status = prato.Status;

                _dataContext.Pratos.Update(pratoAtual);

                await _dataContext.SaveChangesAsync();

                _log.Adicionar("Pratos", pratoAtual.Id, "Atualizar", JsonSerializer.Serialize(pratoAtual), null);
                return pratoAtual;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> Excluir(Guid id)
        {
            Prato prato = await FiltrarId(id);

            if (prato == null) throw new System.Exception("Erro ao Excluir");

            _dataContext.Pratos.Remove(prato);
            await _dataContext.SaveChangesAsync();

            _log.Adicionar("Pratos", prato.Id, "Excluir", JsonSerializer.Serialize(prato), null);
            return true;
        }
        public async Task<IEnumerable<Prato>> Listar()
        {
            return await _dataContext.Pratos.ToListAsync();
        }
        public async Task<Prato> FiltrarId(Guid id)
        {
            return await _dataContext.Pratos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<string> AtivarPrato(Guid id)
        {
            if (id == null)
                return "Favor informar o prato";

            Prato c = new Prato();

            c = await FiltrarId(id);

            if (c.Status)
                return "Prato já está Ativo";

            c.Status = true;
            

            await AlterarPrato(c);

            return $"Prato ativo com sucesso!";
        }
        public async Task<string> InativarPrato(Guid id)
        {
            Prato c = new Prato();

            c = await FiltrarId(id);

            if (!c.Status)
                return "Prato ja esta Inativo!";

            c.Status = false;

            await AlterarPrato(c);

            return $"Prato Inativo com sucesso!";
        }
        private async Task<bool> AlterarPrato(Prato p)
        {
            if (p == null)
            {
                return false;
            }
            try
            {
                _dataContext.Entry<Prato>(p).State = EntityState.Modified;
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
