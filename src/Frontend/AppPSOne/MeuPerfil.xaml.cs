using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPSOne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class MeuPerfil : ContentPage
    {
        public static DateTime Today { get; }
        public MeuPerfil()
        {
            InitializeComponent();
            this.BindingContext = new EditarDados { Enable = true, Texto = "Editar Dados" };
        }

        private string DataAtual(object sender, EventArgs e)
        {
            DateTime thisDay = DateTime.Today;
            return thisDay.ToString("d");
        }

        private void Voltar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void Editar_Perfil(object sender, EventArgs e)
        {
            //Trocar o texto do botão
        }

    }
}