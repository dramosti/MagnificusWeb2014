using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HLP.Magnificus.View.Web.Converters
{
    public class ConverterToBitimap : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //BitmapImage bi = null;
            //if (value != null)
            //{
            //    bi = new BitmapImage();
            //    bi.BeginInit();
            //    bi.StreamSource = new MemoryStream(buffer: value as byte[]);
            //    bi.EndInit();
            //}

            //BitmapImage GetImage( byte[] rawImageBytes )
            BitmapImage imageSource = null;
            if (value != null)
                try
                {
                    using (MemoryStream stream = new MemoryStream((byte[])value))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        BitmapImage b = new BitmapImage();
                        b.SetSource(stream);
                        imageSource = b;
                    }
                }
                catch (System.Exception ex)
                {
                }

            return imageSource;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
