using AceleraPleno.API.Data;
using AceleraPleno.API.Interface;
using AceleraPleno.API.Models;
using AceleraPleno.API.Models.Enuns;
using AceleraPleno.API.Models.PartialModels;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AceleraPleno.API.Repository
{
    public class MesaRepository : IRepositoryMesa<Mesa>
    {
        private readonly DataContext _dataContext;
        private readonly IRepositoryLog<Log> _log;
        public MesaRepository(DataContext dataContext, IRepositoryLog<Log> log)
        {
            _dataContext = dataContext;
            _log = log;
        }
        public async Task<Mesa> Adicionar(Mesa mesa)
        {
            if (mesa == null)
            {
                throw new ArgumentNullException("Erro mesa nulla");
            }

            mesa.DataInclusao = DateTime.Now;
            mesa.Ocupada = false;
            mesa.StatusMesa = StatusMesa.Disponivel;
            mesa.ClienteId = null;

            _dataContext.Mesas.Add(mesa);
            await _dataContext.SaveChangesAsync();

            _log.Adicionar("Mesa", mesa.Id, "Adicionar", JsonSerializer.Serialize(mesa), null);

            return mesa;
        }

        public async Task<Mesa> Atualizar(Mesa cartao, Guid id)
        {
            if (cartao.Id != id)
            {
                throw new Exception("Cliente e Id não conferem");
            }
            Mesa m = await FiltrarId(id);

            m.Ambiente = cartao.Ambiente;
            m.Descricao = cartao.Descricao;
            m.DataAlteracao = DateTime.Now;
            m.Lugares = cartao.Lugares;
            m.StatusMesa = cartao.StatusMesa;

            bool t = await AlterarMesa(m);
            if (t)
                return m;
            else
                throw new Exception("Não foi possivel altera a mesa");
        }

        public async Task<bool> Excluir(Guid id)
        {
            Mesa mesa = await FiltrarId(id);

            if (mesa == null) throw new System.Exception("Erro ao Excluir");

            _dataContext.Mesas.Remove(mesa);
            _dataContext.SaveChangesAsync();
            _log.Adicionar("Mesa", mesa.Id, "Excluir", JsonSerializer.Serialize(mesa), null);
            return true;
        }

        public async Task<Mesa> FiltrarId(Guid id)
        {
            return await _dataContext.Mesas.
                Include(m => m.Clientes).
                FirstOrDefaultAsync(m => m.Id == id);

        }

        public async Task<IEnumerable<Mesa>> Listar()
        {
            return await _dataContext.Mesas.OrderBy(x => x.Ambiente).ToListAsync();
        }

        public async Task<string> OcuparMesa(OcuparMesa mesaOcupada)
        {
            if (mesaOcupada == null)
                return "Favor informar a mesa";

            Mesa c = new Mesa();                

            c = await FiltrarId(mesaOcupada.MesaId);

            if (c.Ocupada)
                return "Mesa ja esta ocupada!";

            if (c.StatusMesa == StatusMesa.Reservado)
                return "Mesa esta Reservada";

            c.Ocupada = true;
            c.ClienteId = mesaOcupada.ClienteId;

            await AlterarMesa(c);

            return $"Mesa ocupada com sucesso!";
        }

        public async Task<string> DesocuparMesa(Guid id)
        {
            Mesa c = new Mesa();

            c = await FiltrarId(id);

            if (!c.Ocupada)
                return "Mesa ja esta Desocupada!";

            c.Ocupada = false;
            c.ClienteId = null;
            c.StatusMesa = StatusMesa.Disponivel;
           
            await AlterarMesa(c);

            return $"Mesa desocupada com sucesso!";
        }

        private async Task<bool> AlterarMesa(Mesa alt)
        {
            if (alt == null)
            {
                return false;
            }
            try
            {
                _dataContext.Entry<Mesa>(alt).State = EntityState.Modified;
                await  _dataContext.SaveChangesAsync();

                _log.Adicionar("Mesa", alt.Id, "Atualizar", JsonSerializer.Serialize(alt), null);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task <IEnumerable<Mesa>> FiltrarPorCPF(string cpf)
        {
            return await _dataContext.Mesas.OrderBy(x => x.Ambiente).Where(m=> m.Clientes.CPF == cpf).ToListAsync();
        }
    }
}
