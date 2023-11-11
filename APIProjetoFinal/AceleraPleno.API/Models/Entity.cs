using System.ComponentModel.DataAnnotations;

namespace AceleraPleno.API.Models
{
    public class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Inclusão")]
        //[JsonIgnore] //Para não exibir no Json da API. Confirmar se não vai dar problemas para com os relacionamentos
        public DateTime DataInclusao { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Alteração")]
        //[JsonIgnore]
        public DateTime? DataAlteracao { get; set; }
    }
}
