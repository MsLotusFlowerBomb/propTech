using System;
using System.Linq;
using Microsoft.Maui.Controls;
using PropTechMaui.Models;
using PropTechMaui.Services;

namespace PropTechMaui.Pages
{
    public partial class InvoicesPage : ContentPage
    {
        private readonly DataStore _dataStore;
        private readonly PropertyManager _manager;

        public InvoicesPage(DataStore dataStore, PropertyManager manager)
        {
            InitializeComponent();
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshData();
        }

        private void RefreshData()
        {
            // Summary counts
            TotalInvoicesLabel.Text = _dataStore.Invoices.Count.ToString();
            PaidLabel.Text = _dataStore.Invoices.Count(i => i.Status == PaymentStatus.Paid).ToString();
            PendingLabel.Text = _dataStore.Invoices
                .Count(i => i.Status == PaymentStatus.Pending || i.Status == PaymentStatus.Overdue)
                .ToString();

            // Populate lease picker
            var leaseItems = _dataStore.Leases
                .Select(l => new LeasePickerItem
                {
                    LeaseId = l.Id,
                    DisplayLabel = $"{l.Lessee.FullName} — {l.LeasedProperty.Address} (R{l.MonthlyRent:N0})"
                })
                .ToList();
            LeasePicker.ItemsSource = leaseItems;

            // Populate tenant picker
            TenantPicker.ItemsSource = _dataStore.Tenants.ToList();

            // Invoice list view model
            var invoiceVms = _dataStore.Invoices
                .Select(i => new InvoiceViewModel
                {
                    TenantName = i.TenantName,
                    PropertyAddress = i.PropertyAddress,
                    DateDisplay = $"{i.Date:d MMM yyyy} → Due {i.DueDate:d MMM yyyy}",
                    TotalDisplay = $"R{i.CalculateTotal():N0}",
                    Status = i.Status.ToString()
                })
                .ToList();
            InvoicesCollection.ItemsSource = invoiceVms;
        }

        private async void OnIssueInvoiceClicked(object sender, EventArgs e)
        {
            var selected = LeasePicker.SelectedItem as LeasePickerItem;
            if (selected == null)
            {
                await DisplayAlert("Validation", "Please select a lease.", "OK");
                return;
            }

            try
            {
                var invoice = await _manager.IssueInvoiceAsync(selected.LeaseId);
                InvoiceResultLabel.Text =
                    $"✅ Invoice issued for {invoice.TenantName}\n" +
                    $"   Total: R{invoice.CalculateTotal():N0}  |  Due: {invoice.DueDate:d MMM yyyy}";
                InvoiceResultLabel.IsVisible = true;
                LeasePicker.SelectedItem = null;
                RefreshData();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnGenerateStatementClicked(object sender, EventArgs e)
        {
            var selected = TenantPicker.SelectedItem as Tenant;
            if (selected == null)
            {
                await DisplayAlert("Validation", "Please select a tenant.", "OK");
                return;
            }

            try
            {
                var stmt = await _manager.GenerateStatementAsync(selected.Id);
                StatementResultLabel.Text =
                    $"📋 Statement for {stmt.TenantName}\n" +
                    $"   Period: {stmt.PeriodStart:d MMM} – {stmt.PeriodEnd:d MMM yyyy}\n" +
                    $"   Billed: R{stmt.TotalBilled:N0}  |  Paid: R{stmt.TotalPaid:N0}\n" +
                    $"   Balance Due: R{stmt.CalculateBalance():N0}";
                StatementResultLabel.IsVisible = true;
                TenantPicker.SelectedItem = null;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        // Helper view models for the MAUI page
        private class LeasePickerItem
        {
            public string LeaseId { get; set; } = "";
            public string DisplayLabel { get; set; } = "";
        }

        private class InvoiceViewModel
        {
            public string TenantName { get; set; } = "";
            public string PropertyAddress { get; set; } = "";
            public string DateDisplay { get; set; } = "";
            public string TotalDisplay { get; set; } = "";
            public string Status { get; set; } = "";
        }
    }
}
