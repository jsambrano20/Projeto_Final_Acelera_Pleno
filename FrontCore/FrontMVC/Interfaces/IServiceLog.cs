namespace FrontMVC.Interfaces
{
    public interface IServiceLog<TEntity>
    {
        Task<IEnumerable<TEntity>> Listar();
        Task<TEntity> FiltrarId(Guid id);
    }
}
