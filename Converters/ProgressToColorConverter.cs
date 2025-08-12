using System.Globalization;

namespace PaywallPurchaseView.Converters;

public class ProgressToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double progress)
        {
            // Interpolate between gray and blue based on progress
            var gray = Color.FromArgb("#CCCCCC");
            var blue = Color.FromArgb("#007AFF");
            
            var red = gray.Red + (blue.Red - gray.Red) * progress;
            var green = gray.Green + (blue.Green - gray.Green) * progress;
            var blueComponent = gray.Blue + (blue.Blue - gray.Blue) * progress;
            
            return Color.FromRgb((int)red, (int)green, (int)blueComponent);
        }
        return Color.FromArgb("#CCCCCC");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
} 