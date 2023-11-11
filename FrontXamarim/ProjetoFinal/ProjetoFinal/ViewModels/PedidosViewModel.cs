using ProjetoFinal.Model;
using ProjetoFinal.Models;
using ProjetoFinal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjetoFinal.ViewModels
{
    public class PedidosViewModel : BaseViewModel
    {
        private Pedido _selectedItem;
        public Pedido SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public ICommand EntregarCommand => new Command<Pedido>((item) =>
        {
            SelectedItem = item;
            Entregar(item);
            
        });

        public ObservableCollection<Pedido> Items { get; }
        public Command LoadPedidoCommand { get; }

        public PedidosViewModel()
        {
            Title = "Pedidos";
            Items = new ObservableCollection<Pedido>();
            LoadPedidoCommand = new Command(async () => await LoadPedidos());


        }
        public async Task LoadPedidos()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var items = await DataStore.ListarPedidosDisponivel();

                Items.Clear();


                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;       
        }
     
        private async void Entregar(Pedido obj)
        {
            try
            {
                string pedido = await DataStore.AlterarPedidoParaEntregue(obj.Id);
                App.Current.MainPage.DisplayAlert("Aviso", $"{pedido}", "Ok");

                
                LoadPedidos();
            }
            catch (Exception ex)
            {

            }
        }


     


    }





}
