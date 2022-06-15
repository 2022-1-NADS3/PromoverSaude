using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Plugin.Calendar.Models;
using System.Runtime.CompilerServices;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.Azure.Management.ContainerInstance.Fluent.Models;
using System.Linq;
using System.Collections;

namespace AppPSOne
{
    public class DadosAgenda
    {
        [JsonProperty("todo_description")]
        public string todo_description { get; set; }

        [JsonProperty("todo_title")]
        public string todo_title { get; set; }

        [JsonProperty("todo_id")]
        public string todo_id { get; set; }

        [JsonProperty("todo_done")]
        public string todo_done { get; set; }

        [JsonProperty("todo_date")]
        public string todo_date { get; set; }

        [JsonProperty("user_id")]
        public string user_id { get; set; }

    }

    public class DadosItem
    {
        public string todo_description { get; set; }

        public string todo_title { get; set; }

        public string todo_id { get; set; }
        public string hora { get; set; }

    }

    public class CalendarioModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static EventCollection evento_copia;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        public static List<DadosAgenda> listaPub { get; set; }

        public static List<DadosItem> listaItem { get; set; }

        private CultureInfo _culture = CultureInfo.InvariantCulture;

        public CultureInfo Culture
        {
            get => _culture;
            set => SetProperty(ref _culture, value);
        }

        public EventCollection Events { get; set; }
        public static List<Agendamento> listAgenda { get; set; }

        public void CarregarAgendamentos()
        {
            Culture = CultureInfo.CreateSpecificCulture("pt-BR");

            Events = new EventCollection{};

            foreach (DadosAgenda aLista in listaPub)
            {
                string dataok = aLista.todo_date.Replace("-", "");
                string hora = dataok.Substring(9, 5);
                dataok = dataok.Replace(":", "");
                dataok = dataok.Replace(".", "");
                dataok = dataok.Substring(0, 15) + "Z";
                var dataformat = DateTime.ParseExact(dataok, "yyyyMMddTHHmmssZ", Culture);
                dataformat = dataformat.AddSeconds(30);

                if (Events.ContainsKey(dataformat))
                {
                    var events = Events[dataformat] as ObservableCollection<Agendamento>;
                    events.Add(new Agendamento { Nome = aLista.todo_title + " - " + hora, Descricao = aLista.todo_description, Id= aLista.todo_id});
                }
                else
                {
                    Events.Add(dataformat, new ObservableCollection<Agendamento> { new Agendamento { Nome = aLista.todo_title + " - " + hora, Descricao = aLista.todo_description, Id = aLista.todo_id } });
                }

            };
            evento_copia = Events;
        }

        public CalendarioModel()
        {
            CarregarAgendamentos();
        }

    }

}


