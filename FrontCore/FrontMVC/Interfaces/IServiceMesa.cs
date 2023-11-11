using FrontMVC.Models.Mesa;

namespace FrontMVC.Interfaces
{
    public interface IServiceMesa<TEntity> : IService<TEntity>
    {
        Task<OcuparMesa> OcuparMesa(OcuparMesa ocuparMesa);
        Task<bool> DesocuparMesa(Guid id);
    }
}
