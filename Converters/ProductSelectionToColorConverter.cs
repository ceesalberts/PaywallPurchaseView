using System.Globalization;

namespace PaywallPurchaseView.Converters;

public class ProductSelectionToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string selectedProductId && parameter is string productId)
        {
            return selectedProductId == productId ? Color.FromArgb("#007AFF") : Color.FromArgb("#CCCCCC");
        }
        return Color.FromArgb("#CCCCCC");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
} 