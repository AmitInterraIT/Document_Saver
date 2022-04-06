

using Document_Saver.Data;
using Document_Saver.Models;
using Microsoft.AspNetCore.Mvc;

namespace Document_Saver.Controllers
{
    public class UploadController : Controller
    {
        private readonly DocumentDetailsContext _DB;
        public UploadController(DocumentDetailsContext DB)
        {
            _DB = DB;
        }
        [HttpGet]

        public IActionResult Index()
        {
            IEnumerable<Documents> objectDocumentlist = _DB.Document;
            return View(objectDocumentlist);


        }


        public IActionResult Download(string filename)
        {
            if (filename == null)
            {
                return Content("filename not present");
            }
            else
            {

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", filename);

                byte[] bytes = System.IO.File.ReadAllBytes(filepath);


                return File(bytes, "application/octet-stream", filename);
            }
        }



        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files, Documents obj)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var filePath = Path.Combine(basePath, file.FileName);
                var extension = Path.GetExtension(file.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var fileModel = new Documents
                    {
                        Document_Id = obj.Document_Id,
                        Document_Name = obj.Document_Name,
                        File_Name = filePath,
                        Process_Id = obj.Process_Id,
                        //ProjectDetails=obj.ProjectDetails,
                        Created_At = obj.Created_At,
                        Created_By = obj.Created_By,
                        Project_Id = obj.Project_Id,
                        Is_Active = obj.Is_Active,
                        Is_Deleted = obj.Is_Deleted,
                        Updated_At = obj.Updated_At,
                        Updated_By = obj.Updated_By,
                    };
                    _DB.Document.Add(fileModel);
                    _DB.SaveChanges();
                }
            }

            TempData["Message"] = "File successfully uploaded";
            return RedirectToAction("Index");
        }
        //get Delete
        public IActionResult Delete(int? Document_Id)
        {
            if (Document_Id == null || Document_Id == 0)
            {
                return NotFound();
            }
            var UserTypeDb = _DB.Document.Find(Document_Id);

            if (UserTypeDb == null)
            {
                return NotFound();
            }
            return View(UserTypeDb);
        }
        //post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Document_Id)
        {
            var obj = _DB.Document.Find(Document_Id);
            if (obj == null)
            {
                return NotFound();
            }

            _DB.Document.Remove(obj);
            _DB.SaveChanges();
            TempData["success"] = "Data Deleted Successfully";
            return RedirectToAction("Index");

        }
        public FileResult ViewFile(string fileName)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);
            byte[] pdfByte = System.IO.File.ReadAllBytes(filepath);
            return File(pdfByte, "application/pdf");
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        public ActionResult Index(string searching)
        {
            return View(_DB.Document.Where(x => x.Document_Name.Contains(searching) || searching == null).ToList());
         


        }
    }

}