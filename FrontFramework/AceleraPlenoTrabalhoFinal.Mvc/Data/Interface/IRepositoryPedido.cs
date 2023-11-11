using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceleraPlenoTrabalhoFinal.Mvc.Data.Interface
{
    public interface IRepositoryPedido<TEntity> : IRepository<TEntity>
    {
        Task<TEntity> AlterarParaPreparando(Guid id);
        Task<TEntity> AlterarParaDisponivel(Guid id);
        Task<TEntity> AlterarParaEntregue(Guid id);
        Task<TEntity> AlterarParaCancelado(Guid id);
    }
}
