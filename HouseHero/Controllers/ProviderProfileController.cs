using BLL.Interface;
using HouseHero.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseHero.Controllers
{
    [Authorize(Roles = "Provider")]
    public class ProviderProfileController : Controller
    {
        private readonly IProviderRepository _provider;
        private readonly ICustomerRepository _customer;


        public ProviderProfileController(IProviderRepository provider, ICustomerRepository customer)
        {
            _provider = provider;
            _customer = customer;

        }
        public IActionResult Details()
        {
            var applicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Provider = _provider.GetProviderByApplicationUserId(int.Parse(applicationUserId));
            var Result = _provider.GetProviderWithAllRelatedData(Provider.Id);
            ProviderWithAllDataViewModel ViewModel = Result;
            return View(ViewModel);
        }

    }
}
