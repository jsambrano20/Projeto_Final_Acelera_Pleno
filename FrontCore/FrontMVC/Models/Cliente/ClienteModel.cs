using System.ComponentModel.DataAnnotations;

namespace FrontMVC.Models.Cliente
{
    public class ClienteModel
    {
        public Guid Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string Nome { get; set; }

        [MaxLength(11)]
        [MinLength(11)]
        public string CPF { get; set; }

        [Display(Name = "CPF")]
        [DisplayFormat(DataFormatString = "{0:###.###.###-##}")]
        public string CPFFormatado { get; set; }

        public string CPFConvertido
        { get { if (!string.IsNullOrEmpty(CPF) && CPF.Length == 11) { return CPF.Substring(0, 3) + "." + CPF.Substring(3, 3) + "." + CPF.Substring(6, 3) + "-" + CPF.Substring(9); } return CPF; } }
    }
}
