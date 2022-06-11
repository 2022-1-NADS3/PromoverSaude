using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPSOne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExcluirEvento : ContentPage, INotifyPropertyChanged
    {
        private ObservableCollection<Agendamento> list;

        public ExcluirEvento(ObservableCollection<Agendamento> appointmentList)
        {
            InitializeComponent();
            deletar.ItemsSource = appointmentList;
            list = appointmentList;
        }

        public Command<object> DeleteCommand { get; set; }


        public void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            deletar.SelectedItem = null;
        }
        public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
        }

        public void Deletar_(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Agendamento agenda = (Agendamento)mi.CommandParameter;
            ExecExcluir(agenda.Id, agenda);
        }

        async void ExecExcluir(string id, Agendamento agenda)
        {
            var httpClient = new HttpClient();

            // envia a requisição POST
            var uri = "https://fecap-promoversaude.herokuapp.com/meus_exames/" + DadosModel.UserId.ToString() + "/" + id;
            var result = await httpClient.DeleteAsync(new Uri(uri));
            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Atenção", @"Agendamento Excluído", "Ok");
                list.Remove(agenda);
                deletar.ItemsSource = list;
            }
        }
        public void Terminar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Calendario());
        }
    }
}