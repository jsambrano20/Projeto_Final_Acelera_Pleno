using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.RegularExpressions;

namespace FrontMVC.Models.Prato
{
    public class PratoModel
    {
        public Guid Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public string Foto { get; set; }
        public IFormFile FotoBase { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Valor { get; set; }
        public bool Status { get; set; }
        public byte[] Jpg { get; set; }

        public void ConverterBase64ParaJpg()
        {
            // converte a string base64 em um array de bytes
            byte[] imagemBytes = Convert.FromBase64String(Foto);

            // cria um objeto Image a partir do array de bytes
            using (MemoryStream ms = new MemoryStream(imagemBytes))
            {
                Image imagem = Image.FromStream(ms);

                // salva a imagem como arquivo JPEG em um array de bytes
                using (MemoryStream ms2 = new MemoryStream())
                {
                    imagem.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Jpg = ms2.ToArray();
                }
            }
        }
    }
}
