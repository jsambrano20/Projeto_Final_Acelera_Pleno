namespace FrontMVC.Models.Log
{
    public class LogModel
    {
        public Guid Id { get; set; }
        public string Tabela { get; set; }
        public string Funcionario { get; set; }
        public string Acao { get; set; }
        public Guid IdTabela { get; set; }
        public string Dados { get; set; }
    }
}
