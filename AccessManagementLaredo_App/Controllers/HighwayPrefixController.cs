using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using AccessManagementLaredo;
using System.Text.Json;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace AccessManagementLaredo_App.Controllers
{
	//[Authorize(Roles = "SUPERADMIN,ADMIN,SUPERVISOR,USER")]
	public class HighwayPrefixController : Controller
	{
		private IHighwayPrefixRepository _highwayPrefixRepository;

		public HighwayPrefixController(IHighwayPrefixRepository highwayPrefixRepository)
		{
			_highwayPrefixRepository = highwayPrefixRepository;
		}

		public IActionResult Index()
		{
			return View();
		}

		[AcceptVerbs("Post")]
		public IActionResult Create([DataSourceRequest] DataSourceRequest request, HighwayPrefix highwayPrefix)
		{
			highwayPrefix.Id = _highwayPrefixRepository.Create(highwayPrefix);
			_highwayPrefixRepository.DisposeDBObjects();
			return Json(new[] { highwayPrefix }.ToDataSourceResult(request, ModelState));
		}

		public IActionResult Read([DataSourceRequest] DataSourceRequest request)
		{
			string result = _highwayPrefixRepository.Read();
			IQueryable<HighwayPrefix> highwayPrefixes = JsonSerializer.Deserialize<List<HighwayPrefix>>(result).AsQueryable();
			_highwayPrefixRepository.DisposeDBObjects();
			DataSourceResult dsResult = highwayPrefixes.ToDataSourceResult(request);
			return Json(dsResult);
		}

		[AcceptVerbs("Post")]
		public IActionResult Update([DataSourceRequest] DataSourceRequest request, HighwayPrefix highwayPrefix)
		{
			_highwayPrefixRepository.Update(highwayPrefix, (int)highwayPrefix.Id);
			_highwayPrefixRepository.DisposeDBObjects();
			return Json(new[] { highwayPrefix }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs("Post")]
		public IActionResult Delete([DataSourceRequest] DataSourceRequest request, HighwayPrefix highwayPrefix)
		{
			_highwayPrefixRepository.Delete((int)highwayPrefix.Id);
			_highwayPrefixRepository.DisposeDBObjects();
			return Json(new[] { highwayPrefix }.ToDataSourceResult(request, ModelState));
		}
	}
}
