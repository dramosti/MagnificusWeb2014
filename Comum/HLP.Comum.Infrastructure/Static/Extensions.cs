using System;
using System.Net;
using System.Text.RegularExpressions;
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
    public static class Extensions
    {
        #region isValid
        public static bool IsValidUrl(this string text)
        {
            Regex rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return rx.IsMatch(text);
        }

        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        public static bool IsValidIPAddress(this string s)
        {
            return Regex.IsMatch(s, @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
        }
        #endregion

        public static string ToStringHoras(this object value)
        {
            if (value != null)
            {
                int iTotHoras = (((TimeSpan)value).Days * 24) + ((TimeSpan)value).Hours;

                int iMinutes;
                if (((TimeSpan)value).Minutes < 0)
                    iMinutes = ((TimeSpan)value).Minutes * -1;
                else
                    iMinutes = ((TimeSpan)value).Minutes;
                return iTotHoras.ToString().PadLeft(2, '0') + ":"
                    + iMinutes.ToString().PadLeft(2, '0') + ":00";
            }
            return "00:00:00";
        }

        public static TimeSpan ToTimeSpan(this string Text)
        {
            string sSinal = (Text.Contains("-") ? "-" : "");
            return new TimeSpan
                (
                Convert.ToInt32(Text.Split(':')[0]),
                Convert.ToInt32(sSinal + Text.Split(':')[1]),
                Convert.ToInt32(sSinal + Text.Split(':')[2])
                );
        }
    }
}
