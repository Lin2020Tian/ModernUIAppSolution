using ModernUIApp.Services;
using Moq;

namespace ModernUIApp.Tests.Mocks
{
    public class MockFeaureFlagService : IFeatureFlagService {
        private Mock<IFeatureFlagService> _mock;
        public MockFeaureFlagService()
        {
            _mock = new Mock<IFeatureFlagService>();
            _mock.Setup(ffs => ffs.ViewFeature(It.IsAny<string>())).Returns(true);
        }
        public bool ViewFeature (string feature) {
            return _mock.Object.ViewFeature(feature);
        }
    }
}