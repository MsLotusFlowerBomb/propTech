using PropTechMaui.Models;
using PropTechMaui.Services;

namespace PropTechMaui.Pages;

public partial class VirtualToursPage : ContentPage
{
    private readonly DataStore _dataStore;
    private readonly PropertyManager _manager;

    public VirtualToursPage(DataStore dataStore, PropertyManager manager)
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
        ToursList.ItemsSource = _dataStore.Properties
            .Select(p =>
            {
                var tour = _dataStore.GetVirtualTourByPropertyId(p.Id);
                return new TourDisplayItem
                {
                    PropertyId = p.Id,
                    Address = p.Address,
                    RoomCount = tour != null ? $"{tour.Rooms.Count} rooms" : "No tour"
                };
            }).ToList();
    }

    private void OnViewTour(object? sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is string propertyId)
        {
            var tour = _dataStore.GetVirtualTourByPropertyId(propertyId);
            var prop = _dataStore.GetPropertyById(propertyId);
            if (tour == null || prop == null) return;

            TourHeader.Text = $"🔄 360° Tour — {prop.Address}";
            TourHeader.IsVisible = true;
            RoomsList.ItemsSource = tour.Rooms.Select(r => new
            {
                r.RoomName,
                r.Description,
                PanoramaUrl = $"📷 {r.PanoramaUrl}"
            }).ToList();

            if (tour.Inspection != null)
                ShowInspection(tour.Inspection, prop.Address);
        }
    }

    private async void OnRunInspection(object? sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is string propertyId)
        {
            var prop = _dataStore.GetPropertyById(propertyId);
            if (prop == null) return;

            var report = await _manager.RunVirtualTourInspectionAsync(propertyId);
            ShowInspection(report, prop.Address);
            RefreshList();
        }
    }

    private void ShowInspection(InspectionReport report, string address)
    {
        InspectionTitle.Text = $"🤖 AI Inspection — {address}";
        InspectionCondition.Text = $"Overall Condition: {report.OverallCondition}";
        InspectionScore.Text = $"Condition Score: {report.ConditionScore:P0}";
        InspectionCost.Text = $"Estimated Repairs: R {report.EstimatedRepairCost:N0}";
        InspectionConfidence.Text = $"AI Confidence: {report.ConfidenceScore:P0}";
        InspectionFindings.Text = string.Join("\n",
            report.Findings.Select(f => $"• [{f.Severity}] {f.Room}: {f.Issue} (R {f.EstimatedCost:N0})"));
        InspectionFrame.IsVisible = true;
    }
}

public class TourDisplayItem
{
    public string PropertyId { get; set; } = "";
    public string Address { get; set; } = "";
    public string RoomCount { get; set; } = "";
}
