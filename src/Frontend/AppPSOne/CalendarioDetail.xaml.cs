using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPSOne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarioDetail : ContentPage
    {
        public CalendarioDetail()
        {
            InitializeComponent();
        }

        private void CalendarView_DateSelectionChanged(object sender, EventArgs arg)
        {
            DisplayAlert("Date Changed", calendar.SelectedDates.ToString(), "OK");
        }
    }
}