

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

       
      
        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            try
            {
                string filename = file.FileName;
                filename = Path.GetFileName(filename);
                string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\css", filename);
                var stream = new FileStream(uploadfilepath, FileMode.Create);
                file.CopyToAsync(stream);
                ViewBag.ImageUrl = "Files/" + file;
            }

            catch (Exception ex)
            {
                ViewBag.Message ="Error: " +ex.Message.ToString();
            }

            return View();
            


        }

        public async Task<IActionResult> Download(IFormFile file)
        {
            if (file == null)
                return Content("filename is not availble");
            string filename = file.FileName;
            filename = Path.GetFileName(filename);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\css", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
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
       

    }

}