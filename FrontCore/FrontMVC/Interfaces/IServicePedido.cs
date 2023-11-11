using FrontMVC.Models.Mesa;
using FrontMVC.Models.Pedido;

namespace FrontMVC.Interfaces
{
    public interface IServicePedido<TEntity>
    {
        /*Task<IEnumerable<TEntity>> ListarPedidoPorMesaECPF(Guid id, string cpf);
        Task<TEntity> AlterarPedidoParaBaixado(Guid id);*/
        Task<IEnumerable<TEntity>> BaixarPedidosLiberarMesa(Guid id, string cpf);
    }
}
