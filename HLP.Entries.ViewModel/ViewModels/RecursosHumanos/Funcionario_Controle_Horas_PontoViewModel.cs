using HLP.Comum.ViewModel.ViewModels;
using HLP.Entries.Model.RecursosHumanos;
using HLP.Entries.ViewModel.ViewModels.RecursosHumanos;
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
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using HLP.Entries.ViewModel.Commands.RecursosHumanos;

namespace HLP.Entries.ViewModel.ViewModels.RecursosHumanos
{
    public class Funcionario_Controle_Horas_PontoViewModel : ViewModelBase<FuncionarioModel>
    {
        public Funcionario_Controle_Horas_PontoCommand commands;
        public ICommand commandSave { get; set; }

        DispatcherTimer timerRelogio = new DispatcherTimer();
        public DispatcherTimer timerRefresh = new DispatcherTimer();
        Action foco;

        public Funcionario_Controle_Horas_PontoViewModel(Action foco)
        {
            this.foco = foco;
            timerRelogio.Interval = TimeSpan.FromSeconds(1);
            timerRelogio.Tick += (s, e) =>
            {
                UpdateTime();
            };
            timerRelogio.Start();

            timerRefresh.Interval = TimeSpan.FromSeconds(3);
            timerRefresh.Tick += (s, e) =>
                { RefreshPonto(); };

            this.currentModel = new FuncionarioModel();
            this.lponto = new ObservableCollection<Funcionario_Controle_Horas_PontoModel>();
            commands = new Funcionario_Controle_Horas_PontoCommand(viewModel: this);
            this.sData = DateTime.Today.ToString("dd/MM/yyyy");
            this.sDia = DateTime.Today.ToString("dddddd").ToUpper();
        }

        public void RefreshPonto()
        {
            this.currentModel = new FuncionarioModel();
            this.lponto = new ObservableCollection<Funcionario_Controle_Horas_PontoModel>();
            this.codigoPonto = string.Empty;
            this.bImageEntrada = Visibility.Collapsed;
            this.bImageSaida = Visibility.Collapsed;
            if (timerRefresh.IsEnabled)
                timerRefresh.Stop();
            foco.Invoke();
        }



        private ObservableCollection<Funcionario_Controle_Horas_PontoModel> _lponto;
        public ObservableCollection<Funcionario_Controle_Horas_PontoModel> lponto
        {
            get { return _lponto; }
            set { _lponto = value; base.NotifyPropertyChanged("lponto"); }
        }

        private string _codigoPonto = "";
        public string codigoPonto
        {
            get { return _codigoPonto; }
            set { _codigoPonto = value; base.NotifyPropertyChanged("codigoPonto"); }
        }

        private string _sData = "";
        public string sData
        {
            get { return _sData; }
            set { _sData = value; base.NotifyPropertyChanged("sData"); }
        }

        private string _sDia = "";
        public string sDia
        {
            get { return _sDia; }
            set { _sDia = value; base.NotifyPropertyChanged("sDia"); }
        }

        private string _sHora = "";
        public string sHora
        {
            get { return _sHora; }
            set { _sHora = value; base.NotifyPropertyChanged("sHora"); }
        }

        private Visibility _bImageSaida = Visibility.Collapsed;
        public Visibility bImageSaida
        {
            get { return _bImageSaida; }
            set { _bImageSaida = value; base.NotifyPropertyChanged("bImageSaida"); }
        }

        private Visibility _bImageEntrada = Visibility.Collapsed;
        public Visibility bImageEntrada
        {
            get { return _bImageEntrada; }
            set { _bImageEntrada = value; base.NotifyPropertyChanged("bImageEntrada"); }
        }




        #region Relógio
        private string _currenttime;
        public string CurrentTime
        {
            get { return _currenttime; }
            set { _currenttime = value; base.NotifyPropertyChanged("CurrentTime"); }
        }
        private TimeZoneInfo _selectedTimeZone;
        public TimeZoneInfo SelectedTimeZone
        {
            get { return _selectedTimeZone; }
            set
            {
                _selectedTimeZone = value;
                base.NotifyPropertyChanged("SelectedTimeZone");
                UpdateTime();
            }
        }
        private void UpdateTime()
        {
            CurrentTime = SelectedTimeZone == null
                   ? DateTime.Now.ToLongTimeString()
                   : DateTime.UtcNow.AddHours(SelectedTimeZone.BaseUtcOffset.TotalHours).ToLongTimeString();
        }
        #endregion

    }
}
