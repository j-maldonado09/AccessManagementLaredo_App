using AccessManagementLaredo;
using AccessManagementLaredo.ViewModels;
using AccessManagementLaredo_App.Models;
using AccessManagementLaredo_App.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AccessManagementLaredo_App.Controllers
{
    public class PermitRequestController : Controller
    {
        private IPermitRequestRepository _permitRequestRepository;
        private UserManager<ApplicationUser> _userManager;
        private IdentityServices _identityServices;

        // Constructor
        public PermitRequestController(IPermitRequestRepository permitRequestRepository, UserManager<ApplicationUser> userManager,
                IdentityServices identityServices)
        { 
            _permitRequestRepository = permitRequestRepository;
            _userManager = userManager;
            _identityServices = identityServices;
        }

        // Index action method
        public IActionResult Index()
        {
            return View();
        }

        //Read action method to fetch permit requests
        //public IActionResult Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    string result = _permitRequestRepository.Read();
        //    IQueryable<PermitRequestViewModel> permitRequests = JsonSerializer.Deserialize<List<PermitRequestViewModel>>(result).AsQueryable();
        //    _permitRequestRepository.DisposeDBObjects();
        //    DataSourceResult dsResult = permitRequests.ToDataSourceResult(request);
        //    return Json(dsResult);
        //}

        public async Task<IActionResult> Read([DataSourceRequest] DataSourceRequest request)
        {
            string result = _permitRequestRepository.Read();
            IQueryable<PermitRequestViewModel> permitRequests = JsonSerializer.Deserialize<List<PermitRequestViewModel>>(result).AsQueryable();
            _permitRequestRepository.DisposeDBObjects();

            // Filter requests based on the current user's role and username
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            string currentUserRole = _userManager.GetRolesAsync(currentUser).Result[0];
            if (currentUserRole == "OWNER" || currentUserRole == "CONSULTANT")
            {
                permitRequests = permitRequests.Where(x => x.UserId == currentUser.Id);
            }
            DataSourceResult dsResult = permitRequests.ToDataSourceResult(request);
            return Json(dsResult);
        }
    }
}
