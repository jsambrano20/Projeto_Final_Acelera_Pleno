using ProjetoFinal.Models;
using ProjetoFinal.ViewModels;
using ProjetoFinal.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjetoFinal.Views
{
    public partial class PedidosPage : ContentPage
    {
        PedidosViewModel _viewModel;

        public PedidosPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new PedidosViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

    }
}