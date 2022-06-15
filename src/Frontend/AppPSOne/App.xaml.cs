using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPSOne
{
    public partial class App : Application
    {
        public static bool UsuarioLogado { get; set; }
        public App()
        {
            InitializeComponent();

            if (!UsuarioLogado)
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new Calendario());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
