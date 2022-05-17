using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Plugin.Calendar.Models;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace AppPSOne
{
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

        public EventCollection Events { get; set; }

        private CultureInfo _culture = CultureInfo.InvariantCulture;
        public CultureInfo Culture
        {
            get => _culture;
            set => SetProperty(ref _culture, value);
        }

        public void CarregarAgendamentos()
        {
            Culture = CultureInfo.CreateSpecificCulture("pt-BR");

            Events = new EventCollection
            {
                [DateTime.Now] = new List<Agendamento>
            {
                new Agendamento { Nome = "Exame Cardíaco", Descricao = "Doutor Amilto - 13hrs" },
                new Agendamento { Nome = "Exame Colesterol", Descricao = "Clinicas - 14hrs" }
            },
                [DateTime.Now.AddDays(5)] = new List<Agendamento>
            {
                new Agendamento { Nome = "Exames de urina", Descricao = "Clinicas - 18hrs" },
                new Agendamento { Nome = "Exame Transaminases ", Descricao = "Doutora Renatta - 10hrs" }
            },
                [DateTime.Now.AddDays(-3)] = new List<Agendamento>
            {
                new Agendamento { Nome = "Dentista", Descricao = "Clareamento - 18hrs" }
            }
            };
        }

        public CalendarioModel()
        {
            CarregarAgendamentos();
        }

    }

    }

