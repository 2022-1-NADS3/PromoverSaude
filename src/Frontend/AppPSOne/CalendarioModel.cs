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
                new Agendamento { Nome = "Ajuda", Descricao = "Levar o Milton Cortar o Cabelo" },
                new Agendamento { Nome = "Artigos", Descricao = "Escrever Artigo de Calendario" }
            },
                [DateTime.Now.AddDays(5)] = new List<Agendamento>
            {
                new Agendamento { Nome = "Tweet", Descricao = "Postar Algo no Twitter estilo bloguerinho" },
                new Agendamento { Nome = "Facebook", Descricao = "Postar Foto de Comida no Facebook" }
            },
                [DateTime.Now.AddDays(-3)] = new List<Agendamento>
            {
                new Agendamento { Nome = "Dentista", Descricao = "Manutenção na Dentadura" }
            },
                [new DateTime(2020, 5, 19)] = new List<Agendamento>
            {
                new Agendamento { Nome = "Microsoft Build", Descricao = "Vai Começar o Build" }
            }
            };
        }

        public CalendarioModel()
        {
            CarregarAgendamentos();
        }
        public class Agendamento
        {
            public string Nome { get; set; }
            public string Descricao { get; set; }
        }

    }

    }

