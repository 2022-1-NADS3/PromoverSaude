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
        public MeusDocumentos()
        {
            InitializeComponent();
        }

        private void Voltar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        private void Adicionar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdicionarArquivos());
        }
    }
}