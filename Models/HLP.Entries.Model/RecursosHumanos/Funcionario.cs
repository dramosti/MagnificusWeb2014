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
    public partial class FuncionarioModel : modelBaseBasic
    {

        private int _idFuncionario;
        [ParameterOrder(Order = 1)]
        public int idFuncionario
        {
            get { return _idFuncionario; }
            set { _idFuncionario = value; base.NotifyPropertyChanged("idFuncionario"); }
        }

        private string _xNome;
        [ParameterOrder(Order = 2)]
        public string xNome
        {
            get { return _xNome; }
            set { _xNome = value; base.NotifyPropertyChanged("xNome"); }
        }

        private DateTime _dAniversario;
        [ParameterOrder(Order = 3)]
        public DateTime dAniversario
        {
            get { return _dAniversario; }
            set { _dAniversario = value; base.NotifyPropertyChanged("dAniversario"); }
        }

        private string _xCargo;
        [ParameterOrder(Order = 4)]
        public string xCargo
        {
            get { return _xCargo; }
            set { _xCargo = value; base.NotifyPropertyChanged("xCargo"); }
        }


        private int _idEmpresa;
        [ParameterOrder(Order = 4)]
        public int idEmpresa
        {
            get { return _idEmpresa; }
            set { _idEmpresa = value; base.NotifyPropertyChanged("idEmpresa"); }
        }
        


        private TimeSpan _dHorasTrabalhadas = new TimeSpan();
        public TimeSpan dHorasTrabalhadas
        {
            get { return _dHorasTrabalhadas; }
            set
            {
                _dHorasTrabalhadas = value; base.NotifyPropertyChanged("dHorasTrabalhadas");
                this.dSaldo = _dHorasTrabalhadas.Subtract(dHorasAtrabalhar);
            }
        }

        private TimeSpan _dHorasAtrabalhar = new TimeSpan();
        public TimeSpan dHorasAtrabalhar
        {
            get { return _dHorasAtrabalhar; }
            set { _dHorasAtrabalhar = value; base.NotifyPropertyChanged("dHorasAtrabalhar"); }
        }
        private TimeSpan _dSaldo;

        public TimeSpan dSaldo
        {
            get { return _dSaldo; }
            set { _dSaldo = value; base.NotifyPropertyChanged("dSaldo"); }
        }


        private string _xObservacao = "";

        public string xObservacao
        {
            get { return _xObservacao; }
            set { _xObservacao = value; base.NotifyPropertyChanged("xObservacao"); }
        }


        private string _sMessageSimples = "";
        public string sMessageSimples
        {
            get { return _sMessageSimples; }
            set { _sMessageSimples = value; base.NotifyPropertyChanged("sMessageSimples"); }
        }

        private string _sMessageFull = "";
        public string sMessageFull
        {
            get { return _sMessageFull; }
            set { _sMessageFull = value; base.NotifyPropertyChanged("sMessageFull"); }
        }

        private int _idSequenciaInterna = 0;

        public int idSequenciaInterna
        {
            get { return _idSequenciaInterna; }
            set { _idSequenciaInterna = value; base.NotifyPropertyChanged("idSequenciaInterna"); }
        }

        private byte[] _imgFoto;

        public byte[] imgFoto
        {
            get { return _imgFoto; }
            set { _imgFoto = value;base.NotifyPropertyChanged("imgFoto"); }
        }
        
    }

    public partial class FuncionarioModel
    {

        public override string this[string columnName]
        {
            get
            {
                return base[columnName];
            }
        }
    }
}
