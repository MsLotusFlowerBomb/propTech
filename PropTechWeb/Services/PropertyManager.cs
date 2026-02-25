using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PropTechWeb.Models;

namespace PropTechWeb.Services
{
    /// <summary>
    /// Main business-logic controller for property management operations.
    /// Coordinates between the DataStore, AI services, and domain models.
    /// Follows SRP: handles orchestration while delegating AI to the agent.
    /// </summary>
    public class PropertyManager
    {
        private readonly DataStore _store;
        private readonly AIPropertyAgent _aiAgent;

        public PropertyManager(DataStore store, AIPropertyAgent aiAgent)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            _aiAgent = aiAgent ?? throw new ArgumentNullException(nameof(aiAgent));
        }

        /// <summary>
        /// Registers a new tenant and performs AI-powered screening.
        /// Returns the screening result along with the created tenant.
        /// </summary>
        public async Task<(Tenant Tenant, TenantScreeningResult Screening)> RegisterNewTenantAsync(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Tenant name is required", nameof(fullName));

            var tenant = new Tenant(Guid.NewGuid().ToString(), fullName);
            _store.AddTenant(tenant);

            var screening = await _aiAgent.ScreenTenantAsync(tenant);
            return (tenant, screening);
        }

        /// <summary>
        /// Creates a lease agreement with AI-generated clause recommendations.
        /// </summary>
        public async Task<(LeaseAgreement Lease, string AIClause)> CreateLeaseAsync(string tenantId, string propertyId)
        {
            if (string.IsNullOrWhiteSpace(tenantId))
                throw new ArgumentException("Tenant ID is required", nameof(tenantId));
            if (string.IsNullOrWhiteSpace(propertyId))
                throw new ArgumentException("Property ID is required", nameof(propertyId));

            var property = _store.GetPropertyById(propertyId)
                ?? throw new InvalidOperationException($"Property '{propertyId}' not found");

            Tenant? tenant = null;
            foreach (var t in _store.Tenants)
            {
                if (string.Equals(t.Id, tenantId, StringComparison.OrdinalIgnoreCase))
                {
                    tenant = t;
                    break;
                }
            }
            if (tenant == null)
                throw new InvalidOperationException($"Tenant '{tenantId}' not found");

            var lease = new LeaseAgreement(Guid.NewGuid().ToString(), tenant, property, property.MonthlyRent);
            _store.AddLease(lease);

            var aiClause = await _aiAgent.GenerateLeaseClauseAsync(property, tenant, "General Terms");
            return (lease, aiClause);
        }

        /// <summary>
        /// Gets an AI rental pricing recommendation for a property.
        /// </summary>
        public async Task<RentalPricingRecommendation> GetPricingRecommendationAsync(string propertyId)
        {
            var property = _store.GetPropertyById(propertyId)
                ?? throw new InvalidOperationException($"Property '{propertyId}' not found");

            return await _aiAgent.RecommendRentalPriceAsync(property);
        }

        /// <summary>
        /// Gets AI-predicted maintenance needs for a property.
        /// </summary>
        public async Task<MaintenancePrediction> GetMaintenancePredictionAsync(string propertyId)
        {
            var property = _store.GetPropertyById(propertyId)
                ?? throw new InvalidOperationException($"Property '{propertyId}' not found");

            return await _aiAgent.PredictMaintenanceAsync(property);
        }

        /// <summary>
        /// Runs an AI-powered inspection on a property's 360° virtual tour.
        /// Analyses room panoramas to detect defects, assess condition, and estimate repair costs.
        /// </summary>
        public async Task<InspectionReport> RunVirtualTourInspectionAsync(string propertyId)
        {
            var property = _store.GetPropertyById(propertyId)
                ?? throw new InvalidOperationException($"Property '{propertyId}' not found");

            var tour = _store.GetVirtualTourByPropertyId(propertyId)
                ?? throw new InvalidOperationException($"No virtual tour found for property '{propertyId}'");

            return await _aiAgent.AnalyseVirtualTourAsync(tour, property);
        }

        private const decimal DefaultWaterElectricityLevy = 350m;
        private const decimal DefaultAdministrationFee = 150m;

        /// <summary>
        /// Issues a monthly rental invoice for the specified lease.
        /// Builds line items from the lease (rent + default levies) and stores the invoice.
        /// The levy amounts are default values; customise via a fee configuration class when needed.
        /// </summary>
        public Task<Invoice> IssueInvoiceAsync(string leaseId)
        {
            if (string.IsNullOrWhiteSpace(leaseId))
                throw new ArgumentException("Lease ID is required", nameof(leaseId));

            LeaseAgreement? lease = null;
            foreach (var l in _store.Leases)
            {
                if (string.Equals(l.Id, leaseId, StringComparison.OrdinalIgnoreCase))
                {
                    lease = l;
                    break;
                }
            }
            if (lease == null)
                throw new InvalidOperationException($"Lease '{leaseId}' not found");

            var today = DateTime.UtcNow.Date;
            var dueDate = new DateTime(today.Year, today.Month, 1).AddMonths(1);

            var items = new List<LineItem>
            {
                new LineItem("Monthly Rent", lease.MonthlyRent),
                new LineItem("Water & Electricity Levy", DefaultWaterElectricityLevy),
                new LineItem("Administration Fee", DefaultAdministrationFee)
            };

            var invoice = new Invoice(
                Guid.NewGuid().ToString(),
                lease.Id,
                lease.Lessee.Id,
                lease.Lessee.FullName,
                lease.LeasedProperty.Address,
                items,
                today,
                dueDate);

            _store.AddInvoice(invoice);
            return Task.FromResult(invoice);
        }

        /// <summary>
        /// Generates a monthly account statement for a tenant, summarising all invoices
        /// for the current calendar month.
        /// </summary>
        public Task<Statement> GenerateStatementAsync(string tenantId)
        {
            if (string.IsNullOrWhiteSpace(tenantId))
                throw new ArgumentException("Tenant ID is required", nameof(tenantId));

            Tenant? tenant = null;
            foreach (var t in _store.Tenants)
            {
                if (string.Equals(t.Id, tenantId, StringComparison.OrdinalIgnoreCase))
                {
                    tenant = t;
                    break;
                }
            }
            if (tenant == null)
                throw new InvalidOperationException($"Tenant '{tenantId}' not found");

            var today = DateTime.UtcNow.Date;
            var periodStart = new DateTime(today.Year, today.Month, 1);
            var periodEnd = periodStart.AddMonths(1).AddDays(-1);

            var tenantInvoices = new List<Invoice>(_store.GetInvoicesByTenantId(tenantId));

            var statement = new Statement(
                Guid.NewGuid().ToString(),
                tenant.Id,
                tenant.FullName,
                periodStart,
                periodEnd,
                tenantInvoices);

            _store.AddStatement(statement);
            return Task.FromResult(statement);
        }
    }
}