using CommunityToolkit.Mvvm.ComponentModel;

namespace PaywallPurchaseView.Models;

public partial class PurchaseProductDetails : ObservableObject
{
    [ObservableProperty]
    private string _price = string.Empty;

    [ObservableProperty]
    private string _productId = string.Empty;

    [ObservableProperty]
    private string _duration = string.Empty;

    [ObservableProperty]
    private string _durationPlanName = string.Empty;

    [ObservableProperty]
    private bool _hasTrial = false;

    public PurchaseProductDetails()
    {
    }

    public PurchaseProductDetails(string price, string productId, string duration, string durationPlanName, bool hasTrial)
    {
        Price = price;
        ProductId = productId;
        Duration = duration;
        DurationPlanName = durationPlanName;
        HasTrial = hasTrial;
    }
} 