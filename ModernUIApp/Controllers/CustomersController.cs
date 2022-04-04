using Microsoft.AspNetCore.Mvc;
using ModernUIApp.Services;

namespace ModernUIApp.Controllers
{
    public class CustomersController : Controller
    {
        private ICustomerDataService _customerDataService;
        private IFeatureFlagService _featureFlagService;

        public CustomersController(ICustomerDataService customerDataService, IFeatureFlagService featureFlagService)
        {
            _customerDataService = customerDataService;
            _featureFlagService = featureFlagService;
        }
        public IActionResult Index()
        {
            if (_featureFlagService.ViewFeature(FeatureFlags.CustomerManagement))
            {
                var customers = _customerDataService.GetAll();
                return View(customers);
            }
            else
            {
                return View("~/Views/ComingSoon/Index.cshtml");
            }
        }
    }
}