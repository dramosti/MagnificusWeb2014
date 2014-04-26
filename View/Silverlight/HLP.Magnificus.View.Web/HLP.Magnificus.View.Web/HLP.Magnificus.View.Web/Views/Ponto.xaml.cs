using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using HLP.Entries.ViewModel.Commands.RecursosHumanos;
using HLP.Entries.ViewModel.ViewModels.RecursosHumanos;

namespace HLP.Magnificus.View.Web.Views
{
    public partial class Ponto : Page
    {
        public Ponto()
        {
            InitializeComponent();
            this.ViewModel = new Funcionario_Controle_Horas_PontoViewModel(new Action(setFoco));            
        }

        public void setFoco()
        {
            Dispatcher.BeginInvoke(() => { System.Windows.Browser.HtmlPage.Plugin.Focus(); });
            Dispatcher.BeginInvoke(() => { txtCodigo.Focus(); });
        }



        public Funcionario_Controle_Horas_PontoViewModel ViewModel
        {
            set { this.DataContext = value; }
            get { return this.DataContext as Funcionario_Controle_Horas_PontoViewModel; }
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
