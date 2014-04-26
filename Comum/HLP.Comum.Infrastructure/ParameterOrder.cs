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

namespace HLP.Comum.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ParameterOrder : System.Attribute
    {
        public int Order { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PrimaryKey : System.Attribute
    {
        public bool isPrimary { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DontMap : System.Attribute
    {
        public bool bDontMap { get; set; }
    }
}
