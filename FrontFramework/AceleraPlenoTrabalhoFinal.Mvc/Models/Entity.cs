using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AceleraPlenoTrabalhoFinal.Mvc.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }
}