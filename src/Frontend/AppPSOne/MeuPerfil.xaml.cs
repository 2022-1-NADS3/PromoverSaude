using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

    public static class DadosModel
    {
        public static string NomeJson { get; set; }
        public static string EmailJson { get; set; }
        public static string SenhaJson { get; set; }
        public static string SexoJson { get; set; }
        public static int UserId { get; set; }

    }
    public partial class MeuPerfil : ContentPage
    {

        public MeuPerfil()
        {
            InitializeComponent();
            Nome.Text = DadosModel.NomeJson;
            Email.Text = DadosModel.EmailJson;
            Senha.Text = DadosModel.SenhaJson;
            Sexo.Text = DadosModel.SexoJson;
        }

        private void Voltar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        async void Editar_Perfil(object sender, EventArgs e)
        {
            if (Text_Botao.Text == "Editar Perfil")
            {
                Email.IsEnabled = true;
                Senha.IsEnabled = true;
                Text_Botao.Text = "Salvar";
            } else
            {
                /*var httpClient = new HttpClient();
                var novoPost = new DadosUsuario
                {
                    useremail = Email.Text,
                    userpassword = Senha.Text,
                };

                // cria o conteudo da requisição e define o tipo Json
                var json = JsonConvert.SerializeObject(novoPost);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // envia a requisição POST
                var uri = "https://fecap-promoversaude.herokuapp.com/alterar_usuario/" + DadosModel.UserId.ToString();
                var post = await httpClient.PatchAsync(uri, content);
                var result = await post.Content.ReadAsStringAsync();
                JObject textresult = JsonConvert.DeserializeObject<JObject>(result);
                // exibe a saida no TextView 
                if (post.IsSuccessStatusCode && result.Contains(Email.Text))
                {
                    await DisplayAlert("Atenção", @"Dados Salvos", "Ok");
                }*/

                Text_Botao.Text = "Editar Perfil";
                Email.IsEnabled = false;
                Senha.IsEnabled = false;
            }
        }

    }
}