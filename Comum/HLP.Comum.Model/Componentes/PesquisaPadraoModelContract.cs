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

namespace HLP.Comum.Model.Componentes
{
    public class PesquisaPadraoModelContract
    {
        public string COLUMN_NAME { get; set; }
        public string DATA_TYPE { get; set; }
        public string IS_NULLABLE { get; set; }
        public int CHARACTER_MAXIMUM_LENGTH { get; set; }

    }
}
