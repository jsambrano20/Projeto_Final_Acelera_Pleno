using ProjetoFinal.Services;
using ProjetoFinal.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjetoFinal
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<PedidoService>();

            MainPage = new AppShell();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
