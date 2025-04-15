using AccessManagementLaredo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagementLaredo_App.Controllers
{
	public class PermitEventController : Controller
	{
		private IPermitEventRepository _permitEventRepository;

		public PermitEventController(IPermitEventRepository permitEventRepository)
		{
			_permitEventRepository = permitEventRepository;
		}

		public IActionResult Index()
		{
			return View();
		}

		public string ReadPermitEvents(string role, int? id)
		{
			string result = _permitEventRepository.ReadPermitEvents(role, id);
			_permitEventRepository.DisposeDBObjects();
			return result;
		}
	}
}
