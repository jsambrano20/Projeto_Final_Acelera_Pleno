
using AceleraPleno.API.Models;
using AceleraPleno.API.Models.PartialModels;

namespace AceleraPleno.API.Interface
{
    public interface IRepositoryMesa<TEntity> : IRepository<TEntity>
    {
        Task<string> OcuparMesa(OcuparMesa mesaOcupada);
        Task<string> DesocuparMesa(Guid id);
        Task <IEnumerable<Mesa>> FiltrarPorCPF(string cpf);
    }
}
