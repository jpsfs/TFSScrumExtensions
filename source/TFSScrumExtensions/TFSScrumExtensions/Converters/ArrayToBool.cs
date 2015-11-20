using System;
using System.Windows.Data;

namespace JosePedroSilva.TFSScrumExtensions.Converters
{
    public class ArrayToBool : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int[] array = value as int[];
            if(array != null && array.Length > 0)
            {
                return false;
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
