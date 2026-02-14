using PropTechMaui.Models;
using PropTechMaui.Services;

namespace PropTechMaui.Pages;

public partial class PropertiesPage : ContentPage
{
    private readonly DataStore _dataStore;
    private readonly PropertyManager _manager;

    public PropertiesPage(DataStore dataStore, PropertyManager manager)
    {
        InitializeComponent();
        _dataStore = dataStore;
        _manager = manager;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshList();
    }

    private void RefreshList()
    {
        PropertiesList.ItemsSource = _dataStore.Properties
            .Select(p => new PropertyDisplayItem
            {
                Id = p.Id,
                Address = p.Address,
                RentDisplay = $"R {p.MonthlyRent:N0}"
            }).ToList();
    }

    private void OnAddProperty(object? sender, EventArgs e)
    {
        var id = NewIdEntry.Text?.Trim();
        var address = NewAddressEntry.Text?.Trim();
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(address))
            return;

        if (!decimal.TryParse(NewRentEntry.Text, out var rent) || rent <= 0)
            return;

        _dataStore.AddProperty(new Property(id, address, rent));
        NewIdEntry.Text = "";
        NewAddressEntry.Text = "";
        NewRentEntry.Text = "";
        RefreshList();
    }

    private async void OnGetPricing(object? sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is string propertyId)
        {
            var pricing = await _manager.GetPricingRecommendationAsync(propertyId);

            PricingTitle.Text = $"💰 AI Pricing Recommendation for {propertyId}";
            PricingRecommended.Text = $"Recommended Rent: R {pricing.RecommendedRent:N0} / month";
            PricingRange.Text = $"Range: R {pricing.MinRent:N0} – R {pricing.MaxRent:N0}";
            PricingConfidence.Text = $"Confidence: {pricing.ConfidenceScore:P0}";
            PricingFactors.Text = string.Join("\n", pricing.MarketFactors.Select(f => $"• {f}"));
            PricingResultFrame.IsVisible = true;
        }
    }
}

public class PropertyDisplayItem
{
    public string Id { get; set; } = "";
    public string Address { get; set; } = "";
    public string RentDisplay { get; set; } = "";
}
