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
	public class ConstructionTypeController : Controller
	{
		private IConstructionTypeRepository _constructionTypeRepository;

		public ConstructionTypeController(IConstructionTypeRepository constructionTypeRepository)
		{
			_constructionTypeRepository = constructionTypeRepository;
		}

		public IActionResult Index()
		{
			return View();
		}

		[AcceptVerbs("Post")]
		public IActionResult Create([DataSourceRequest] DataSourceRequest request, ConstructionType constructionType)
		{
			constructionType.Id = _constructionTypeRepository.Create(constructionType);
			_constructionTypeRepository.DisposeDBObjects();
			return Json(new[] { constructionType }.ToDataSourceResult(request, ModelState));
		}

		public IActionResult Read([DataSourceRequest] DataSourceRequest request)
		{
			string result = _constructionTypeRepository.Read();
			IQueryable<ConstructionType> constructionTypes = JsonSerializer.Deserialize<List<ConstructionType>>(result).AsQueryable();
			_constructionTypeRepository.DisposeDBObjects();
			DataSourceResult dsResult = constructionTypes.ToDataSourceResult(request);
			return Json(dsResult);
		}

		[AcceptVerbs("Post")]
		public IActionResult Update([DataSourceRequest] DataSourceRequest request, ConstructionType constructionType)
		{
			_constructionTypeRepository.Update(constructionType, (int)constructionType.Id);
			_constructionTypeRepository.DisposeDBObjects();
			return Json(new[] { constructionType }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs("Post")]
		public IActionResult Delete([DataSourceRequest] DataSourceRequest request, ConstructionType constructionType)
		{
			_constructionTypeRepository.Delete((int)constructionType.Id);
			_constructionTypeRepository.DisposeDBObjects();
			return Json(new[] { constructionType }.ToDataSourceResult(request, ModelState));
		}
	}
}
