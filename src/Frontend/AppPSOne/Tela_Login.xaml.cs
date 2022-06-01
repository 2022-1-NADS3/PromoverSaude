using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

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
            if (string.IsNullOrWhiteSpace(useremail.Text) || string.IsNullOrWhiteSpace(userpassword.Text))
            {
                await DisplayAlert("Atenção", @"Todos os campos devem ser preenchidos com valores válidos", "Ok");
            }
            else
            {
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
                var post = await httpClient.PostAsync(uri, content);
                var result = await post.Content.ReadAsStringAsync();
                JObject textresult = JsonConvert.DeserializeObject<JObject>(result);
                // exibe a saida no TextView 
                if (post.IsSuccessStatusCode && result.Contains(useremail.Text))
                {
                    DadosModel.EmailModel = textresult["user_email"].ToString();
                    DadosModel.NomeModel = textresult["user_name"].ToString();
                    DadosModel.SenhaModel = textresult["user_password"].ToString();
                    DadosModel.SexoModel = textresult["user_sex"].ToString();
                    DadosModel.UserId = (int)textresult["user_id"];
                    await Navigation.PushAsync(new Calendario());
                }
                else
                {
                    await DisplayAlert("Atenção", @"Login Inválido, tente novamente", "Ok");
                }
            }
        }
    }
}