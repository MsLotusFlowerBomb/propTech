using PropTechMaui.Models;
using PropTechMaui.Services;

namespace PropTechMaui.Pages;

public partial class MaintenancePage : ContentPage
{
    private readonly DataStore _dataStore;
    private readonly PropertyManager _manager;
    private readonly List<MaintenancePrediction> _predictions = new();

    public MaintenancePage(DataStore dataStore, PropertyManager manager)
    {
        InitializeComponent();
        _dataStore = dataStore;
        _manager = manager;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        PropertiesList.ItemsSource = _dataStore.Properties
            .Select(p => new { p.Id, p.Address })
            .ToList();
    }

    private async void OnPredictMaintenance(object? sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is string propertyId)
        {
            var prediction = await _manager.GetMaintenancePredictionAsync(propertyId);

            _predictions.RemoveAll(p => p.PropertyId == propertyId);
            _predictions.Add(prediction);

            var prop = _dataStore.GetPropertyById(propertyId);
            var address = prop?.Address ?? propertyId;

            PredictionsList.ItemsSource = _predictions.Select(p => new MaintenanceDisplayItem
            {
                Header = $"🔧 {_dataStore.GetPropertyById(p.PropertyId)?.Address ?? p.PropertyId}",
                UrgencyText = $"Urgency: {p.Urgency}",
                CostText = $"Estimated Cost: R {p.EstimatedCost:N0}",
                DaysText = $"Days Until Needed: {p.EstimatedDaysUntilNeeded}",
                Issue = $"Issue: {p.Issue}",
                ConfidenceText = $"Confidence: {p.ConfidenceScore:P0}"
            }).ToList();

            PredictionsHeader.IsVisible = true;
        }
    }
}

public class MaintenanceDisplayItem
{
    public string Header { get; set; } = "";
    public string UrgencyText { get; set; } = "";
    public string CostText { get; set; } = "";
    public string DaysText { get; set; } = "";
    public string Issue { get; set; } = "";
    public string ConfidenceText { get; set; } = "";
}
