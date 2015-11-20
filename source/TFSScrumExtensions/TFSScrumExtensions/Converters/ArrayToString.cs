using System;
using System.Linq;
using System.Windows.Data;

namespace JosePedroSilva.TFSScrumExtensions.Converters
{
    public class ArrayToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int[] array = value as int[];

            return String.Join(", ", array);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string arrayString = value as String;
            string[] array = arrayString.Split(new string[]{ ", " }, StringSplitOptions.RemoveEmptyEntries);

            int[] intArray = new int[array.Count()];
            int currentIteration = 0;
            foreach(string number in array)
            {
                intArray[currentIteration++] = Int32.Parse(number);
            }

            return intArray;
        }
    }
}
