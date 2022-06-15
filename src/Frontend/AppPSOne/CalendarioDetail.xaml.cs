using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Collections.Generic;
using Xamarin.Plugin.Calendar.Models;
using System.Runtime.CompilerServices;
using System.Globalization;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppPSOne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarioDetail : ContentPage, INotifyPropertyChanged
    {
        private string day;
        private string month;

        public DateTime SelectedDate { get; private set; }

        public CalendarioDetail()
        {
            InitializeComponent();
            GetList();
        }
        private void Adicionar_Evento(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdicionarEvento());
        }

        async void GetList()
        {

            var httpClient = new HttpClient();

            // envia a requisição POST
            var uri = "https://fecap-promoversaude.herokuapp.com/meus_exames/" + DadosModel.UserId.ToString();
            var result = await httpClient.GetAsync(new Uri(uri));
            string responseBody = await result.Content.ReadAsStringAsync();
            //JObject textresult = JsonConvert.DeserializeObject<JObject>(responseBody);
            List<DadosAgenda> listaExames = JsonConvert.DeserializeObject<List<DadosAgenda>>(responseBody) as List<DadosAgenda>;
            CalendarioModel.listaPub = listaExames;

            if (result.IsSuccessStatusCode)
            {
                this.BindingContext = new CalendarioModel();
            }
        }

        private void Excluir_Evento(object sender, EventArgs e)
        {
            if (!CalendarioModel.evento_copia.Any())
            {
                return;
            }

            if (xcalendar.Day.ToString().Length == 1)
            {
                day = "0" + xcalendar.Day.ToString();
            }
            else
            {
                day = xcalendar.Day.ToString();
            }

            if (xcalendar.Month.ToString().Length == 1)
            {
                month = "0" + xcalendar.Month.ToString();
            }
            else
            {
                month = xcalendar.Month.ToString();
            }

            string datastring = xcalendar.Year.ToString() + month + day + "T133000Z";

            var dataformat = (DateTime)xcalendar.SelectedDate;

            if (CalendarioModel.evento_copia.ContainsKey(dataformat))
            {
                if (CalendarioModel.evento_copia[dataformat].Count == 1)
                {
                    CalendarioModel.evento_copia.Remove(dataformat);
                    DisplayAlert("Atenção", @"Agendamento Excluído", "Ok");
                }
                else
                {

                    var events = CalendarioModel.evento_copia[dataformat] as ObservableCollection<Agendamento>;
                    Navigation.PushAsync(new ExcluirEvento(events));
                }
            }
        }

    }
}