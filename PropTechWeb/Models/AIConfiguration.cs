using System;

namespace PropTechWeb.Models
{
    /// <summary>
    /// Configuration settings for Huawei Cloud AI services.
    /// Stores API credentials and endpoint URLs for Pangu model and ModelArts.
    /// </summary>
    public class AIConfiguration
    {
        /// <summary>Huawei Cloud region endpoint (e.g. "af-south-1" for South Africa).</summary>
        public string Region { get; private set; }

        /// <summary>Huawei Cloud project ID for API authentication.</summary>
        public string ProjectId { get; private set; }

        /// <summary>IAM endpoint for obtaining authentication tokens.</summary>
        public string IamEndpoint { get; private set; }

        /// <summary>ModelArts endpoint for AI model inference.</summary>
        public string ModelArtsEndpoint { get; private set; }

        /// <summary>API key or AK/SK access key for Huawei Cloud authentication.</summary>
        public string AccessKey { get; private set; }

        /// <summary>Secret key for Huawei Cloud authentication.</summary>
        public string SecretKey { get; private set; }

        /// <summary>Pangu model ID deployed on ModelArts for NLP tasks.</summary>
        public string PanguModelId { get; private set; }

        /// <summary>Whether AI features are enabled (can run in offline/demo mode if false).</summary>
        public bool IsEnabled { get; private set; }

        public AIConfiguration(
            string region,
            string projectId,
            string accessKey,
            string secretKey,
            string panguModelId,
            bool isEnabled = true)
        {
            Region = region ?? "af-south-1";
            ProjectId = projectId ?? throw new ArgumentNullException(nameof(projectId));
            AccessKey = accessKey ?? throw new ArgumentNullException(nameof(accessKey));
            SecretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
            PanguModelId = panguModelId ?? "pangu-alpha";
            IsEnabled = isEnabled;

            IamEndpoint = $"https://iam.{Region}.myhuaweicloud.com";
            ModelArtsEndpoint = $"https://modelarts.{Region}.myhuaweicloud.com";
        }

        /// <summary>
        /// Creates a demo/offline configuration for testing without live API access.
        /// </summary>
        public static AIConfiguration CreateDemo()
        {
            return new AIConfiguration(
                region: "af-south-1",
                projectId: "demo-project",
                accessKey: "demo-ak",
                secretKey: "demo-sk",
                panguModelId: "pangu-alpha",
                isEnabled: false);
        }
    }
}
