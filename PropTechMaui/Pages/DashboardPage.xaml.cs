using PropTechMaui.Models;
using PropTechMaui.Services;

namespace PropTechMaui.Pages;

public partial class DashboardPage : ContentPage
{
    private readonly DataStore _dataStore;
    private readonly AIPropertyAgent _aiAgent;

    public DashboardPage(DataStore dataStore, AIPropertyAgent aiAgent)
    {
        InitializeComponent();
        _dataStore = dataStore;
        _aiAgent = aiAgent;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshCounts();
    }

    private void RefreshCounts()
    {
        PropertyCount.Text = _dataStore.Properties.Count.ToString();
        TenantCount.Text = _dataStore.Tenants.Count.ToString();
        LeaseCount.Text = _dataStore.Leases.Count.ToString();
        InsightCount.Text = _aiAgent.InsightHistory.Count.ToString();
    }

    private async void OnRunAnalysis(object? sender, EventArgs e)
    {
        AnalyseButton.IsEnabled = false;
        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;

        var insights = await _aiAgent.RunPortfolioAnalysisAsync();

        var displayItems = insights.Select(i => new InsightDisplayItem
        {
            Title = $"{GetCategoryIcon(i.Category)} {i.Title}",
            Description = i.Description,
            ConfidenceText = $"Confidence: {i.ConfidenceScore:P0}"
        }).ToList();

        InsightsList.ItemsSource = displayItems;
        RefreshCounts();

        LoadingIndicator.IsVisible = false;
        LoadingIndicator.IsRunning = false;
        AnalyseButton.IsEnabled = true;
    }

    private static string GetCategoryIcon(InsightCategory category) => category switch
    {
        InsightCategory.TenantScreening => "👤",
        InsightCategory.RentalPricing => "💰",
        InsightCategory.MaintenancePrediction => "🔧",
        InsightCategory.OccupancyForecast => "📊",
        InsightCategory.RiskAssessment => "⚠️",
        InsightCategory.LeaseRecommendation => "📄",
        _ => "💡"
    };
}

public class InsightDisplayItem
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string ConfidenceText { get; set; } = "";
}
