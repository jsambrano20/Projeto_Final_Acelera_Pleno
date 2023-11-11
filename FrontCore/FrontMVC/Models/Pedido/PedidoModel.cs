using FrontMVC.Models.Enuns;

namespace FrontMVC.Models.Pedido
{
    public class PedidoModel
    {
        public Guid Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public Guid MesaId { get; set; }
        //public virtual Mesa? Mesas { get; set; }
        public Guid PratoId { get; set; }
        //public virtual Prato? Pratos { get; set; }
        public string CPF { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public StatusPedido? StatusPedido { get; set; }
        public DateTime DtRecebimento { get; set; }
    }
}
