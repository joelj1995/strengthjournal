using Microsoft.FeatureManagement;

namespace StrengthJournal.Server.Integrations.Implementation
{
    public class AzureAppConfigurationFeatureService : IFeatureService
    {
        private readonly IFeatureManager featureManager;

        public AzureAppConfigurationFeatureService(IFeatureManager featureManager)
        {
            this.featureManager = featureManager;
        }

        public async Task<ICollection<string>> GetFeatures()
        {
            var features = featureManager.GetFeatureNamesAsync();
            var result = new List<string>();
            await foreach (var feature in features)
            {
                var enabled = await HasFeature(feature);
                if (enabled)
                    result.Add(feature);
            }
            return result;
        }

        public async Task<bool> HasFeature(string featureName)
        {
            return await featureManager.IsEnabledAsync(featureName);
        }
    }
}
