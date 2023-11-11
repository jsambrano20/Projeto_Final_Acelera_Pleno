namespace AceleraPleno.API.Interface
{
    public interface IRepositoryLog<TEntity>
    {
        //void Adicionar(TEntity entity);
        void Adicionar(string tabela, Guid id, string acao, string objetoNovo, string? objetoOriginal);
        Task<IEnumerable<TEntity>> Listar();
        Task<IEnumerable<TEntity>> FiltrarData(DateTime date);
        Task<TEntity> FiltrarId(Guid id);
    }
}
