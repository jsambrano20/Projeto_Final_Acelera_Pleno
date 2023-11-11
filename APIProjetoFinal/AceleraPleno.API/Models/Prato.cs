namespace AceleraPleno.API.Models
{
    public class Prato : Entity
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public decimal Valor { get; set; }
        public bool Status { get; set; }
    }
}
