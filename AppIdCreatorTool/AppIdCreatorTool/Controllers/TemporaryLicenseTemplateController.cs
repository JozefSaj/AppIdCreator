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

        public IActionResult Index(int pg=1)
        {
            var data = GetRecords(pg, out int count);
            var pager = CreatePager(pg, count);
            ViewBag.Pager = pager;

            return View("Index", data);
        }

        private List<TemporaryLicenseTemplateModel> GetRecords(int pg, out int count)
        {
            var recSkip = (pg - 1) * PagerModel.PageSize;
            count = _db.TemporaryLicenseTemplateModels.Count();
            var data = _db.TemporaryLicenseTemplateModels.Skip(recSkip).Take(PagerModel.PageSize).ToList();
            return data;

        }
        private PagerModel CreatePager(int pg, int count)
        {
            var pager = new PagerModel(count, pg)
            {
                Controller = "TemporaryLicenseTemplate",
                Action = "Index",
            };
            return pager;
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

        public IActionResult ShowNote(int ID)
        {
            var record = _db.TemporaryLicenseTemplateModels
                .FirstOrDefault(x => x.ID == ID);
            return View("ShowNote", record);
        }
        [HttpPost]
        public IActionResult SaveNote(int ID, string Note)
        {
            var record = _db.TemporaryLicenseTemplateModels.FirstOrDefault(lt => lt.ID == ID);

            if (record == null)
            {
                return NotFound();
            }
            record.Note = Note;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
