using AceleraPlenoTrabalhoFinal.Mvc.Data.Interface;
using AceleraPlenoTrabalhoFinal.Mvc.Data.Repository;
using AceleraPlenoTrabalhoFinal.Mvc.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AceleraPlenoTrabalhoFinal.Mvc.Startup))]
namespace AceleraPlenoTrabalhoFinal.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        /*public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRepositoryPedido<Pedido>, RepositoryPedido>();
        }*/
    }
}
