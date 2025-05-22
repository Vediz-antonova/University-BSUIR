using System.Globalization;

namespace Antonava__353504.UI.ValueConverters
{
    internal class EntranceFeeToColorValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double fee)
            {
                if (fee < 5.000)
                    return Colors.LightPink;
                return Colors.DarkBlue;
            }
            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
