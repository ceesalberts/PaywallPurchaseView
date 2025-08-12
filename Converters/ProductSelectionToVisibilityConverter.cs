using System.Globalization;

namespace PaywallPurchaseView.Converters;

public class ProductSelectionToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string selectedProductId && parameter is string productId)
        {
            return selectedProductId == productId;
        }
        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
} 