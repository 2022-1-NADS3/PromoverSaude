using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPSOne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarioFlyout : ContentPage
    {
        public ListView ListView;

        public CalendarioFlyout()
        {
            InitializeComponent();

            BindingContext = new CalendarioFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class CalendarioFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<CalendarioFlyoutMenuItem> MenuItems { get; set; }

            public CalendarioFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<CalendarioFlyoutMenuItem>(new[]
                {
                    new CalendarioFlyoutMenuItem { Id = 0, Title = "Meu Perfil", Image = "perfil.png"},
                    new CalendarioFlyoutMenuItem { Id = 1, Title = "Agenda", Image = "calendario.png" },
                    new CalendarioFlyoutMenuItem { Id = 2, Title = "Exames", Image = "pasta.png" },
                    new CalendarioFlyoutMenuItem { Id = 3, Title = "Sair", Image = "sair.png" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}