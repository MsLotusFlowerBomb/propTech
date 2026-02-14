using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PropTechPrototype.Models;

namespace PropTechPrototype.Services
{
    /// <summary>
    /// Agentic AI service that autonomously orchestrates property management tasks.
    /// Uses Huawei Cloud AI (Pangu models via ModelArts) to make intelligent decisions
    /// across tenant screening, rental pricing, lease generation, and maintenance planning.
    /// 
    /// The agent follows a plan-act-observe loop:
    ///   1. Analyse the current context (property data, tenant info, market signals)
    ///   2. Select and execute the appropriate AI action
    ///   3. Return structured recommendations with confidence scores
    /// </summary>
    public class AIPropertyAgent
    {
        private readonly HuaweiAIService _aiService;
        private readonly DataStore _dataStore;
        private readonly List<AIInsight> _insightHistory = new();

        public IReadOnlyList<AIInsight> InsightHistory => _insightHistory.AsReadOnly();

        public AIPropertyAgent(HuaweiAIService aiService, DataStore dataStore)
        {
            _aiService = aiService ?? throw new ArgumentNullException(nameof(aiService));
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        /// <summary>
        /// Screens a tenant by analysing their profile and returning a risk assessment.
        /// Uses Huawei Pangu model for natural language analysis of tenant data.
        /// </summary>
        public async Task<TenantScreeningResult> ScreenTenantAsync(Tenant tenant)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));

            var prompt = $"Screen this tenant application for a South African rental property:\n" +
                         $"Name: {tenant.FullName}\n" +
                         $"ID: {tenant.Id}\n" +
                         $"Provide a risk assessment with factors to consider for approval.";

            var aiResponse = await _aiService.GenerateTextAsync(prompt);

            var predictionFeatures = new Dictionary<string, object>
            {
                { "tenant_id", tenant.Id },
                { "tenant_name", tenant.FullName }
            };
            var prediction = await _aiService.PredictAsync("tenant-risk-model", predictionFeatures);

            var riskScore = prediction.GetValueOrDefault("risk_score", 0.25);
            var risk = riskScore switch
            {
                < 0.3 => RiskLevel.Low,
                < 0.7 => RiskLevel.Medium,
                _ => RiskLevel.High
            };

            var factors = new List<string>
            {
                $"Payment reliability: {prediction.GetValueOrDefault("payment_reliability", 0.85):P0}",
                $"Predicted tenure: {prediction.GetValueOrDefault("tenure_prediction_months", 12):F0} months"
            };

            var result = new TenantScreeningResult(tenant.Id, risk, riskScore, aiResponse, factors);

            _insightHistory.Add(new AIInsight(
                InsightCategory.TenantScreening,
                $"Screening: {tenant.FullName}",
                aiResponse,
                1.0 - riskScore));

            return result;
        }

        /// <summary>
        /// Generates an optimal rental pricing recommendation for a property
        /// using market analysis via Huawei Cloud AI.
        /// </summary>
        public async Task<RentalPricingRecommendation> RecommendRentalPriceAsync(Property property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            var prompt = $"Recommend optimal monthly rental pricing for this South African property:\n" +
                         $"Address: {property.Address}\n" +
                         $"Current rent: R{property.MonthlyRent:N0}\n" +
                         $"Analyse local market conditions, demand trends, and comparable properties.";

            var aiResponse = await _aiService.GenerateTextAsync(prompt);

            var features = new Dictionary<string, object>
            {
                { "property_id", property.Id },
                { "address", property.Address },
                { "current_rent", (double)property.MonthlyRent }
            };
            var prediction = await _aiService.PredictAsync("rental-pricing-model", features);

            var predictedRent = (decimal)prediction.GetValueOrDefault("predicted_rent", (double)property.MonthlyRent);
            var confidence = prediction.GetValueOrDefault("confidence", 0.80);
            var marketTrend = prediction.GetValueOrDefault("market_trend", 0.0);

            var variance = predictedRent * 0.15m;
            var marketFactors = new List<string>
            {
                $"Market trend: {(marketTrend >= 0 ? "+" : "")}{marketTrend:P1}",
                aiResponse
            };

            var recommendation = new RentalPricingRecommendation(
                property.Id, predictedRent,
                predictedRent - variance, predictedRent + variance,
                confidence, marketFactors);

            _insightHistory.Add(new AIInsight(
                InsightCategory.RentalPricing,
                $"Pricing: {property.Address}",
                $"Recommended R{predictedRent:N0}/month (confidence: {confidence:P0})",
                confidence));

            return recommendation;
        }

        /// <summary>
        /// Predicts upcoming maintenance needs for a property using AI analysis.
        /// </summary>
        public async Task<MaintenancePrediction> PredictMaintenanceAsync(Property property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            var prompt = $"Predict maintenance needs for this South African rental property:\n" +
                         $"Address: {property.Address}\n" +
                         $"Identify likely upcoming repairs, estimated costs in Rand, and urgency levels.";

            var aiResponse = await _aiService.GenerateTextAsync(prompt);

            var features = new Dictionary<string, object>
            {
                { "property_id", property.Id },
                { "address", property.Address }
            };
            var prediction = await _aiService.PredictAsync("maintenance-model", features);

            var estimatedCost = (decimal)prediction.GetValueOrDefault("estimated_cost", 4200.0);
            var daysUntilNeeded = (int)prediction.GetValueOrDefault("days_until_needed", 90.0);
            var failureProb = prediction.GetValueOrDefault("failure_probability", 0.15);
            var urgency = failureProb switch
            {
                > 0.7 => "High",
                > 0.3 => "Medium",
                _ => "Low"
            };

            var result = new MaintenancePrediction(
                property.Id, aiResponse, urgency,
                estimatedCost, daysUntilNeeded, 1.0 - failureProb);

            _insightHistory.Add(new AIInsight(
                InsightCategory.MaintenancePrediction,
                $"Maintenance: {property.Address}",
                $"Urgency: {urgency}, Est. R{estimatedCost:N0} in {daysUntilNeeded} days",
                1.0 - failureProb));

            return result;
        }

        /// <summary>
        /// Generates AI-enhanced lease clause text tailored to a specific property and tenant.
        /// </summary>
        public async Task<string> GenerateLeaseClauseAsync(Property property, Tenant tenant, string clauseType)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));

            var prompt = $"Generate a '{clauseType}' lease clause for a South African residential rental agreement.\n" +
                         $"Property: {property.Address}\n" +
                         $"Tenant: {tenant.FullName}\n" +
                         $"Monthly Rent: R{property.MonthlyRent:N0}\n" +
                         $"The clause must comply with the South African Rental Housing Act.";

            var clause = await _aiService.GenerateTextAsync(prompt);

            _insightHistory.Add(new AIInsight(
                InsightCategory.LeaseRecommendation,
                $"Clause '{clauseType}' generated",
                clause,
                0.90));

            return clause;
        }

        /// <summary>
        /// Runs a full agentic analysis across all properties and tenants,
        /// generating a portfolio-wide set of insights and recommendations.
        /// </summary>
        public async Task<List<AIInsight>> RunPortfolioAnalysisAsync()
        {
            var insights = new List<AIInsight>();

            foreach (var property in _dataStore.Properties)
            {
                var pricing = await RecommendRentalPriceAsync(property);
                insights.Add(new AIInsight(
                    InsightCategory.RentalPricing,
                    $"Pricing insight: {property.Address}",
                    $"Recommended R{pricing.RecommendedRent:N0}/month (range: R{pricing.MinRent:N0} – R{pricing.MaxRent:N0})",
                    pricing.ConfidenceScore));

                var maintenance = await PredictMaintenanceAsync(property);
                insights.Add(new AIInsight(
                    InsightCategory.MaintenancePrediction,
                    $"Maintenance alert: {property.Address}",
                    $"{maintenance.Urgency} urgency — {maintenance.Issue}",
                    maintenance.ConfidenceScore));
            }

            foreach (var tenant in _dataStore.Tenants)
            {
                var screening = await ScreenTenantAsync(tenant);
                insights.Add(new AIInsight(
                    InsightCategory.TenantScreening,
                    $"Tenant profile: {tenant.FullName}",
                    screening.Summary,
                    1.0 - screening.RiskScore));
            }

            // Occupancy forecast
            var totalProperties = _dataStore.Properties.Count;
            var leasedProperties = _dataStore.Leases.Count;
            var occupancyRate = totalProperties > 0
                ? (double)leasedProperties / totalProperties
                : 0.0;

            insights.Add(new AIInsight(
                InsightCategory.OccupancyForecast,
                "Portfolio occupancy forecast",
                $"Current occupancy: {occupancyRate:P0}. " +
                $"AI projects stable demand in the short term with seasonal variation expected.",
                0.75));

            return insights;
        }
    }
}
