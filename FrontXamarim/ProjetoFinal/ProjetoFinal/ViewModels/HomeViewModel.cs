using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjetoFinal.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            Title = "Sistema de Entrega";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync(""));
        }

        public ICommand OpenWebCommand { get; }
    }
}