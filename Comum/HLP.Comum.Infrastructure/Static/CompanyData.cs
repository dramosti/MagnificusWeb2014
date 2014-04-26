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

namespace HLP.Comum.Infrastructure.Static
{
    public  static class CompanyData
    {
        private static int _idEmpresa = 1;

        public static int idEmpresa
        {
            get { return CompanyData._idEmpresa; }
            set { CompanyData._idEmpresa = value; }
        }
        public static string xNome { get; set; }
        public static string xFantasia { get; set; }
        public static string xLinqLogoEmpresa { get; set; }

    }
}
