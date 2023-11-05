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

        public IActionResult SearchResult(string recordName)
        {
            if(string.IsNullOrWhiteSpace(recordName))
            {
                return View("Index");
            }

            var searchResult = _db.LicenseTemplates
                .Where(x => EF.Functions.Like(x.FullName, $"%{recordName}%"))
                .ToList();
            return View("SearchResult", searchResult);
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

            return View("CreateRecord", viewModel); // If there are validation errors, return to the same page
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