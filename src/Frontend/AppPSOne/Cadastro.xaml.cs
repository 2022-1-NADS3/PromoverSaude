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
    public class CadastroUsuario 
    {
        [JsonProperty("useremail")]
        public string useremail { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("userpassword")]
        public string userpassword { get; set; }

        [JsonProperty("usersex")]
        public string usersex { get; set; }

    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastro : ContentPage
    {

        public Cadastro()
        {
            InitializeComponent();
        }

        private void Voltar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        async void Cad(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(email.Text) || string.IsNullOrWhiteSpace(senha.Text) || string.IsNullOrWhiteSpace(sexo.Text) || string.IsNullOrWhiteSpace(nome.Text))
            {
                await DisplayAlert("Atenção", @"Todos os campos devem ser preenchidos com valores válidos", "Ok");
            }
            else
            {

                var httpClient = new HttpClient();
                var novoPost = new CadastroUsuario
                {
                    useremail = email.Text,
                    userpassword = senha.Text,
                    usersex = sexo.Text,
                    username = nome.Text

                };

                // cria o conteudo da requisição e define o tipo Json
                var json = JsonConvert.SerializeObject(novoPost);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // envia a requisição POST
                var uri = "https://fecap-promoversaude.herokuapp.com/cadastrar_usuarios";
                var post = await httpClient.PostAsync(uri, content);
                var result = await post.Content.ReadAsStringAsync();
                // exibe a saida no TextView 
                if (post.IsSuccessStatusCode && result.Contains(email.Text))
                {
                    await Navigation.PushAsync(new Tela_Login());
                }
                else
                {
                    await DisplayAlert("Atenção", @"E-mail já cadastrado, por favor utilize outro", "Ok");
                }
            }
        }

    }
}