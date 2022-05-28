using Java.Lang;
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

    public partial class Calendario : FlyoutPage
    {
        public Calendario()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as CalendarioFlyoutMenuItem;
            if (item == null)
                return;

            if (item.Title == "Meu Perfil")
            {
                Navigation.PushAsync(new MeuPerfil());
            
            } else if (item.Title == "Exames")
            {
                Navigation.PushAsync(new MeusDocumentos());
            
            } else if (item.Title == "Agenda")
            {
                Navigation.PushAsync(new Calendario());
            
            } else if (item.Title == "Sair")
            {
                Navigation.PushAsync(new MainPage());
            }

            FlyoutPage.ListView.SelectedItem = null;
        }
    }
}