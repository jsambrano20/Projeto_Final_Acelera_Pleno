namespace AceleraPleno.API.Models
{
    public class Log : Entity
    {
        public string Tabela { get; set; }
        public string Funcionario { get; set; }
        public string Acao { get; set; }
        public Guid IdTabela { get; set; }
        public string Dados { get; set; }
    }
}
