using System;
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
    }
}