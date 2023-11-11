using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AceleraPlenoTrabalhoFinal.Mvc.Models
{
    public class Cliente : Entity
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
    }
}