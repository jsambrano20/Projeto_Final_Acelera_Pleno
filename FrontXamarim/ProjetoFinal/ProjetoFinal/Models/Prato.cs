using ProjetoFinal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace ProjetoFinal.Models
{
    public class Prato : Entity
    {
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public string Foto { get; set; }
        public decimal Valor { get; set; }
        public bool Status { get; set; }

        //public ObservableCollection<Prato> Items { get; set; }
        //public ImageSource ImageSource { get; set; }
        //public Prato(Prato item)
        //{
        //    Descricao = item.Descricao;
        //    byte[] imageBytes = Convert.FromBase64String(item.Foto);
        //    ImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
        //}
    }
}
