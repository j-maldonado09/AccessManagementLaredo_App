using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using AccessManagementLaredo;
using AccessManagementLaredo.HelperModels;
using AccessManagementLaredo.ViewModels;
using System.Text.Json;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Nodes;

namespace AccessManagementLaredo_App.Controllers
{
	//[Authorize(Roles = "SUPERADMIN,ADMIN,SUPERVISOR,USER")]
	public class HighwayController : Controller
	{
		private IHighwayPrefixRepository _highwayPrefixRepository;
		private IHighwayRepository _highwayRepository;
		private ICountyRepository _countyRepository;

		public HighwayController(IHighwayPrefixRepository highwayPrefixRepository, IHighwayRepository highwayRepository, ICountyRepository countyRepository)
		{
			_highwayPrefixRepository = highwayPrefixRepository;
			_highwayRepository = highwayRepository;
			_countyRepository = countyRepository;
		}

        //public IActionResult Index()
        //{
        //	List<County> counties = GetCounties().ToList();			
        //          ViewData["counties"] = counties;
        //          List<HighwayViewModel> model = Read(counties[0].Id);
        //          return View(model);
        //}

        public IActionResult Index2()
        {
            List<County> counties = GetCounties().ToList();
            ViewData["counties"] = counties;
            //List<HighwayViewModel> model = Read(counties[0].Id);
            return View();
        }

        [HttpPost]
		public IActionResult Create([DataSourceRequest] DataSourceRequest request, [FromBody] Highway highway)
		{
			highway.Id = _highwayRepository.Create(highway);
			_highwayRepository.DisposeDBObjects();
			return Json(new[] { highway }.ToDataSourceResult(request, ModelState));
		}

		//public IActionResult Read([DataSourceRequest] DataSourceRequest request)
		//{
		//	string result = _highwayRepository.Read();
		//	IQueryable<Highway> highways = JsonSerializer.Deserialize<List<Highway>>(result).AsQueryable();
		//	_highwayRepository.DisposeDBObjects();
		//	DataSourceResult dsResult = highways.ToDataSourceResult(request);
		//	return Json(dsResult);
		//}

		public IActionResult Read([DataSourceRequest] DataSourceRequest request, int id)
		{
			string result = _highwayRepository.ReadByCounty(id);
			IQueryable<HighwayViewModel> highways = JsonSerializer.Deserialize<List<HighwayViewModel>>(result).AsQueryable();
			_highwayRepository.DisposeDBObjects();
			DataSourceResult dsResult = highways.ToDataSourceResult(request);
			return Json(dsResult);
		}

		//public List<HighwayViewModel> Read(int id)
		//{
		//	string result = _highwayRepository.ReadByCounty(id);
		//	List<HighwayViewModel> highways = JsonSerializer.Deserialize<List<HighwayViewModel>>(result);
		//	_highwayRepository.DisposeDBObjects();
		//	return highways;
		//}

		[AcceptVerbs("Post")]
		public IActionResult Update([DataSourceRequest] DataSourceRequest request, [FromBody] Highway highway)
		{
			_highwayRepository.Update(highway, (int)highway.Id);
			_highwayRepository.DisposeDBObjects();
			return Json(new[] { highway }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs("Post")]
		public IActionResult Delete([DataSourceRequest] DataSourceRequest request, [FromBody] Highway highway)
		{
			_highwayRepository.Delete((int)highway.Id);
			_highwayRepository.DisposeDBObjects();
            return Json(new[] { highway }.ToDataSourceResult(request, ModelState));
        }

        public IEnumerable<County> GetCounties()
		{
			string result = _countyRepository.Read();
			IEnumerable<County> counties = JsonSerializer.Deserialize<List<County>>(result).AsEnumerable();
			_countyRepository.DisposeDBObjects();
			return counties;
		}

        public IEnumerable<HighwayPrefix> GetPrefixes()
        {
            string result = _highwayPrefixRepository.Read();
            IEnumerable<HighwayPrefix> prefixes = JsonSerializer.Deserialize<List<HighwayPrefix>>(result).AsEnumerable();
            _highwayPrefixRepository.DisposeDBObjects();
            return prefixes;
        }

        
    }
}
