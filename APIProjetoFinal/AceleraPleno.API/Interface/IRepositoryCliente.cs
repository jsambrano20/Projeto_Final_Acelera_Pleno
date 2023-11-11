namespace AceleraPleno.API.Interface
{
    public interface IRepositoryCliente<TEntity> : IRepository<TEntity>
    {
        Task<TEntity> FiltrarPorCpf(string cpf);
    }
}
