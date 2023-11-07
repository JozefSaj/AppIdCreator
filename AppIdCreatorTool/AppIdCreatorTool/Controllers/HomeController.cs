using AppIdCreatorTool.Dal;
using AppIdCreatorTool.Model;
using AppIdCreatorTool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppIdCreatorTool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchResult(string recordName, int pg = 1)
        {
            if(string.IsNullOrWhiteSpace(recordName))
            {
                ModelState.AddModelError("CustomError", "Empty or WhiteSpace string is not accepted");
                return View("Index");
            }
            
            var searchResult = GetSearchResults(recordName, pg, out int count);
            if(searchResult.Count == 0)
            {
                ModelState.AddModelError("CustomError", "No result found based on given string");
                return View("Index");

            }
            var pager = CreatePager(recordName, pg, count);
            ViewBag.Pager = pager;
            
            return View("SearchResult", searchResult);
        }

        private List<LicenseTemplateModel> GetSearchResults(string recordName, int pg, out int count)
        {
            var skip = (pg - 1) * PagerModel.PageSize; 

            var searchResult = _db.LicenseTemplates
                .Where(x => EF.Functions.Like(x.FullName, $"%{recordName}%"))
                .Skip(skip)
                .Take(PagerModel.PageSize)
                .ToList();

            count = _db.LicenseTemplates
                .Count(x => EF.Functions.Like(x.FullName, $"%{recordName}%"));

            return searchResult;
        }

        private PagerModel CreatePager(string recordName, int pg, int count)
        {
            var pager = new PagerModel(count, pg)
            {
                Controller = "Home",
                Action = "SearchResult",
                RecordName = recordName
            };
            return pager;
        }

        public IActionResult CreateRecord(int ID)
        {
            var selectedRecord = _db.LicenseTemplates.FirstOrDefault(lt => lt.ID == ID);

            if (selectedRecord == null)
            {
                return NotFound(); 
            }

            return View("CreateRecord", selectedRecord);
        }
        [HttpPost]
        public IActionResult SaveNewRecord(TemporaryLicenseTemplateModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.CurrentStatus = StatusType.Pending;
                viewModel.DatePublished = DateTime.Now;
                _db.TemporaryLicenseTemplateModels.Add(viewModel);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("CreateRecord"); // If there are validation errors, return to the same page
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}