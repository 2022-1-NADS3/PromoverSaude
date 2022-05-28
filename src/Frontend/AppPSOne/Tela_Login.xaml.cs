using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;

namespace AppPSOne
{
    public class DadosUsuario
    {
        [JsonProperty("useremail")]
        public string useremail { get; set; }

        [JsonProperty("userpassword")]
        public string userpassword { get; set; }

    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tela_Login : ContentPage
    {
        public Tela_Login()
        {
            InitializeComponent();
        }

        private void Voltar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        private void Entrar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Calendario());
        }

        async void Dados(object sender, EventArgs e)
        {
            /*var httpClient = new HttpClient();
            var resultados = await httpClient.GetStringAsync("https://fecap-promoversaude.herokuapp.com/users");
            Console.WriteLine(resultados);

            var resultadoFinal = JsonConvert.DeserializeObject<DadosUsuario>(resultados);

            Console.WriteLine(resultadoFinal);*/
            var httpClient = new HttpClient();
            var novoPost = new DadosUsuario
            {
                useremail = useremail.Text,
                userpassword = userpassword.Text,
            };

            // cria o conteudo da requisição e define o tipo Json
            var json = JsonConvert.SerializeObject(novoPost);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // envia a requisição POST
            var uri = "https://fecap-promoversaude.herokuapp.com/login_validacao";
            var result = await httpClient.PostAsync(uri, content);
            // Se ocorrer um erro lança uma exceção
            result.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                Navigation.PushAsync(new Calendario());
            }
        }
    }
}