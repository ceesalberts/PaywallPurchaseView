using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PaywallPurchaseView.Models;
using System.Collections.ObjectModel;

namespace PaywallPurchaseView.ViewModels;

public partial class PurchaseViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<string> _productIds = new();

    [ObservableProperty]
    private ObservableCollection<PurchaseProductDetails> _productDetails = new();

    [ObservableProperty]
    private bool _isSubscribed = false;

    [ObservableProperty]
    private bool _isPurchasing = false;

    [ObservableProperty]
    private bool _isFetchingProducts = false;

    [ObservableProperty]
    private string _selectedProductId = string.Empty;

    [ObservableProperty]
    private bool _freeTrial = true;

    [ObservableProperty]
    private bool _showCloseButton = false;

    [ObservableProperty]
    private double _progress = 0.0;

    [ObservableProperty]
    private double _shakeDegrees = 0.0;

    [ObservableProperty]
    private double _shakeZoom = 0.9;

    [ObservableProperty]
    private bool _showNoneRestoredAlert = false;

    [ObservableProperty]
    private bool _showTermsActionSheet = false;

    private readonly double _allowCloseAfter = 3.0; // time in seconds until close is allowed

    public PurchaseViewModel()
    {
        InitializeProducts();
        StartCooldownTimer();
        StartShakingAnimation();
    }

    private void InitializeProducts()
    {
        ProductIds.Add("demo_y");
        ProductIds.Add("demo_w");

        ProductDetails.Add(new PurchaseProductDetails(YearlyPlanPrice, "demo_y", YearlyPlanDuration, "Yearly Plan", false));
        ProductDetails.Add(new PurchaseProductDetails(WeeklyPlanPrice, "demo_w", WeeklyPlanDuration, "3-Day Trial", true));

        SelectedProductId = ProductIds.LastOrDefault() ?? string.Empty;
    }

    public string CallToActionText
    {
        get
        {
            var selectedProduct = ProductDetails.FirstOrDefault(p => p.ProductId == SelectedProductId);
            if (selectedProduct?.HasTrial == true)
            {
                return "Start Free Trial";
            }
            return "Unlock Now";
        }
    }

    public double? CalculateFullPrice
    {
        get
        {
            var weeklyProduct = ProductDetails.FirstOrDefault(p => p.Duration == "week");
            if (weeklyProduct != null && double.TryParse(weeklyProduct.Price.Replace("$", ""), out double weeklyPrice))
            {
                return weeklyPrice * 52;
            }
            return null;
        }
    }

    public int CalculatePercentageSaved
    {
        get
        {
            var fullPrice = CalculateFullPrice;
            var yearlyProduct = ProductDetails.FirstOrDefault(p => p.Duration == "year");
            
            if (fullPrice.HasValue && yearlyProduct != null && 
                double.TryParse(yearlyProduct.Price.Replace("$", ""), out double yearlyPrice))
            {
                var saved = (int)(100 - ((yearlyPrice / fullPrice.Value) * 100));
                return saved > 0 ? saved : 90;
            }
            return 90;
        }
    }

    public string YearlyPlanPrice => "$25.99";
    public string YearlyPlanDuration => "year";
    public string WeeklyPlanPrice => "$4.99";
    public string WeeklyPlanDuration => "week";
    public string WeeklyPlanTrialText => "then";

    // Feature texts
    public string Feature1Text => "Add first feature here";
    public string Feature2Text => "Then add second feature";
    public string Feature3Text => "Put final feature here";
    public string Feature4Text => "Remove annoying paywalls";
    
    // Product plan names
    public string YearlyPlanName => "Yearly Plan";
    public string WeeklyPlanName => "3-Day Trial";
    
    // UI text
    public string FreeTrialEnabledText => "Free Trial Enabled";
    public string RestoreText => "Restore";
    public string TermsText => "Terms of Use & Privacy Policy";
    
    // Main title and UI elements
    public string MainTitleText => "Unlock Premium Access";
    public string CloseButtonText => "✕";
    public string HeroIconText => "🎯";
    public string Feature1IconText => "★";
    public string Feature2IconText => "★";
    public string Feature3IconText => "★";
    public string Feature4IconText => "🔒";
    public string CheckmarkText => "✓";
    public string ArrowGlyph => "›";
    
    // Save badge text components
    public string SaveBadgePrefix => "SAVE";
    public string SaveBadgeSuffix => "%";

    [RelayCommand]
    private async Task PurchaseSubscription()
    {
        if (!IsPurchasing)
        {
            IsPurchasing = true;
            // Implement your purchase logic here
            // ....
            
            // For demo purposes, we'll simulate a purchase
            await Task.Delay(2000);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                IsPurchasing = false;
                IsSubscribed = true;
            });
        }
    }

    [RelayCommand]
    private async Task RestorePurchases()
    {
        // Show loading state
        IsPurchasing = true;
        
        // Simulate restore process
        await Task.Delay(2000);
        
        // For demo purposes, always show "no purchases restored"
        MainThread.BeginInvokeOnMainThread(() =>
        {
            IsPurchasing = false;
            ShowNoneRestoredAlert = true;
        });
    }

    [RelayCommand]
    private async Task ShowTerms()
    {
        var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page as Page;
        if (mainPage != null)
        {
            await mainPage.DisplayAlert("Terms", TermsText, "OK");
        }
    }

    [RelayCommand]
    private async Task ShowRestoreAlert()
    {
        var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page as Page;
        if (mainPage != null)
        {
            await mainPage.DisplayAlert("Restore Purchases", "No purchases restored", "OK");
        }
        ShowNoneRestoredAlert = false;
    }

    [RelayCommand]
    private async Task CloseView()
    {
        // Close the modal view
        var currentPage = Application.Current?.Windows.FirstOrDefault()?.Page;
        if (currentPage != null)
        {
            await currentPage.Navigation.PopModalAsync();
        }
    }

    [RelayCommand]
    private void SelectProduct(string productId)
    {
        SelectedProductId = productId;
    }

    private void StartCooldownTimer()
    {
        // Start the progress animation after a short delay
        Task.Delay(600).ContinueWith(_ =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Simple progress animation using Task.Delay
                AnimateProgress();
            });
        });
    }

    private async void AnimateProgress()
    {
        const int steps = 50;
        int delayMs = (int)(_allowCloseAfter * 1000 / steps);
        
        for (int i = 0; i <= steps; i++)
        {
            Progress = (double)i / steps;
            await Task.Delay(delayMs);
        }
        
        // Enable close button after the cooldown period
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ShowCloseButton = true;
            System.Diagnostics.Debug.WriteLine($"Close button enabled after {_allowCloseAfter} seconds");
        });
    }

    private void StartShakingAnimation()
    {
        Task.Delay(1000).ContinueWith(_ =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                PerformShakeAnimation();
            });
        });
    }

    private async void PerformShakeAnimation()
    {
        const double totalDuration = 0.7;
        const int numberOfShakes = 3;
        const double initialAngle = 10;

        // Zoom animation
        await AnimateZoom(0.9, 0.95, totalDuration / 2);
        await AnimateZoom(0.95, 0.9, totalDuration / 2);

        // Shake animation
        for (int i = 0; i < numberOfShakes; i++)
        {
            var angle = initialAngle - (initialAngle / numberOfShakes) * i;
            await AnimateRotation(0, angle, totalDuration / numberOfShakes / 2);
            await AnimateRotation(angle, -angle, totalDuration / numberOfShakes / 2);
        }

        // Stop shaking and reset
        await AnimateRotation(ShakeDegrees, 0, 0.3);
        
        // Schedule next shake
        await Task.Delay(1300);
        PerformShakeAnimation();
    }

    private async Task AnimateZoom(double from, double to, double duration)
    {
        const int steps = 10;
        int delayMs = (int)(duration * 1000 / steps);
        
        for (int i = 0; i <= steps; i++)
        {
            ShakeZoom = from + (to - from) * i / steps;
            await Task.Delay(delayMs);
        }
    }

    private async Task AnimateRotation(double from, double to, double duration)
    {
        const int steps = 10;
        int delayMs = (int)(duration * 1000 / steps);
        
        for (int i = 0; i <= steps; i++)
        {
            ShakeDegrees = from + (to - from) * i / steps;
            await Task.Delay(delayMs);
        }
    }

    partial void OnSelectedProductIdChanged(string value)
    {
        var selectedProduct = ProductDetails.FirstOrDefault(p => p.ProductId == value);
        if (selectedProduct != null)
        {
            FreeTrial = selectedProduct.HasTrial;
        }
        OnPropertyChanged(nameof(CallToActionText));
        OnPropertyChanged(nameof(CalculateFullPrice));
        OnPropertyChanged(nameof(CalculatePercentageSaved));
    }

    partial void OnFreeTrialChanged(bool value)
    {
        if (!value && ProductIds.Any())
        {
            SelectedProductId = ProductIds.First();
        }
        else if (value && ProductIds.Any())
        {
            SelectedProductId = ProductIds.Last();
        }
    }

    partial void OnShowNoneRestoredAlertChanged(bool value)
    {
        if (value)
        {
            ShowRestoreAlertCommand.Execute(null);
        }
    }
} 