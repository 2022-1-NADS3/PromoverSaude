using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppPSOne
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void Proximo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Tela_Login());
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
