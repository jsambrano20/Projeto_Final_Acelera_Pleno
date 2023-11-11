using AceleraPlenoTrabalhoFinal.Mvc.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AceleraPlenoTrabalhoFinal.Mvc.Models
{
    public class Pedido : Entity
    {
        public Guid MesaId { get; set; }
        public string Mesa { get; set; }
        public Guid PratoId { get; set; }
        public string Prato { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public DateTime DtRecebimento { get; set; }
    }
}