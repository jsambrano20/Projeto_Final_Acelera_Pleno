using FrontMVC.Models.Cliente;

namespace FrontMVC.Models.Mesa
{
    public class OcuparMesa
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public ClienteModel Clientes { get; set; }
    }
}
