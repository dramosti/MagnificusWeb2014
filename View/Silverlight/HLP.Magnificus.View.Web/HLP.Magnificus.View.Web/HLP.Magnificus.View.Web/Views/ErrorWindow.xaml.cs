using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HLP.Magnificus.View.Web
{
    public partial class ErrorWindow : ChildWindow
    {

        public DispatcherTimer timerRefresh = new DispatcherTimer();


        public ErrorWindow(Exception e)
        {
            InitializeComponent();
            if (e != null)
            {
                ErrorTextBox.Text = e.Message + Environment.NewLine + Environment.NewLine + e.StackTrace;
            }

            timerRefresh.Interval = TimeSpan.FromSeconds(3);
            timerRefresh.Tick+=timerRefresh_Tick;
            timerRefresh.Start();

        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {            
            this.DialogResult = true;
            this.Close();
        }

        public ErrorWindow(Uri uri)
        {
            InitializeComponent();
            if (uri != null)
            {
                ErrorTextBox.Text = "Página não encontrada : \"" + uri.ToString() + "\"";
            }
        }

        public ErrorWindow(string message, string details)
        {
            InitializeComponent();
            ErrorTextBox.Text = Environment.NewLine + message + Environment.NewLine;// +Environment.NewLine + details;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}