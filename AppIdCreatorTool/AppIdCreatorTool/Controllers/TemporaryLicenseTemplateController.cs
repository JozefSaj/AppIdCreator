using AppIdCreatorTool.Dal;
using Microsoft.AspNetCore.Mvc;
using AppIdCreatorTool.Model;

namespace AppIdCreatorTool.Controllers
{
    public class TemporaryLicenseTemplateController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TemporaryLicenseTemplateController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var records = _db.TemporaryLicenseTemplateModels.ToList();
            return View(records);
        }

        public IActionResult AcceptRecord(int ID)
        {
            var acceptedRecord = _db.TemporaryLicenseTemplateModels
                .FirstOrDefault(x => x.ID == ID);
            
            if (acceptedRecord == null)
            {
                return NotFound();
            }
            acceptedRecord.CurrentStatus = StatusType.Accepted;
            _db.TemporaryLicenseTemplateModels.Update(acceptedRecord);
            _db.LicenseTemplates.Add(
                new LicenseTemplateModel
                {
                    FullName = acceptedRecord.FullName,
                    ShortName = acceptedRecord.ShortName,
                    LicenseType = acceptedRecord.LicenseType,
                    DatePublished = acceptedRecord.DatePublished
                }
            );
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeclineRecord(int ID)
        {
            var recordToDecline = _db.TemporaryLicenseTemplateModels
                .FirstOrDefault(x => x.ID == ID);

            if (recordToDecline == null)
            {
                return NotFound();
            }

            recordToDecline.CurrentStatus = StatusType.Declined;
            _db.TemporaryLicenseTemplateModels.Update(recordToDecline);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
