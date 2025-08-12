using PaywallPurchaseView.ViewModels;
using PaywallPurchaseView.Views;

namespace PaywallPurchaseView;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnShowPaywallClicked(object? sender, EventArgs e)
	{
		var purchaseViewModel = new PurchaseViewModel();
		var purchaseView = new PurchaseView(purchaseViewModel);
		
		await Navigation.PushModalAsync(purchaseView);
	}
}
