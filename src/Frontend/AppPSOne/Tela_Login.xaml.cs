﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPSOne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tela_Login : ContentPage
    {
        public Tela_Login()
        {
            InitializeComponent();
        }

        private void Voltar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        private void Entrar(object sender, EventArgs e)
        {
            Application.Current.Properties["Name"] = Login.Text;
            Navigation.PushAsync(new Calendario());
        }
    }
}