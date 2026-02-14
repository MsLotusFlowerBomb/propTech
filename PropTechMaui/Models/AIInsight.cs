using System;
using System.Collections.Generic;

namespace PropTechMaui.Models
{
    /// <summary>
    /// Represents an AI-generated insight or recommendation for property management decisions.
    /// </summary>
    public class AIInsight
    {
        public string Id { get; private set; }
        public InsightCategory Category { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public double ConfidenceScore { get; private set; }
        public DateTime GeneratedAt { get; private set; }

        public AIInsight(InsightCategory category, string title, string description, double confidenceScore)
        {
            Id = Guid.NewGuid().ToString();
            Category = category;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? string.Empty;
            ConfidenceScore = Math.Clamp(confidenceScore, 0.0, 1.0);
            GeneratedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Categories for AI-generated insights.
    /// </summary>
    public enum InsightCategory
    {
        TenantScreening,
        RentalPricing,
        MaintenancePrediction,
        LeaseRecommendation,
        OccupancyForecast,
        RiskAssessment,
        VirtualTourInspection
    }

    /// <summary>
    /// AI-generated tenant risk assessment result.
    /// </summary>
    public class TenantScreeningResult
    {
        public string TenantId { get; private set; }
        public RiskLevel Risk { get; private set; }
        public double RiskScore { get; private set; }
        public string Summary { get; private set; }
        public List<string> Factors { get; private set; }
        public DateTime AssessedAt { get; private set; }

        public TenantScreeningResult(string tenantId, RiskLevel risk, double riskScore, string summary, List<string>? factors = null)
        {
            TenantId = tenantId ?? throw new ArgumentNullException(nameof(tenantId));
            Risk = risk;
            RiskScore = Math.Clamp(riskScore, 0.0, 1.0);
            Summary = summary ?? string.Empty;
            Factors = factors ?? new List<string>();
            AssessedAt = DateTime.UtcNow;
        }
    }

    public enum RiskLevel
    {
        Low,
        Medium,
        High
    }

    /// <summary>
    /// AI-generated rental pricing recommendation.
    /// </summary>
    public class RentalPricingRecommendation
    {
        public string PropertyId { get; private set; }
        public decimal RecommendedRent { get; private set; }
        public decimal MinRent { get; private set; }
        public decimal MaxRent { get; private set; }
        public double ConfidenceScore { get; private set; }
        public List<string> MarketFactors { get; private set; }
        public DateTime GeneratedAt { get; private set; }

        public RentalPricingRecommendation(
            string propertyId,
            decimal recommendedRent,
            decimal minRent,
            decimal maxRent,
            double confidenceScore,
            List<string>? marketFactors = null)
        {
            PropertyId = propertyId ?? throw new ArgumentNullException(nameof(propertyId));
            RecommendedRent = recommendedRent;
            MinRent = minRent;
            MaxRent = maxRent;
            ConfidenceScore = Math.Clamp(confidenceScore, 0.0, 1.0);
            MarketFactors = marketFactors ?? new List<string>();
            GeneratedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// AI-generated maintenance prediction for a property.
    /// </summary>
    public class MaintenancePrediction
    {
        public string PropertyId { get; private set; }
        public string Issue { get; private set; }
        public string Urgency { get; private set; }
        public decimal EstimatedCost { get; private set; }
        public int EstimatedDaysUntilNeeded { get; private set; }
        public double ConfidenceScore { get; private set; }
        public DateTime PredictedAt { get; private set; }

        public MaintenancePrediction(
            string propertyId,
            string issue,
            string urgency,
            decimal estimatedCost,
            int estimatedDaysUntilNeeded,
            double confidenceScore)
        {
            PropertyId = propertyId ?? throw new ArgumentNullException(nameof(propertyId));
            Issue = issue ?? string.Empty;
            Urgency = urgency ?? "Medium";
            EstimatedCost = estimatedCost;
            EstimatedDaysUntilNeeded = estimatedDaysUntilNeeded;
            ConfidenceScore = Math.Clamp(confidenceScore, 0.0, 1.0);
            PredictedAt = DateTime.UtcNow;
        }
    }
}
