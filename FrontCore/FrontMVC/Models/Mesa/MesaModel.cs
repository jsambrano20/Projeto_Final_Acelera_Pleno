using FrontMVC.Models.Cliente;
using FrontMVC.Models.Enuns;

namespace FrontMVC.Models.Mesa
{
    public class MesaModel
    {
        public Guid Id { get; set; }
        public DateTime? DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string Descricao { get; set; }
        public int Lugares { get; set; }
        public Guid? ClienteId { get; set; }
        public ClienteModel? Clientes { get; set; }
        public bool Ocupada { get; set; }
        public string Ambiente { get; set; }
        public StatusMesa StatusMesa { get; set; }
    }
}
