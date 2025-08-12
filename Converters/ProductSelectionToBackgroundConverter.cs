using System.Globalization;

namespace PaywallPurchaseView.Converters;

public class ProductSelectionToBackgroundConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string selectedProductId && parameter is string productId)
        {
            return selectedProductId == productId ? Color.FromArgb("#E3F2FD") : Colors.Transparent;
        }
        return Colors.Transparent;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
} 