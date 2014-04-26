using System;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HLP.Comum.Infrastructure.Static;


namespace HLP.Comum.Model.Models
{
    public class modelBaseBasic : INotifyPropertyChanged, IDataErrorInfo
    {
        public readonly string[] ignore = { "Item", "Error" }; //Campos que não devem ser verificados no reflection de iniciaobjeto()

        public modelBaseBasic()
        {
        }


        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Validação de Dados

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public virtual string this[string columnName]
        {
            get
            {
                object valor = this.GetType().GetProperty(columnName).GetValue(this, null);
                string sMessage = string.Empty;
                if (sMessage == null)
                    if (columnName.ToUpper().Contains("XEMAIL"))
                        if (valor != null)
                            if (valor != "")
                                if (!(valor.ToString()).IsValidEmailAddress())
                                    sMessage = "Email inválido.";

                return sMessage;
            }
        }
        #endregion

       
    }
}
