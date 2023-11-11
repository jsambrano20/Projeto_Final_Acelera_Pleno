using AceleraPleno.API.Models.Enuns;

namespace AceleraPleno.API.Models
{
    public class Mesa : Entity
    {
        public string Descricao { get; set; }
        public int Lugares { get; set; }
        public Guid? ClienteId { get; set; }
        public virtual Cliente? Clientes { get; set; }
        public bool Ocupada { get; set; }
        public string Ambiente { get; set; }
        public StatusMesa StatusMesa { get; set; }
    }
}
