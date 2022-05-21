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

    public partial class MeuPerfil : ContentPage
    {
        public MeuPerfil()
        {
            InitializeComponent();
            this.BindingContext = new EditarDados { Enable = true, Texto = "Editar Dados" };
        }

        private void Voltar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void Editar_Perfil(object sender, EventArgs e)
        {
            //Trocar o texto do botão
        }

        public async void Dados(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            var resultados = await httpClient.GetStringAsync("https://promover-saude.herokuapp.com/user");
            var resultadoFinal = JsonConvert.DeserializeObject<MeuPerfil>(resultados);

            nome.Text = resultadoFinal.nome.ToString();
            email.Text = resultadoFinal.email.ToString();
            senha.Text = resultadoFinal.senha.ToString();
            sexo.SelectedItem = resultadoFinal.sexo.ToString();
        }

    }
}