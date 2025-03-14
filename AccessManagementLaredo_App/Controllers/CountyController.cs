using AccessManagementLaredo;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace AccessManagementLaredo_App.Controllers
{
    public class CountyController : Controller
    {
        private ICountyRepository _countyRepository;

        public CountyController(ICountyRepository countyRepository)
        {
            _countyRepository = countyRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("Post")]
        public IActionResult Create([DataSourceRequest] DataSourceRequest request, County county)
        {
            county.Id = _countyRepository.Create(county);
            _countyRepository.DisposeDBObjects();
            return Json(new[] { county }.ToDataSourceResult(request, ModelState));
        }

        public IActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            string result = _countyRepository.Read();
            IQueryable<County> counties = JsonSerializer.Deserialize<List<County>>(result).AsQueryable();
            _countyRepository.DisposeDBObjects();
            DataSourceResult dsResult = counties.ToDataSourceResult(request);
            return Json(dsResult);
        }

        [AcceptVerbs("Post")]
        public IActionResult Update([DataSourceRequest] DataSourceRequest request, County county)
        {
            _countyRepository.Update(county, (int)county.Id);
            _countyRepository.DisposeDBObjects();
            return Json(new[] { county }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public IActionResult Delete([DataSourceRequest] DataSourceRequest request, County county)
        {
            _countyRepository.Delete((int)county.Id);
            _countyRepository.DisposeDBObjects();
            return Json(new[] { county }.ToDataSourceResult(request, ModelState));
        }
    }
}
