using System.Globalization;

namespace Antonava__353504.UI.ValueConverters
{
    internal class IdToImageSourceValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int id)
            {
                string imagesDirectory = Path.Combine(FileSystem.Current.AppDataDirectory, "Images");
                string filePath = Path.Combine(imagesDirectory, $"{id}.png"); 
                if (File.Exists(filePath))
                    return ImageSource.FromFile(filePath);
            }
            return ImageSource.FromFile("dotnet_bot.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
