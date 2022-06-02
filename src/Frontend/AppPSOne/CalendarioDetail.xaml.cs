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

namespace AppPSOne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarioDetail : ContentPage, INotifyPropertyChanged
    {

        public CalendarioDetail()
        {
            InitializeComponent();
            GetList();
            //this.BindingContext = new CalendarioModel();
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

    }
}