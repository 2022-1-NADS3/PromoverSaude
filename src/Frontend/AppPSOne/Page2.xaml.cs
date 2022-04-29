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
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();
            if (Application.Current.Properties["Name"].ToString() == "Renato")
            {
                NomeAqui.Text = $"Seja bem vindo Sir {Application.Current.Properties["Name"].ToString()}!";
            }
            else
            {
                NomeAqui.Text = "Error";
            }
        }   
    }
}