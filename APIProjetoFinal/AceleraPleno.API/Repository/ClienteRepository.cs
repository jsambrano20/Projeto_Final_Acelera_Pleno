using AceleraPleno.API.Data;
using AceleraPleno.API.Interface;
using AceleraPleno.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AceleraPleno.API.Repository
{
    public class ClienteRepository : IRepositoryCliente<Cliente>
    {
        private readonly DataContext _dataContext;
        private readonly IRepositoryLog<Log> _log;
        public ClienteRepository(DataContext dataContext, IRepositoryLog<Log> log)
        {
            _dataContext = dataContext;
            _log = log;
        }
        public async Task<IEnumerable<Cliente>> Listar()
        {
            return await _dataContext.Clientes.ToListAsync();
        }
        public async Task<Cliente> FiltrarId(Guid id)
        {
            return await _dataContext.Clientes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Cliente> Adicionar(Cliente cliente)
        {
            try
            {
                cliente.DataInclusao = DateTime.Now;
                await _dataContext.Clientes.AddAsync(cliente);
                _dataContext.SaveChanges();

                //Log log = new Log()
                //{
                //    Funcionario = "Atendente1",
                //    Tabela = "Cliente",
                //    IdTabela = cliente.Id,
                //    Acao = "Adcionar",
                //    Dados = JsonSerializer.Serialize(cliente), //$"Nome: {cliente.Nome}, CPF: {cliente.CPF}",
                //    DataInclusao = cliente.DataInclusao,
                //};
                //_log.Adicionar(log);

                _log.Adicionar("Cliente", cliente.Id, "Adicionar", JsonSerializer.Serialize(cliente), null);
                return cliente;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<Cliente> Atualizar(Cliente cliente, Guid id)
        {
            if (cliente == null) throw new System.Exception("Erro ao atualizar");

            Cliente clienteDb = await FiltrarId(id);
            if (clienteDb == null) throw new System.Exception(string.Format("Cliente não encontrado"));

            try
            {
                cliente.Nome = cliente.Nome == null ? clienteDb.Nome : cliente.Nome;
                cliente.CPF = cliente.CPF == null ? clienteDb.CPF : cliente.CPF;
                cliente.DataInclusao = clienteDb.DataInclusao;
                cliente.DataAlteracao = DateTime.Now;

                _dataContext.Clientes.Update(cliente);

                await _dataContext.SaveChangesAsync();

                _log.Adicionar("Cliente", cliente.Id, "Atualizar", JsonSerializer.Serialize(cliente), JsonSerializer.Serialize(clienteDb));
                return cliente;

            }
            catch (Exception ex)
            {
                throw new Exception( ex.Message.ToString());
            }
        }
        public async Task<bool> Excluir(Guid id)
        {
            Cliente cliente = await FiltrarId(id);

            if (cliente == null) throw new System.Exception("Erro ao Excluir");

            _dataContext.Clientes.Remove(cliente);
            await _dataContext.SaveChangesAsync();

            _log.Adicionar("Cliente", cliente.Id, "Excluir", JsonSerializer.Serialize(cliente), null);
            return true;
        }

        public async Task<Cliente> FiltrarPorCpf(string cpf)
        {
            return await _dataContext.Clientes.AsNoTracking().FirstOrDefaultAsync(x => x.CPF == cpf);
        }
    }
}
