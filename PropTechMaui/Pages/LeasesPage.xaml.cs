using PropTechMaui.Models;
using PropTechMaui.Services;

namespace PropTechMaui.Pages;

public partial class LeasesPage : ContentPage
{
    private readonly DataStore _dataStore;
    private readonly PropertyManager _manager;

    public LeasesPage(DataStore dataStore, PropertyManager manager)
    {
        InitializeComponent();
        _dataStore = dataStore;
        _manager = manager;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshPickers();
        RefreshList();
    }

    private void RefreshPickers()
    {
        TenantPicker.ItemsSource = _dataStore.Tenants
            .Select(t => $"{t.FullName} ({t.Id[..8]}...)")
            .ToList();
        PropertyPicker.ItemsSource = _dataStore.Properties
            .Select(p => $"{p.Address} (R {p.MonthlyRent:N0})")
            .ToList();
    }

    private void RefreshList()
    {
        var leases = _dataStore.Leases
            .Select(l => new LeaseDisplayItem
            {
                LeaseId = l.Id[..8] + "...",
                TenantName = l.Lessee.FullName,
                PropertyAddress = l.LeasedProperty.Address,
                RentDisplay = $"R {l.MonthlyRent:N0}"
            }).ToList();

        LeasesList.ItemsSource = leases;
        EmptyLeaseMessage.IsVisible = leases.Count == 0;
    }

    private async void OnCreateLease(object? sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;
        ClauseResultFrame.IsVisible = false;

        if (TenantPicker.SelectedIndex < 0 || PropertyPicker.SelectedIndex < 0)
        {
            ErrorLabel.Text = "Please select both a tenant and a property.";
            ErrorLabel.IsVisible = true;
            return;
        }

        var tenant = _dataStore.Tenants[TenantPicker.SelectedIndex];
        var property = _dataStore.Properties[PropertyPicker.SelectedIndex];

        try
        {
            CreateLeaseButton.IsEnabled = false;
            LeaseIndicator.IsVisible = true;
            LeaseIndicator.IsRunning = true;

            var (_, clause) = await _manager.CreateLeaseAsync(tenant.Id, property.Id);
            ClauseText.Text = clause;
            ClauseResultFrame.IsVisible = true;

            RefreshPickers();
            RefreshList();
        }
        catch (Exception)
        {
            ErrorLabel.Text = "An error occurred while creating the lease. Please check your selections and try again.";
            ErrorLabel.IsVisible = true;
        }
        finally
        {
            LeaseIndicator.IsVisible = false;
            LeaseIndicator.IsRunning = false;
            CreateLeaseButton.IsEnabled = true;
        }
    }
}

public class LeaseDisplayItem
{
    public string LeaseId { get; set; } = "";
    public string TenantName { get; set; } = "";
    public string PropertyAddress { get; set; } = "";
    public string RentDisplay { get; set; } = "";
}
