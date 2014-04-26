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
using System.Windows.Threading;

namespace HLP.Comum.Viewcs
{
    public partial class MessageWindow : ChildWindow
    {
               
        public DispatcherTimer timerRefresh = new DispatcherTimer();
        public MessageWindow(string _Header, string _Message)
        {
            InitializeComponent();
            this.Title = _Header;
            this.MessageTextBox.Text = _Message;
            timerRefresh.Interval = TimeSpan.FromSeconds(3);
            timerRefresh.Tick += timerRefresh_Tick;
            timerRefresh.Start();
        }
        private void timerRefresh_Tick(object sender, EventArgs e)
        {            
            this.Close();
            this.DialogResult = true;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

    }
}

