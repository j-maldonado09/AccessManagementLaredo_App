using AccessManagementLaredo;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using AccessManagementLaredo.ViewModels;
using System.Text.Json;

namespace AccessManagementLaredo_App.Controllers
{
    public class PermitRequestController : Controller
    {
        private IPermitRequestResidentialRepository _permitRequestResidentialRepository;

        public PermitRequestController(IPermitRequestResidentialRepository permitRequestResidentialRepository)
        { 
            _permitRequestResidentialRepository = permitRequestResidentialRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            string result = _permitRequestResidentialRepository.Read();
            IQueryable<PermitRequestViewModel> permitRequests = JsonSerializer.Deserialize<List<PermitRequestViewModel>>(result).AsQueryable();
            _permitRequestResidentialRepository.DisposeDBObjects();
            DataSourceResult dsResult = permitRequests.ToDataSourceResult(request);
            return Json(dsResult);
        }
    }
}
