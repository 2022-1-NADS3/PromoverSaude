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
    public partial class AdicionarEvento : ContentPage
    {
        public AdicionarEvento()
        {
            InitializeComponent();
        }
        private void Voltar_(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CalendarioDetail());
        }
    }
}