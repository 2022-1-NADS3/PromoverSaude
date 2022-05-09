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
    public partial class MeusDocumentos : ContentPage
    {
        public AdicionarArquivos()
        {
            InitializeComponent();
        }

        private void Voltar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        private void Carregar(object sender, EventArgs e)
        {
            //Abrir arquivos do aparelho para escolher um documento
        }

        private void Salvar(object sender, EventArgs e)
        {
            //Salva alteracoes
        }
    }
}