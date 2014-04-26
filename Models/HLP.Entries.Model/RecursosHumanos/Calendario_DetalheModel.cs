using HLP.Comum.Infrastructure;
using HLP.Comum.Model.Models;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace HLP.Entries.Model.RecursosHumanos
{
    public class Calendario_DetalheModel : modelBaseBasic
    {
        private int? _idCalendarioDetalhe;
        [ParameterOrder(Order = 1), PrimaryKey(isPrimary = true)]
        public int? idCalendarioDetalhe
        {
            get { return _idCalendarioDetalhe; }
            set
            {
                _idCalendarioDetalhe = value;
                base.NotifyPropertyChanged(propertyName: "idCalendarioDetalhe");
            }
        }
        private DateTime _dCalendario;
        [ParameterOrder(Order = 2)]
        public DateTime dCalendario
        {
            get { return _dCalendario; }
            set
            {
                _dCalendario = value;
                base.NotifyPropertyChanged(propertyName: "dCalendario");
            }
        }
        private TimeSpan _tHoraInicio;
        [ParameterOrder(Order = 3)]
        public TimeSpan tHoraInicio
        {
            get { return _tHoraInicio; }
            set
            {
                _tHoraInicio = value;
                base.NotifyPropertyChanged(propertyName: "dHoraInicio");
            }
        }
        private TimeSpan _tHoraTermino;
        [ParameterOrder(Order = 4)]
        public TimeSpan tHoraTermino
        {
            get { return _tHoraTermino; }
            set
            {
                _tHoraTermino = value;
                base.NotifyPropertyChanged(propertyName: "dHoraTermino");
            }
        }
        private int _idCalendario;
        [ParameterOrder(Order = 5)]
        public int idCalendario
        {
            get { return _idCalendario; }
            set
            {
                _idCalendario = value;
                base.NotifyPropertyChanged(propertyName: "idCalendario");
            }
        }

    }
}
