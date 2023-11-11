using ProjetoFinal.ViewModels;
using ProjetoFinal.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjetoFinal
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
        }

    }
}
