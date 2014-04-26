using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace HLP.Comum.ViewModel.ViewModels
{
    public class ViewModelBase<T> : INotifyPropertyChanged where T : class
    {

        private T _currentModel;
        public T currentModel
        {
            get { return _currentModel; }
            set { _currentModel = value; NotifyPropertyChanged("currentModel"); }
        }


        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


    }
}
