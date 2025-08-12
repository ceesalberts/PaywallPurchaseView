using PaywallPurchaseView.ViewModels;

namespace PaywallPurchaseView.Views;

public partial class PurchaseView : ContentPage
{
    public PurchaseView(PurchaseViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
} 