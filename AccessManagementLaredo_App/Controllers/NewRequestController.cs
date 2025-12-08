using AccessManagementLaredo;
using AccessManagementLaredo.HelperModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using AccessManagementLaredo_App.Models;
using System.Security.Claims;
using AccessManagementLaredo_App.Services;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using AccessManagementLaredo.ViewModels;

namespace AccessManagementLaredo_App.Controllers
{
    public class NewRequestController : Controller
    {
        // variables ****************************************************************************
        //private IPermitRequestResidentialRepository _permitRequestResidentialRepository;
        private IPermitRequestRepository _permitRequestRepository;
        private ICountyRepository _countyRepository;
        private IHighwayRepository _highwayRepository;
        private IConstructionTypeRepository _constructionTypeRepository;
        private IPermitEventRepository _permitEventRepository;
        private IAttachmentTypeRepository _attachmentTypeRepository;
        private UserManager<ApplicationUser> _userManager;
        private IdentityServices _identityServices;
        private PermitEventHelperModel _permitEventHelperModel;
		private PermitRequestStatusHelperModel _permitRequestStatusHelperModel;
		private ExportPDF _exportPdf;
        private IWebHostEnvironment _webHostEnvironment;
		private IAttachmentRepository _attachmentRepository;
		private AttachmentHelperModel _attachmentHelperModel;

        // Controller ********************************************************************************************************************************************************************
        public NewRequestController(IPermitRequestRepository permitRequestRepository,ICountyRepository countyRepository , IHighwayRepository highwayRepository, 
            IConstructionTypeRepository constructionTypeRepository, IPermitEventRepository permitEventRepository, IAttachmentTypeRepository attachmentTypeRepository,
            UserManager<ApplicationUser> userManager, IdentityServices identityServices, PermitEventHelperModel permitEventHelperModel, 
			PermitRequestStatusHelperModel permitRequestStatusHelperModel, ExportPDF exportPDF, IWebHostEnvironment webHostEnvironment, IAttachmentRepository attachmentRepository)
        {
            _permitRequestRepository = permitRequestRepository;
			_countyRepository = countyRepository;
            _highwayRepository = highwayRepository;
            _constructionTypeRepository = constructionTypeRepository;
            _permitEventRepository = permitEventRepository;
            _attachmentTypeRepository = attachmentTypeRepository;
            _userManager = userManager;
            _identityServices = identityServices;
            _permitEventHelperModel = permitEventHelperModel;
			_permitRequestStatusHelperModel = permitRequestStatusHelperModel;
			_exportPdf = exportPDF;
			_webHostEnvironment = webHostEnvironment;
			_attachmentRepository = attachmentRepository;
        }

		// Index ********************************************************************************************************************************************************************
		public IActionResult Index()
        {
			ViewData["counties"] = GetCounties();
            ViewData["highways"] = GetHighways();
            ViewData["constructionTypes"] = GetConstructionTypes();
			ViewData["attachmentTypes"] = GetAttachmentTypes();
			ViewData["prefixesByCounty"] = GetPrefixesByCounty();

            return View();
        }

        // View to review permit requests ********************************************************************************************************************************************
        public IActionResult PermitRequestReview() 
		{ 
			return View();
		}


		// Action that creates a residential request ********************************************************************************************************************************
		[HttpPost]
        public IActionResult CreateResidential([DataSourceRequest] DataSourceRequest request, [FromBody] PermitRequestResidentialHelperModel permitRequestResidentialHelperModel)
        {
            var user = _identityServices.GetCurrentUserByName();
            //int sequenceValue = _permitRequestResidentialRepository.Create(permitRequestResidentialHelperModel);
            int sequenceValue = _permitRequestRepository.CreateResidential(permitRequestResidentialHelperModel);

            // An event is created every time a new residential request is created
            // First: Set values for event helper model
            _permitEventHelperModel.DateCreated = DateTime.Now;
			_permitEventHelperModel.PermitRequestId = sequenceValue;
            _permitEventHelperModel.PermitEventTypeCode = "RQST_STRT";
            _permitEventHelperModel.UserName = user.Result.ContactFullName;
            _permitEventHelperModel.UserRoleName = user.Result.ContactRole;
            _permitEventHelperModel.PermitEventComment = "";

			// Second: Create event
            _permitEventRepository.Create(_permitEventHelperModel);

			// Third: Update status
			//UpdateStatus(sequenceValue);

            permitRequestResidentialHelperModel.PermitRequestId = sequenceValue;
            _permitRequestRepository.DisposeDBObjects();
            return Json(new[] { permitRequestResidentialHelperModel }.ToDataSourceResult(request, ModelState));
        }

        // Action that creates a residential request ********************************************************************************************************************************
        [HttpPost]
        public IActionResult CreateCommercial([DataSourceRequest] DataSourceRequest request, [FromBody] PermitRequestCommercialHelperModel permitRequestCommercialHelperModel)
        {
            var user = _identityServices.GetCurrentUserByName();
            //int sequenceValue = _permitRequestResidentialRepository.Create(permitRequestResidentialHelperModel);
            int sequenceValue = _permitRequestRepository.CreateCommercial(permitRequestCommercialHelperModel);

            // An event is created every time a new residential request is created
            // First: Set values for event helper model
            _permitEventHelperModel.DateCreated = DateTime.Now;
            _permitEventHelperModel.PermitRequestId = sequenceValue;
            _permitEventHelperModel.PermitEventTypeCode = "RQST_STRT";
            _permitEventHelperModel.UserName = user.Result.ContactFullName;
            _permitEventHelperModel.UserRoleName = user.Result.ContactRole;
            _permitEventHelperModel.PermitEventComment = "";

            // Second: Create event
            _permitEventRepository.Create(_permitEventHelperModel);

            // Third: Update status
            //UpdateStatus(sequenceValue);

            permitRequestCommercialHelperModel.PermitRequestId = sequenceValue;
            _permitRequestRepository.DisposeDBObjects();
            return Json(new[] { permitRequestCommercialHelperModel }.ToDataSourceResult(request, ModelState));
        }

        // Action that updates an existing residential request **********************************************************************************************************************************
        [HttpPost]
		public IActionResult UpdateResidential([DataSourceRequest] DataSourceRequest request, [FromBody] PermitRequestResidentialHelperModel permitRequestResidentialHelperModel)
		{
            //_permitRequestRepository.Update(permitRequestResidentialHelperModel, (int)permitRequestResidentialHelperModel.PermitRequestId);
            _permitRequestRepository.UpdateResidential(permitRequestResidentialHelperModel, (int)permitRequestResidentialHelperModel.PermitRequestId);
            _permitRequestRepository.DisposeDBObjects();
			return Json(new[] { permitRequestResidentialHelperModel }.ToDataSourceResult(request, ModelState));
		}

        // Action that updates an existing commercial request **********************************************************************************************************************************
        [HttpPost]
        public IActionResult UpdateCommercial([DataSourceRequest] DataSourceRequest request, [FromBody] PermitRequestCommercialHelperModel permitRequestCommercialHelperModel)
        {
            //_permitRequestRepository.Update(permitRequestResidentialHelperModel, (int)permitRequestResidentialHelperModel.PermitRequestId);
            _permitRequestRepository.UpdateCommercial(permitRequestCommercialHelperModel, (int)permitRequestCommercialHelperModel.PermitRequestId);
            _permitRequestRepository.DisposeDBObjects();
            return Json(new[] { permitRequestCommercialHelperModel }.ToDataSourceResult(request, ModelState));
        }

        // Action that updates the internal revision section of a permit request ******************************************************************************************************
        [HttpPost]
        public void UpdateInternalReview([FromBody] PermitRequestInternalReviewHelperModel permitRequestInternalReviewHelperModel)
        {
            var user = _identityServices.GetCurrentUserByName();
            _permitRequestRepository.UpdateInternalSection(permitRequestInternalReviewHelperModel, (int)permitRequestInternalReviewHelperModel.PermitRequestId);

            // An event is created every time an internal user reviews (approve/reject) a permit request
            // If reviewAction is "pdf", no event is created and no status is updated.
			if (permitRequestInternalReviewHelperModel.ReviewAction == "form1058" || permitRequestInternalReviewHelperModel.ReviewAction == "finalPackage")
			{
				_permitRequestRepository.DisposeDBObjects();
				return;
			}

            // If reviewAction is not "pdf", then an event is created and the status of the request is updated.
            // First: Set values for event helper model
            _permitEventHelperModel.DateCreated = DateTime.Now;
			_permitEventHelperModel.PermitRequestId = permitRequestInternalReviewHelperModel.PermitRequestId;

			if (permitRequestInternalReviewHelperModel.ReviewAction == "reject")
                _permitEventHelperModel.PermitEventTypeCode = "RTRN_USR";
			else if (permitRequestInternalReviewHelperModel.ReviewAction == "review")
				_permitEventHelperModel.PermitEventTypeCode = "REVIEWED";
			else if (permitRequestInternalReviewHelperModel.ReviewAction == "approve")
                _permitEventHelperModel.PermitEventTypeCode = "APPRV";
            else if (permitRequestInternalReviewHelperModel.ReviewAction == "complete")
                _permitEventHelperModel.PermitEventTypeCode = "CMPLT";

            _permitEventHelperModel.UserName = user.Result.ContactFullName;
			_permitEventHelperModel.UserRoleName = user.Result.ContactRole;
			_permitEventHelperModel.PermitEventComment = permitRequestInternalReviewHelperModel.Comment;

			// Second: Create event
			_permitEventRepository.Create(_permitEventHelperModel);

			// Third: Update status
			UpdateStatus((int)permitRequestInternalReviewHelperModel.PermitRequestId, permitRequestInternalReviewHelperModel.ReviewAction);

			_permitRequestRepository.DisposeDBObjects();
            //return Json(new[] { permitRequestResidentialHelperModel }.ToDataSourceResult(request, ModelState));
        }

        // Action that reads a permit request *************************************************************************************************************************************************
        public JsonResult ReadPermitRequests(int id, string reqType)
		{
			string result = _permitRequestRepository.ReadPermitRequests(id, reqType);
			_permitRequestRepository.DisposeDBObjects();
			return Json(result);
		}

		// Mehotd that gets called when a request is finalized and submitted ******************************************************************************************************************
		public void SubmitPermitRequest(int id, string comment)
		{
			var user = _identityServices.GetCurrentUserByName();

			// An event is created at the moment of submitting a request and the status of the request gets updated
			// First: set values for the event helper model
			_permitEventHelperModel.DateCreated = DateTime.Now;
			_permitEventHelperModel.PermitRequestId = id;
			_permitEventHelperModel.PermitEventTypeCode = "RQST_SUBM";
			_permitEventHelperModel.UserName = user.Result.ContactFullName;
			_permitEventHelperModel.UserRoleName = user.Result.ContactRole;
			_permitEventHelperModel.PermitEventComment = comment;

			// Second: Create the event
			_permitEventRepository.Create(_permitEventHelperModel);

			// Third: Call the update status method
			UpdateStatus(id);

			_permitEventRepository.DisposeDBObjects();
		}

		// Method that updates the status of a permit request ******************************************************************************************************************
		public void UpdateStatus(int id, string? action = "")
		{
			// Check if methods that call This method get the current user. If so, then UpdateStatus can receive the user as an argument
			var user = _identityServices.GetCurrentUserByName();

			var result = _permitRequestRepository.Read(id);
			List<PermitRequestViewModel> permitRequestViewModel = JsonSerializer.Deserialize<List<PermitRequestViewModel>>(result);

			//int requestId = permitRequestViewModel[0].Id;

			string userRole = user.Result.ContactRole;
			string statusAreaOffice = permitRequestViewModel[0].StatusAreaOfficeCode;
			string statusTraffic = permitRequestViewModel[0].StatusTrafficCode;
			string statusTPD = permitRequestViewModel[0].StatusTPDCode;
			string statusExternal = permitRequestViewModel[0].StatusExternalCode;
			bool requiresTraffic = permitRequestViewModel[0].RequiresTraffic;
			bool requiresTPD = permitRequestViewModel[0].RequiresTPD;


			if (userRole == "OWNER" || userRole == "CONSULTANT")
			{
				if (statusExternal == "DRFT" || statusExternal == "AWACT")
				{
					statusAreaOffice = "AWACT";
					statusExternal = "UNDREV";
				}
			}
			else if (userRole == "AREAOFFICE" || userRole == "SUPERADMIN")
			{
				if (action == "reject")
				{
					statusExternal = "AWACT";
					statusAreaOffice = "RETUSR";
				}
				else if (action == "review")
				{
					statusAreaOffice = "UNDREV";

					if (!requiresTraffic && !requiresTPD)
					{
						//statusTraffic = "NA";
						//statusTPD = "NA";
					}
					else if (!requiresTraffic && requiresTPD)
					{
						//statusTraffic = "NA";
						statusTPD = "AWACT";
					}
					else if (requiresTraffic && !requiresTPD)
					{
						statusTraffic = "AWACT";
						//statusTPD = "NA";
					}
					else if (requiresTraffic && requiresTPD)
					{
						statusTraffic = "AWACT";
						statusTPD = "AWACT";
					}
				}
				else if (action == "approve")
				{
					statusExternal = "APPRV";
					statusAreaOffice = "APPRV";
					statusTraffic = "APPRV";
					statusTPD = "APPRV";
				}
			}
			else if (userRole == "TRAFFIC")
			{
				if (action == "review")
				{
					statusTraffic = "REVIEWED";
					//statusAreaOffice = "AWACT";
				}
			}
			else if (userRole == "TPD")
			{
				if (action == "review")
				{
					statusTPD = "REVIEWED";
					//statusAreaOffice = "AWACT";
				}
			}


			//if (userRole == "OWNER" || userRole == "CONSULTANT")
			//{
			//	if (statusExternal == "DRFT")
			//	{
			//		statusAreaOffice = "AWACT";
			//	}
			//	else if (statusExternal == "AWACT")
			//	{
			//		if (statusAreaOffice == "RETUSR")
			//		{
			//			statusAreaOffice = "AWACT";
			//		}
			//		else if (statusAreaOffice == "APPRV")
			//		{
			//			if (statusTraffic != "RETUSR" && statusTPD == "RETUSR")
			//			{
			//				statusTPD = "AWACT";
			//			}
			//			else if (statusTraffic == "RETUSR" && statusTPD != "RETUSR")
			//			{
			//				statusTraffic = "AWACT";
			//			}
			//			else if (statusTraffic == "RETUSR" && statusTPD == "RETUSR")
			//			{
			//				statusTPD = "AWACT";
			//				statusTraffic = "AWACT";
			//			}
			//		}
			//	}
			//	statusExternal = "UNDREV";
			//}
			//else if (userRole == "AREAOFFICE")
			//{
			//	if (action == "reject")
			//	{
			//		statusExternal = "AWACT";
			//		statusAreaOffice = "RETUSR";
			//	}
			//	else if (action == "approve")
			//	{
			//		statusExternal = "UNDREV";
			//		statusAreaOffice = "APPRV";

			//		if (!requiresTraffic && !requiresTPD)
			//		{
			//			statusTraffic = "NA";
			//			statusTPD = "NA";
			//		}
			//		else if (!requiresTraffic && requiresTPD)
			//		{
			//			statusTraffic = "NA";
			//			statusTPD = "AWACT";
			//		}
			//		else if (requiresTraffic && !requiresTPD)
			//		{
			//			statusTraffic = "AWACT";
			//			statusTPD = "NA";
			//		}
			//		else if (requiresTraffic && requiresTPD)
			//		{
			//			statusTraffic = "AWACT";
			//			statusTPD = "AWACT";
			//		}
			//	}
			//	else if (action == "complete")
			//	{
			//		statusExternal = "APPRV";
			//		statusAreaOffice = "COMPL";
			//		statusTraffic = "COMPL";
			//		statusTPD = "COMPL";
			//	}
			//}
			//else if (userRole == "TRAFFIC")
			//{
			//	if (action == "reject")
			//	{
			//		statusExternal = "AWACT";
			//		statusTraffic = "RETUSR";
			//	}
			//	else if (action == "approve") 
			//	{
			//                 //statusExternal = "UNDREV";
			//                 statusTraffic = "APPRV";
			//             }
			//}
			//         else if (userRole == "TPD")
			//         {
			//             if (action == "reject")
			//             {
			//                 statusExternal = "AWACT";
			//                 statusTPD = "RETUSR";
			//             }
			//             else if (action == "approve")
			//             {
			//                 //statusExternal = "UNDREV";
			//                 statusTPD = "APPRV";
			//             }
			//         }

			_permitRequestStatusHelperModel.Id = id;
			_permitRequestStatusHelperModel.StatusExternalCode = statusExternal;
			_permitRequestStatusHelperModel.StatusAreaOfficeCode = statusAreaOffice;
			_permitRequestStatusHelperModel.StatusTrafficCode = statusTraffic;
			_permitRequestStatusHelperModel.StatusTPDCode = statusTPD;

            _permitRequestRepository.UpdateStatus(_permitRequestStatusHelperModel);
        }

		// Return counties **************************************************************************************************************************
		public IEnumerable<County> GetCounties()
		{
			string result = _countyRepository.Read();
			IEnumerable<County> counties = JsonSerializer.Deserialize<List<County>>(result).AsEnumerable();
			_countyRepository.DisposeDBObjects();
			return counties;
		}

		// Return construction types ******************************************************************************************************************
		public IEnumerable<ConstructionType> GetConstructionTypes()
		{
			string result = _constructionTypeRepository.Read();
			IEnumerable<ConstructionType> constructionTypes = JsonSerializer.Deserialize<List<ConstructionType>>(result).AsEnumerable();
			_constructionTypeRepository.DisposeDBObjects();
			return constructionTypes;
		}

		// Return attachment types ******************************************************************************************************************
		public IEnumerable<AttachmentType> GetAttachmentTypes()
		{
			string result = _attachmentTypeRepository.Read();
			IEnumerable<AttachmentType> attachmentTypes = JsonSerializer.Deserialize<List<AttachmentType>>(result).AsEnumerable();
			_attachmentTypeRepository.DisposeDBObjects();
			return attachmentTypes;
		}

		// Return types of attachments ******************************************************************************************************************
		public string GetAttachmentTypesAux()
		{
			string result = _attachmentTypeRepository.Read();
			_attachmentTypeRepository.DisposeDBObjects();
			return result;
		}

		// Return unique prefixes per each county ******************************************************************************************************************
		public IEnumerable<PrefixByCountyHelperModel> GetPrefixesByCounty()
        {
            string result = _highwayRepository.ReadPrefixesByCounty();
            IEnumerable<PrefixByCountyHelperModel> prefixesByCounty = JsonSerializer.Deserialize<List<PrefixByCountyHelperModel>>(result).AsEnumerable();
            _highwayRepository.DisposeDBObjects();
			return prefixesByCounty;
        }

		// Return highway numbers ******************************************************************************************************************
		public IEnumerable<HighwayHelperModel> GetHighways()
		{
			string result = _highwayRepository.ReadHighways();
			IEnumerable<HighwayHelperModel> highways = JsonSerializer.Deserialize<List<HighwayHelperModel>>(result).AsEnumerable();
			_highwayRepository.DisposeDBObjects();		
			return highways;
		}

		// Generate the PDF file for form 1058 ******************************************************************************************
		public string ExportPdf([FromBody] PermitRequestCompositeHelperModel permitRequestCompositeHelperModel)
		{
			string physicalPath = Path.Combine(_webHostEnvironment.WebRootPath);
			string fileNameStart = "";

            _exportPdf.CreatePdf(permitRequestCompositeHelperModel, physicalPath);

			if (permitRequestCompositeHelperModel.PermitRequestInternalReviewHelperModel.ReviewAction == "form1058")
				fileNameStart = "Form1058_";
			else if (permitRequestCompositeHelperModel.PermitRequestInternalReviewHelperModel.ReviewAction == "finalPackage")
				fileNameStart = "FinalDoc_";

            return fileNameStart + permitRequestCompositeHelperModel.PermitRequestHelperModel.PermitRequestNumber + ".pdf";
        }

		// Saves the file that was uploaded with the kendo upload control ************************************************************************************
		public async Task<ActionResult> SaveUploadedFile(IEnumerable<IFormFile> files)
		{
			// The Name attribute of the Kendo Upload component is "files" and it must match the parameter name of this action.
			if (files != null)
			{
				foreach (var file in files)
				{
					var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

					// Some browsers send file names with full path.
					// We are only interested in the file name.
					var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
					var physicalPath = Path.Combine(_webHostEnvironment.WebRootPath, "files", fileName);

					using (var fileStream = new FileStream(physicalPath, FileMode.Create))
					{
						await file.CopyToAsync(fileStream);
					}
				}
			}

			// Return an empty string to signify success.
			return Content("");
		}

		// Removes the file that was uploaded with the kendo upload control *********************************************************************
		public ActionResult RemoveUploadedFile(string[] fileNames)
		{
			// The parameter of the Remove action must be called "fileNames".

			if (fileNames != null)
			{
				foreach (var fullName in fileNames)
				{
					var fileName = Path.GetFileName(fullName);
					var physicalPath = Path.Combine(_webHostEnvironment.WebRootPath, "files", fileName);

					// TODO: Verify user permissions.

					if (System.IO.File.Exists(physicalPath))
					{
						System.IO.File.Delete(physicalPath);
					}
				}
			}

			// Return an empty string to signify success.
			return Content("");
		}

		[HttpPost]
		public IActionResult SaveAttachments([FromBody] List<AttachmentHelperModel> attachments)
		{
			try
			{
				if (attachments == null || attachments.Count == 0)
					return Json(new { success = false, message = "No attachments data provided." });
                _attachmentRepository.UpdateAttachmentIncluded(attachments);
                _attachmentRepository.DisposeDBObjects();
				return Json(new { success = true, message = "Saved successfully." });
            }
			catch(Exception ex)
			{
                _attachmentRepository.DisposeDBObjects();
                return Json(new { success = false, message = $"Error saving attachments: {ex.Message}" });
            }            
        }
    }
}
