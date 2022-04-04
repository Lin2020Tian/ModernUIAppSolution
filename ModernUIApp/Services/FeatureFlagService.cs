using Microsoft.FeatureManagement;

namespace ModernUIApp.Services
{
    public class FeatureFlagService : IFeatureFlagService {

        private readonly IFeatureManager _featureManager;

       public FeatureFlagService(IFeatureManager featureManager)
       {
            _featureManager = featureManager;
       }
        public bool ViewFeature(string feature)
        {
            return _featureManager.IsEnabledAsync(feature).Result; 
        }
    }
}