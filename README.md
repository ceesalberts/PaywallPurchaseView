# Paywall-PurchaseView for .NET-MAUI

**Version:** 1.0  
**Target Framework:** .NET 9.0  
**MAUI Version:** Latest stable

PurchaseView is a .NET MAUI view for implementing a premium access purchase screen in your cross-platform app. This screen offers a user-friendly way to present subscription options, handle purchase transactions, and restore purchases using the MVVM architectural pattern. 

This .NET MAUI version is based on the original SwiftUI implementation created by Adam Lyttle.

## Supported Platforms

This application is built with .NET MAUI and supports the following platforms:
- **iOS** - iPhone and iPad devices
- **Android** - Android phones and tablets  
- **macOS** - Mac computers via MacCatalyst

The application automatically adapts to each platform's design guidelines and provides a native look and feel across all supported devices.

## Features

* **Cross-Platform Support**: Works on iOS, Android, macOS, and Windows
* **MVVM Architecture**: Clean separation of concerns using CommunityToolkit.Mvvm
* **Customizable UI**: Easily modify the view to fit your app's design using XAML
* **Animated Hero Image**: Includes a shaking effect for the hero image
* **Dynamic Content**: Displays product details dynamically based on the available purchase options
* **Subscription Management**: Has placeholders for purchasing and restoring subscriptions
* **Trial Support**: Displays trial information and adjusts the call to action accordingly
* **Cooldown Timer**: Prevents immediate closing of the purchase view, encouraging interaction
* **Resource Dictionary**: Centralized styling with colors, fonts, and spacing in App.xaml

## Architecture

This project follows the MVVM (Model-View-ViewModel) pattern:

* **Models**: `PurchaseProductDetails.cs` - Data models for product information
* **ViewModels**: `PurchaseViewModel.cs` - Business logic and state management
* **Views**: `PurchaseView.xaml` - UI presentation using XAML
* **Converters**: Custom value converters for data transformation

## How to Use

The best way to present `PurchaseView` is using modal navigation. Here's an example:

```csharp
private async void OnShowPaywallClicked(object? sender, EventArgs e)
{
    var purchaseViewModel = new PurchaseViewModel();
    var purchaseView = new PurchaseView(purchaseViewModel);
    
    await Navigation.PushModalAsync(purchaseView);
}
```

## Customization

### Customizing Features

The features displayed in the PurchaseView can be customized by modifying the properties in `PurchaseViewModel.cs`:

```csharp
// Feature texts
public string Feature1Text => "Add first feature here";
public string Feature2Text => "Then add second feature";
public string Feature3Text => "Put final feature here";
public string Feature4Text => "Remove annoying paywalls";
```

To customize, simply change the return values of these properties:

```csharp
public string Feature1Text => "Your Custom Feature";
```

### Customizing the Title

The main title displayed in the PurchaseView can be customized by modifying the `MainTitleText` property in `PurchaseViewModel.cs`:

```csharp
public string MainTitleText => "Unlock Premium Access";
```

To customize, simply change the return value of this property:

```csharp
public string MainTitleText => "Your Custom Title";
```
```

### Customizing All Text Content

All text content in the PurchaseView can be customized by modifying properties in `PurchaseViewModel.cs`:

```csharp
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
```

To customize any text, simply change the return values of these properties.

**Note:** The save badge text uses FormattedText with separate properties for "SAVE", the percentage value, and "%" to allow full customization through the ViewModel.

For example, to change the save badge text:
```csharp
public string SaveBadgePrefix => "DISCOUNT";
public string SaveBadgeSuffix => " OFF";
```
This would display "DISCOUNT 25 OFF" instead of "SAVE 25%".

### Customizing Styling

All colors, fonts, and spacing are centralized in `App.xaml`:

```xml
<!-- Colors -->
<Color x:Key="PrimaryBlue">#007AFF</Color>
<Color x:Key="PrimaryOrange">#FF9500</Color>
<Color x:Key="PrimaryRed">#FF3B30</Color>

<!-- Font Sizes -->
<x:Double x:Key="TitleFontSize">28</x:Double>
<x:Double x:Key="FeatureTextSize">17</x:Double>
```

## Customizing Purchase Functionality

### PurchaseViewModel.cs

The `PurchaseViewModel` class manages the purchase data and state using CommunityToolkit.Mvvm. You can customize the purchase functionality by modifying methods in this class.

### Initializing Products

You can set up your product IDs and details in the `InitializeProducts()` method:

```csharp
private void InitializeProducts()
{
    ProductIds.Add("your_product_id_1");
    ProductIds.Add("your_product_id_2");

    ProductDetails.Add(new PurchaseProductDetails("$9.99", "your_product_id_1", "month", "Monthly Plan", true));
    ProductDetails.Add(new PurchaseProductDetails("$99.99", "your_product_id_2", "year", "Yearly Plan", false));

    SelectedProductId = ProductIds.LastOrDefault() ?? string.Empty;
}
```

Replace "your_product_id_1", "your_product_id_2", and the corresponding details with your actual product information.

### Handling Purchases

To customize the purchase process, modify the `PurchaseSubscription` method:

```csharp
[RelayCommand]
private async Task PurchaseSubscription()
{
    if (!IsPurchasing)
    {
        IsPurchasing = true;
        // Implement your purchase logic here
        await Task.Delay(2000); // Simulate purchase
        IsPurchasing = false;
        IsSubscribed = true;
    }
}
```

Insert your in-app purchase logic within this method to handle the subscription process.

### Restoring Purchases

To customize the restore purchases functionality, modify the `RestorePurchases` method:

```csharp
[RelayCommand]
private async Task RestorePurchases()
{
    IsPurchasing = true;
    // Implement your restore logic here
    await Task.Delay(2000); // Simulate restore
    IsPurchasing = false;
    ShowNoneRestoredAlert = true;
}
```

Insert your restore purchases logic within this method to handle restoring previous purchases.

## Dependencies

This project uses the following NuGet packages:

* **CommunityToolkit.Mvvm**: Version 8.4.0 - For MVVM implementation with ObservableObject and RelayCommand
* **Microsoft.Maui.Controls**: Latest stable version - For cross-platform UI controls
* **Microsoft.Extensions.Logging.Debug**: Version 9.0.0 - For debug logging support

## Project Structure

```
PaywallPurchaseView/
├── Models/
│   └── PurchaseProductDetails.cs
├── ViewModels/
│   └── PurchaseViewModel.cs
├── Views/
│   ├── PurchaseView.xaml
│   └── PurchaseView.xaml.cs
├── Converters/
│   ├── BoolToOpacityConverter.cs
│   ├── InverseBoolConverter.cs
│   ├── ProductSelectionToColorConverter.cs
│   ├── ProductSelectionToBackgroundConverter.cs
│   ├── ProductSelectionToVisibilityConverter.cs
│   └── ProgressToColorConverter.cs
├── App.xaml
├── MainPage.xaml
└── PaywallPurchaseView.sln
```

## Key Features Explained

### Cooldown Timer
The view implements a 3-second cooldown timer that prevents users from immediately closing the purchase view. During this time, the close button is greyed out and becomes fully functional afterward.

### Animated Hero Image
The hero image (🎯) includes a continuous shaking animation to draw attention and create visual interest.

### Dynamic Product Selection
Users can select between different subscription plans, with visual feedback showing the selected option and calculated savings.

### Responsive Design
The UI adapts to different screen sizes and orientations using MAUI's responsive layout system.

## About

**Original SwiftUI Version:** Created by Adam Lyttle

**Twitter:** [https://x.com/adamlyttleapps](adamlyttleapps)

**About this Project:** This .NET MAUI adaptation provides the same functionality as the original SwiftUI version, with cross-platform support and modern C# development practices. The design, layout, and user experience are based on Adam Lyttle's original SwiftUI implementation.

## Contributions

Contributions are welcome! Feel free to open an issue or submit a pull request on the GitHub repository.

## MIT License

This project is licensed under the MIT License. See the LICENSE file for more details.

This README provides a clear overview of the .NET MAUI project, detailed usage instructions, and additional sections like examples, contributions, and licensing, making it comprehensive and user-friendly for .NET developers. 
