using AceleraPleno.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;


namespace AceleraPleno.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Prato> Pratos { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DataContext()
        {
        }
        /*public DataContext(DbContextOptions options) : base(options)
        {
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));
            optionsBuilder.UseSqlServer(jAppSettings["ConnectionStrings"]["DefaultConnection"].ToString());
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
