using FrontMVC.Models.Prato;

namespace FrontMVC.Interfaces
{
    public interface IServicePrato<TEntity> : IService<TEntity>
    {
        Task<string> ConverteImg(IFormFile foto);
        Task<string> AtivarPrato(Guid id,bool prato);
        Task<string> InativarPrato(Guid id, bool prato);
    }
}
