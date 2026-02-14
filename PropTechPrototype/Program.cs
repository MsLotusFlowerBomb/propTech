using System;
using System.Threading.Tasks;
using PropTechPrototype.Models;
using PropTechPrototype.Services;

namespace PropTechPrototype
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== PropMate — AI-Powered Property Management ===");
            Console.WriteLine("  Powered by Huawei Cloud AI (Pangu / ModelArts)");
            Console.WriteLine(new string('=', 52));
            Console.WriteLine();

            // --- Initialise core services ---
            var aiConfig = AIConfiguration.CreateDemo();
            var aiService = new HuaweiAIService(aiConfig);
            var dataStore = new DataStore();
            var aiAgent = new AIPropertyAgent(aiService, dataStore);
            var manager = new PropertyManager(dataStore, aiAgent);

            // --- Seed sample properties ---
            var prop1 = new Property("P001", "12 Mandela Ave, Soweto", 3800m);
            var prop2 = new Property("P002", "45 Long St, Cape Town", 5200m);
            dataStore.AddProperty(prop1);
            dataStore.AddProperty(prop2);

            Console.WriteLine("[1] Registered properties:");
            foreach (var p in dataStore.Properties)
                Console.WriteLine($"    {p.Id} — {p.Address} @ R{p.MonthlyRent:N0}/month");
            Console.WriteLine();

            // --- Register tenant with AI screening ---
            Console.WriteLine("[2] Registering tenant with AI screening...");
            var (tenant, screening) = await manager.RegisterNewTenantAsync("Thabo Mokoena");
            Console.WriteLine($"    Tenant: {tenant.FullName} ({tenant.Id})");
            Console.WriteLine($"    Risk level: {screening.Risk} (score: {screening.RiskScore:F2})");
            Console.WriteLine($"    AI summary: {screening.Summary}");
            foreach (var factor in screening.Factors)
                Console.WriteLine($"      • {factor}");
            Console.WriteLine();

            // --- AI rental pricing recommendation ---
            Console.WriteLine("[3] AI rental pricing recommendation...");
            var pricing = await manager.GetPricingRecommendationAsync("P001");
            Console.WriteLine($"    Property: {prop1.Address}");
            Console.WriteLine($"    Recommended: R{pricing.RecommendedRent:N0}/month");
            Console.WriteLine($"    Range: R{pricing.MinRent:N0} – R{pricing.MaxRent:N0}");
            Console.WriteLine($"    Confidence: {pricing.ConfidenceScore:P0}");
            foreach (var f in pricing.MarketFactors)
                Console.WriteLine($"      • {f}");
            Console.WriteLine();

            // --- Create lease with AI-generated clause ---
            Console.WriteLine("[4] Creating lease with AI-generated clause...");
            var (lease, aiClause) = await manager.CreateLeaseAsync(tenant.Id, "P001");
            Console.WriteLine($"    Lease {lease.Id} created");
            Console.WriteLine($"    Tenant: {lease.Lessee.FullName}");
            Console.WriteLine($"    Property: {lease.LeasedProperty.Address}");
            Console.WriteLine($"    Monthly rent: R{lease.MonthlyRent:N0}");
            Console.WriteLine($"    AI clause: {aiClause}");
            Console.WriteLine();

            // --- AI maintenance prediction ---
            Console.WriteLine("[5] AI maintenance prediction...");
            var maintenance = await manager.GetMaintenancePredictionAsync("P002");
            Console.WriteLine($"    Property: {prop2.Address}");
            Console.WriteLine($"    Urgency: {maintenance.Urgency}");
            Console.WriteLine($"    Estimated cost: R{maintenance.EstimatedCost:N0}");
            Console.WriteLine($"    Days until needed: {maintenance.EstimatedDaysUntilNeeded}");
            Console.WriteLine($"    Issue: {maintenance.Issue}");
            Console.WriteLine();

            // --- Full portfolio analysis ---
            Console.WriteLine("[6] Running full agentic portfolio analysis...");
            var insights = await aiAgent.RunPortfolioAnalysisAsync();
            Console.WriteLine($"    Generated {insights.Count} insights:");
            foreach (var insight in insights)
            {
                Console.WriteLine($"    [{insight.Category}] {insight.Title}");
                Console.WriteLine($"      {insight.Description}");
                Console.WriteLine($"      Confidence: {insight.ConfidenceScore:P0}");
            }
            Console.WriteLine();

            Console.WriteLine("=== AI-powered property management session complete ===");
        }
    }
}