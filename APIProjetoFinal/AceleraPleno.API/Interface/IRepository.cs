namespace AceleraPleno.API.Interface
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> Listar();
        Task<TEntity> FiltrarId(Guid id);
        Task<TEntity> Adicionar(TEntity entity);
        Task<TEntity> Atualizar(TEntity entity, Guid id);
        Task<bool> Excluir(Guid id);
    }
}
