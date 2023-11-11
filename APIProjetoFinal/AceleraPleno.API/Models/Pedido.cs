using AceleraPleno.API.Models.Enuns;

namespace AceleraPleno.API.Models
{
    public class Pedido : Entity
    {
        public Guid MesaId { get; set; }
        public virtual Mesa? Mesas { get; set; }
        public Guid PratoId { get; set; }
        public virtual Prato? Pratos { get; set; }
        public string CPF { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public StatusPedido? StatusPedido { get; set; }
        public DateTime DtRecebimento { get; set; }
    }
}
