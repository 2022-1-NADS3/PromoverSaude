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
    public class CalendarioModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        private CultureInfo _culture = CultureInfo.InvariantCulture;
        public CultureInfo Culture
        {
            get => _culture;
            set => SetProperty(ref _culture, value);
        }
        public EventCollection Events { get; set; }
        public void CarregarAgendamentos()
        {
            Culture = CultureInfo.CreateSpecificCulture("pt-BR");
            /*string datestring = listaPub[0].todo_date.Replace("-", "");
            datestring = datestring.Replace(":", "");
            datestring = datestring.Replace(".", "");
            datestring = datestring.Substring(0, 15) + "Z";
            var datahora = DateTime.ParseExact(datestring, "yyyyMMddTHHmmssZ", Culture);*/
            //acessa assim: convert[0].todo_date*/
            //foreach (DadosAgenda aExames in listaPub)
            //{
            //}
            string datestring = listaPub[0].todo_date.Replace("-", "");
            datestring = datestring.Replace(":", "");
            datestring = datestring.Replace(".", "");
            datestring = datestring.Substring(0, 15) + "Z";
            var datahora = DateTime.ParseExact(datestring, "yyyyMMddTHHmmssZ", Culture);
            var datateste = DateTime.ParseExact(datestring, "yyyyMMddTHHmmssZ", Culture);

            Events = new EventCollection{};

            foreach (DadosAgenda aExames in listaPub)
            {
                string dataok = aExames.todo_date.Replace("-", "");
                dataok = dataok.Replace(":", "");
                dataok = dataok.Replace(".", "");
                dataok = dataok.Substring(0, 15) + "Z";
                var dataform = DateTime.ParseExact(dataok, "yyyyMMddTHHmmssZ", Culture);

                Events.Add(dataform, new List<Agendamento> { new Agendamento { Nome = aExames.todo_title, Descricao = aExames.todo_description } });
            }
        }
        public CalendarioModel()
        {
            CarregarAgendamentos();
        }

    }
}


