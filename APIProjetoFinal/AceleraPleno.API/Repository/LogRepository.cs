using AceleraPleno.API.Data;
using AceleraPleno.API.Interface;
using AceleraPleno.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AceleraPleno.API.Repository
{
    public class LogRepository : IRepositoryLog<Log>
    {
        private readonly DataContext _dataContext;
        public LogRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        /*public async void Adicionar(Log log)
        {   
            log.DataInclusao = DateTime.Now;
            await _dataContext.Logs.AddAsync(log);
            _dataContext.SaveChanges();
        }*/
        public async void Adicionar(string tabela, Guid id, string acao, string objetoNovo, string? objetoOriginal)
        {
            var dados = objetoNovo;
            if(acao == "Atualizar")
                dados = $"DE:[{objetoOriginal}]; PARA:[{objetoNovo}]";

            Log log = new Log()
            {
                Funcionario = "Atendente1",
                Tabela = tabela,
                IdTabela = id,
                Acao = acao,
                Dados = dados,
                DataInclusao = DateTime.Now,
            };

            await _dataContext.Logs.AddAsync(log);
            _dataContext.SaveChanges();
        }
        public async Task<IEnumerable<Log>> Listar()
        {
            return await _dataContext.Logs.ToListAsync();
        }
        public async Task<IEnumerable<Log>> FiltrarData(DateTime data)
        {
            //data = data.Date;
            return await _dataContext.Logs.AsNoTracking().Where(x => x.DataInclusao.Date == data).ToListAsync();
        }
        public async Task<Log> FiltrarId(Guid id)
        {
            return await _dataContext.Logs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
