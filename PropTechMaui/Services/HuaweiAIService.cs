using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PropTechMaui.Models;

namespace PropTechMaui.Services
{
    /// <summary>
    /// Service client for Huawei Cloud AI platform (ModelArts / Pangu models).
    /// Provides authenticated access to Huawei Cloud NLP and prediction APIs
    /// for property management intelligence features.
    /// 
    /// When live API access is unavailable (demo mode), returns built-in
    /// heuristic results so the application can demonstrate AI-driven workflows.
    /// </summary>
    public class HuaweiAIService
    {
        private readonly AIConfiguration _config;
        private readonly HttpClient _httpClient;
        private string? _authToken;
        private DateTime _tokenExpiry = DateTime.MinValue;

        public HuaweiAIService(AIConfiguration config, HttpClient? httpClient = null)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _httpClient = httpClient ?? new HttpClient();
        }

        /// <summary>
        /// Obtains an IAM authentication token from Huawei Cloud.
        /// Tokens are cached until expiry to minimise API calls.
        /// </summary>
        public async Task<string> GetAuthTokenAsync()
        {
            if (_authToken != null && DateTime.UtcNow < _tokenExpiry)
                return _authToken;

            if (!_config.IsEnabled)
            {
                _authToken = "demo-token";
                _tokenExpiry = DateTime.UtcNow.AddHours(1);
                return _authToken;
            }

            var requestBody = new
            {
                auth = new
                {
                    identity = new
                    {
                        methods = new[] { "hw_ak_sk" },
                        hw_ak_sk = new
                        {
                            access = new { key = _config.AccessKey },
                            secret = new { key = _config.SecretKey }
                        }
                    },
                    scope = new
                    {
                        project = new { id = _config.ProjectId }
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = $"{_config.IamEndpoint}/v3/auth/tokens";

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            _authToken = response.Headers.GetValues("X-Subject-Token").FirstOrDefault();
            _tokenExpiry = DateTime.UtcNow.AddHours(23);
            return _authToken ?? throw new InvalidOperationException("Failed to obtain auth token");
        }

        /// <summary>
        /// Sends a prompt to the Huawei Pangu large language model for text generation.
        /// Used for lease clause generation, tenant communication drafting, and insights.
        /// </summary>
        public async Task<string> GenerateTextAsync(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                throw new ArgumentException("Prompt cannot be empty", nameof(prompt));

            if (!_config.IsEnabled)
                return GenerateDemoResponse(prompt);

            var token = await GetAuthTokenAsync();

            var requestBody = new
            {
                model = _config.PanguModelId,
                messages = new[]
                {
                    new { role = "system", content = "You are an expert South African property management AI assistant." },
                    new { role = "user", content = prompt }
                },
                max_tokens = 1024,
                temperature = 0.7
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{_config.ModelArtsEndpoint}/v1/{_config.ProjectId}/deployments/{_config.PanguModelId}/predict")
            {
                Content = content
            };
            request.Headers.Add("X-Auth-Token", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var root = doc.RootElement;
            if (root.TryGetProperty("choices", out var choices) &&
                choices.GetArrayLength() > 0 &&
                choices[0].TryGetProperty("message", out var message) &&
                message.TryGetProperty("content", out var contentVal))
            {
                return contentVal.GetString() ?? string.Empty;
            }
            return string.Empty;
        }

        /// <summary>
        /// Calls the Huawei ModelArts prediction endpoint for numerical/classification tasks
        /// such as rental price prediction and tenant risk scoring.
        /// </summary>
        public async Task<Dictionary<string, double>> PredictAsync(string modelId, Dictionary<string, object> features)
        {
            if (!_config.IsEnabled)
                return GenerateDemoPrediction(modelId, features);

            var token = await GetAuthTokenAsync();

            var requestBody = new { data = new { inputs = features } };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{_config.ModelArtsEndpoint}/v1/{_config.ProjectId}/deployments/{modelId}/predict")
            {
                Content = content
            };
            request.Headers.Add("X-Auth-Token", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);

            var results = new Dictionary<string, double>();
            foreach (var prop in doc.RootElement.GetProperty("predictions").EnumerateObject())
            {
                if (prop.Value.TryGetDouble(out double val))
                    results[prop.Name] = val;
            }
            return results;
        }

        /// <summary>
        /// Generates a demo response when live API is unavailable.
        /// </summary>
        private static string GenerateDemoResponse(string prompt)
        {
            var lowerPrompt = prompt.ToLowerInvariant();

            if (lowerPrompt.Contains("maintenance") || lowerPrompt.Contains("repair"))
                return "Predictive analysis indicates the property may require plumbing inspection within " +
                       "the next 90 days and electrical certification renewal within 6 months. " +
                       "Estimated combined maintenance cost: R4,200.";

            if (lowerPrompt.Contains("lease") || lowerPrompt.Contains("clause"))
                return "The Lessee shall occupy the premises solely for residential purposes. " +
                       "The property shall be maintained in good condition, and the Lessee agrees " +
                       "to comply with all applicable municipal by-laws and regulations. Any alterations " +
                       "to the premises require prior written consent from the Lessor.";

            if (lowerPrompt.Contains("screen"))
                return "Based on the provided tenant profile, the applicant demonstrates stable employment " +
                       "history and satisfactory references. Recommended for approval with standard deposit terms.";

            if (lowerPrompt.Contains("price") || lowerPrompt.Contains("rent") || lowerPrompt.Contains("pricing"))
                return "Current market analysis suggests rental pricing between R3,500 and R5,500 per month " +
                       "for this property type and area. Factors include proximity to transport, " +
                       "property condition, and local demand trends.";

            if (lowerPrompt.Contains("tenant"))
                return "Based on the provided tenant profile, the applicant demonstrates stable employment " +
                       "history and satisfactory references. Recommended for approval with standard deposit terms.";

            return "AI analysis completed. The property management platform has processed your request " +
                   "using Huawei Cloud Pangu model intelligence. Please provide more specific details " +
                   "for targeted recommendations.";
        }

        /// <summary>
        /// Generates demo prediction results when live API is unavailable.
        /// </summary>
        private static Dictionary<string, double> GenerateDemoPrediction(string modelId, Dictionary<string, object> features)
        {
            var results = new Dictionary<string, double>();

            if (modelId.Contains("pricing") || modelId.Contains("rent"))
            {
                results["predicted_rent"] = 4500.0;
                results["confidence"] = 0.82;
                results["market_trend"] = 0.03;
            }
            else if (modelId.Contains("risk") || modelId.Contains("tenant"))
            {
                results["risk_score"] = 0.25;
                results["payment_reliability"] = 0.85;
                results["tenure_prediction_months"] = 18.0;
            }
            else if (modelId.Contains("maintenance"))
            {
                results["failure_probability"] = 0.15;
                results["estimated_cost"] = 4200.0;
                results["days_until_needed"] = 90.0;
            }
            else
            {
                results["prediction"] = 0.75;
                results["confidence"] = 0.80;
            }

            return results;
        }
    }
}
