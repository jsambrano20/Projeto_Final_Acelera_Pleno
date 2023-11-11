using FrontMVC.Models.Enuns;

namespace FrontMVC.Models.Pedido
{
    public class PedidosMesa
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
        public string? Nome { get; set; }
        public Guid MesaId { get; set; }
        public string DescMesa { get; set; }
        public Guid PratoId { get; set; }
        public string DescPrato { get; set; }
        public string CPF { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public StatusPedido? StatusPedido { get; set; }
    }
}
