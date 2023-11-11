using FrontMVC.Models.Enuns;

namespace FrontMVC.Models.Mesa
{
    public class MesaAlterar
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public int Lugares { get; set; }
        public string Ambiente { get; set; }
        public StatusMesa StatusMesa { get; set; }
    }
}
