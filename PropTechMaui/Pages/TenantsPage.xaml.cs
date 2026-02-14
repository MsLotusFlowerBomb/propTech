using PropTechMaui.Models;
using PropTechMaui.Services;

namespace PropTechMaui.Pages;

public partial class TenantsPage : ContentPage
{
    private readonly DataStore _dataStore;
    private readonly PropertyManager _manager;

    public TenantsPage(DataStore dataStore, PropertyManager manager)
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
        var tenants = _dataStore.Tenants
            .Select(t => new TenantDisplayItem { Id = t.Id, FullName = t.FullName })
            .ToList();

        TenantsList.ItemsSource = tenants;
        EmptyMessage.IsVisible = tenants.Count == 0;
    }

    private async void OnRegisterTenant(object? sender, EventArgs e)
    {
        var name = NewNameEntry.Text?.Trim();
        if (string.IsNullOrWhiteSpace(name)) return;

        RegisterButton.IsEnabled = false;
        ScreeningIndicator.IsVisible = true;
        ScreeningIndicator.IsRunning = true;

        var (tenant, screening) = await _manager.RegisterNewTenantAsync(name);

        ScreeningTitle.Text = $"🤖 AI Screening Result for {name}";
        ScreeningRisk.Text = $"Risk Level: {screening.Risk} (Score: {screening.RiskScore:F2})";
        ScreeningSummary.Text = $"Summary: {screening.Summary}";
        ScreeningFactors.Text = string.Join("\n", screening.Factors.Select(f => $"• {f}"));

        var riskColor = screening.Risk switch
        {
            RiskLevel.Low => Colors.Green,
            RiskLevel.Medium => Colors.Orange,
            RiskLevel.High => Colors.Red,
            _ => Colors.Gray
        };
        ScreeningResultFrame.BorderColor = riskColor;
        ScreeningResultFrame.IsVisible = true;

        NewNameEntry.Text = "";
        RefreshList();

        ScreeningIndicator.IsVisible = false;
        ScreeningIndicator.IsRunning = false;
        RegisterButton.IsEnabled = true;
    }
}

public class TenantDisplayItem
{
    public string Id { get; set; } = "";
    public string FullName { get; set; } = "";
}
