using Microsoft.AspNetCore.Mvc;

namespace AccessManagementLaredo_App.Controllers
{
    public class ReviewPermitRequestController : Controller
    {
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}
