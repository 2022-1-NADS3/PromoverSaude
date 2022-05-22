using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppPSOne
{
    /*-----Teste-----------*/
    public class Objeto
    {
        [JsonProperty("nome")]
        public string nome { get; set; }
            
        [JsonProperty("sobrenome")]
        public string sobrenome { get; set; }            

        [JsonProperty("idade")]
        public int idade { get; set; }

        [JsonProperty("altura")]
        public float altura { get; set; }
    }
    /*-----Teste-----------*/

    public partial class MainPage : ContentPage
    {
        /*------------ Teste -------------*/
        async void Dados(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            var resultados = await httpClient.GetStringAsync("http://132.1.2.21:3000/user");
            var resultadoFinal = JsonConvert.DeserializeObject<Objeto>(resultados);

            json.Text = resultados;
            userName.Text = resultadoFinal.nome;
            sureName.Text = resultadoFinal.sobrenome;
            userAge.Text = resultadoFinal.idade.ToString();
            userHeight.Text = resultadoFinal.altura.ToString();

        }
        /*-----Teste-----------*/

        public MainPage()
        {
            InitializeComponent();
        }
        private void Proximo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page1());
        }

        private void CadastroPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Cadastro());
        }

        private void Editar_Perfil(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MeuPerfil());
        }

        private void Adicionar_Arquivos(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdicionarArquivos());
        }

        private void Meus_Doc(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MeusDocumentos());
        }


    }
}
