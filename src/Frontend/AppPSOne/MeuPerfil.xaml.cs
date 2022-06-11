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
        public static string NomeModel { get; set; }
        public static string EmailModel { get; set; }
        public static string SenhaModel { get; set; }
        public static string SexoModel { get; set; }
        public static int UserId { get; set; }

    }

    public class DadosJson
    {
        [JsonProperty("user_name")]
        public  string user_name { get; set; }
        [JsonProperty("user_email")]
        public  string user_email { get; set; }
        [JsonProperty("user_password")]
        public  string user_password { get; set; }
        [JsonProperty("user_sex")]
        public  string user_sex { get; set; }
    }

    public partial class MeuPerfil : ContentPage
    {

        public MeuPerfil()
        {
            InitializeComponent();
            Nome.Text = DadosModel.NomeModel;
            Email.Text = DadosModel.EmailModel;
            Senha.Text = DadosModel.SenhaModel;
            Sexo.Text = DadosModel.SexoModel;
        }

        private void Voltar(object sender, EventArgs e)
        {
            Nome.Text = "";
            DadosModel.NomeModel = "";
            Email.Text = "";
            DadosModel.EmailModel = "";
            Senha.Text = "";
            DadosModel.SenhaModel = "";
            Sexo.Text = "";
            DadosModel.SexoModel = "";
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
                if (Email.Text != DadosModel.EmailModel || Senha.Text != DadosModel.SenhaModel)
                {
                    var httpClient = new HttpClient();
                    var novoPost = new DadosJson
                    {
                        user_name = Nome.Text,
                        user_email = Email.Text,
                        user_password = Senha.Text,
                        user_sex = Sexo.Text
                    };

                    // cria o conteudo da requisição e define o tipo Json
                    var json = JsonConvert.SerializeObject(novoPost);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    // envia a requisição POST
                    var uri = "https://fecap-promoversaude.herokuapp.com/alterar_usuario/" + DadosModel.UserId.ToString();
                    var post = await httpClient.PutAsync(uri, content);
                    var result = await post.Content.ReadAsStringAsync();
                    // exibe a saida no TextView 
                    if (post.IsSuccessStatusCode && result.Contains(Email.Text))
                    {
                        await DisplayAlert("Atenção", @"Dados Salvos", "Ok");
                    }
                }

                Text_Botao.Text = "Editar Perfil";
                Email.IsEnabled = false;
                Senha.IsEnabled = false;
            }
        }

    }
}