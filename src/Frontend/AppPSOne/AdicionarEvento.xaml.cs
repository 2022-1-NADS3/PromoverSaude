using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPSOne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public class DadosExame
    {
        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        public TimePicker hora { get; set; }

        public DatePicker dataFormat { get; set; }

        [JsonProperty("dateExams")]
        public string dateExams { get; set; }

    }
    public partial class AdicionarEvento : ContentPage
    {
        public AdicionarEvento()
        {
            InitializeComponent();
        }
        private void Voltar_(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CalendarioDetail());
        }

        async void Cadastro_Exame(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(title.Text) || string.IsNullOrWhiteSpace(description.Text) ||
                string.IsNullOrWhiteSpace(hora.Time.ToString()) || string.IsNullOrWhiteSpace(dataFormat.Date.ToString()))
            {
                await DisplayAlert("Atenção", @"Todos os campos devem ser preenchidos com valores válidos", "Ok");
            }
            else
            {
                //Formato da data/hora: 2022-06-01T13: 00:00.000Z
                var data = FormatarData(hora.Time.ToString(), dataFormat.Date.ToString("yyyy/MM/dd"));
                var httpClient = new HttpClient();
                var novoPost = new DadosExame
                {
                    title = title.Text,
                    description = description.Text,
                    dateExams = data
                };

                // cria o conteudo da requisição e define o tipo Json
                var json = JsonConvert.SerializeObject(novoPost);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // envia a requisição POST
                var uri = "https://fecap-promoversaude.herokuapp.com/cadastrar_exames/" + DadosModel.UserId.ToString();
                var post = await httpClient.PostAsync(uri, content);
                var result = await post.Content.ReadAsStringAsync();
                // exibe a saida no TextView 
                if (post.IsSuccessStatusCode && result.Contains(description.Text))
                {
                    await DisplayAlert("Atenção", @"Exame cadastrado com sucesso", "Ok");
                    await Navigation.PushAsync(new Calendario());
                }
                else
                {
                    await DisplayAlert("Atenção", @"Todos os campos são obrigatórios", "Ok");
                }
            }
        }

        public string FormatarData(string Hora, string Data)
        {

            string dataFormatada = Data + "T" + Hora + ".000Z";

            return dataFormatada.Replace("/", "-");
        }

    }
}