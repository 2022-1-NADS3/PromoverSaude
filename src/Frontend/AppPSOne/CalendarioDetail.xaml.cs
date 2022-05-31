using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Collections.Generic;
using Xamarin.Plugin.Calendar.Models;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace AppPSOne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarioDetail : ContentPage, INotifyPropertyChanged
    {

        public CalendarioDetail()
        {
            InitializeComponent();
            this.BindingContext = new CalendarioModel();
        }
        private void Adicionar_Evento(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdicionarEvento());
        }

    }
}