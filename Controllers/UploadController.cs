

using Document_Saver.Data;
using Document_Saver.Models;
using Document_Saver.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;

namespace Document_Saver.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {
        private readonly DocumentDetailsContext _DB;
        private readonly JWTTokenServices _jWTTokenServices;
        private readonly UserRepository _userRepository;
        private IConfiguration _config;
        public UploadController(DocumentDetailsContext DB, IConfiguration config, JWTTokenServices JWTTokenServices, UserRepository userRepository)
        {
            _DB = DB;
            _jWTTokenServices = JWTTokenServices;
            _config = config;
            _userRepository = userRepository;
        }
        [HttpGet]
        //[Authorize]
        private User GetUser(User userInfo)
        {
            return _userRepository.GetUser(userInfo);
        }
        public IActionResult Index(string Sorting_Order,string Search_Data)
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return RedirectToAction("Login", "User");
            }


            if (!_jWTTokenServices.IsTokenValid(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), token))
            {
                return (RedirectToAction("Dashboard"));
            }
            ViewBag.Message = BuildMessage(token, 50);
         
            IEnumerable<Documents> objectDocumentlist = _DB.Document;

            var students = from stu in _DB.Document select stu;
            {
                students = students.Where(stu => stu.Document_Name.ToUpper().Contains(Search_Data.ToUpper()));
                  
            }
            ViewBag.SortingDocumentName = String.IsNullOrEmpty(Sorting_Order) ? "Document_Name" : "";

            var Documents = from stu in _DB.Document.AsQueryable() select stu;
            switch (Sorting_Order)
            {
                case "Document_Name":
                    Documents = Documents.OrderByDescending(stu => stu.Document_Name);
                    break;
                default:
                    Documents = Documents.OrderBy(stu => stu.Document_Name);
                    break;

            }
            return View(Documents.ToList());
           
            return View(objectDocumentlist);
        }


        public IActionResult Error()
        {
            ViewBag.Message = "An error occured...";
            return View();
        }

        private string BuildMessage(string stringToSplit, int chunkSize)
        {
            var data = Enumerable.Range(0, stringToSplit.Length / chunkSize).Select(i => stringToSplit.Substring(i * chunkSize, chunkSize));
            string result = "The generated token is:";
            foreach (string str in data)
            {
                result += Environment.NewLine + str;
            }
            return result;
        }
        public IActionResult Download(string fileName)
          {
              if (fileName == null)
              {
                  return Content("filename not present");
              }
              else
              {

                  var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);

                  byte[] bytes = System.IO.File.ReadAllBytes(filepath);


                  return File(bytes, "application/octet-stream", fileName);
              }
          }
        [HttpGet]
       // [Authorize]
        
        public IActionResult Upload()
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return (RedirectToAction("Index"));
            }


            if (!_jWTTokenServices.IsTokenValid(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), token))
            {
                return (RedirectToAction("Dashboard"));
            }
            ViewBag.Message = BuildMessage(token, 50);

            return View();
        }
   
        [HttpPost]
        public IActionResult Upload(IFormFile files,Documents obj)
        {
            if (files != null)
            {
                if (files.Length > 0)
                {
                    
                    var fileName = Path.GetFileName(files.FileName);

                  
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                  
                    var fileExtension = Path.GetExtension(fileName);

                  
                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

              
                    var filepath =new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files")).Root + $@"\{newFileName}";

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        files.CopyTo(fs);
                        fs.Flush();
                    }
                    var fileModel = new Documents
                    {
                        Document_Id = obj.Document_Id,
                        Document_Name = fileName,
                        File_Name = newFileName,
                        File_Type = fileExtension,

                        Process_Id = obj.Process_Id = Process.GetCurrentProcess().Id.ToString(),
                        //ProjectDetails=obj.ProjectDetails,
                        Created_At = obj.Created_At,
                        Created_By = obj.Created_By,
                        Project_Id = obj.Project_Id,
                        Is_Active = obj.Is_Active,
                        Is_Deleted = obj.Is_Deleted,
                        Updated_At = obj.Updated_At,
                        Updated_By = obj.Updated_By,
                       /* Process_Id= obj.Process_Id = Process.GetCurrentProcess().Id.ToString()*/
                };

                    _DB.Document.Add(fileModel);
                    _DB.SaveChanges();
                }
                TempData["Message"] = "File successfully uploaded";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? Document_Id)
        {
            if (Document_Id == null || Document_Id == 0)
            {
                return NotFound();
            }
            Documents document = _DB.Document.FirstOrDefault(u => u.Document_Id == Document_Id);

            if (document == null)
            {
                return NotFound();
            }
            return View(document);
        }
        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Document_Id)
        {
            var obj = _DB.Document.Find(Document_Id);
            if (obj == null)
            {
                return NotFound();
            }
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files");
            var oldfile = Path.Combine(filepath, obj.File_Name);
            if (System.IO.File.Exists(oldfile))
            {
                System.IO.File.Delete(oldfile);
            }
            _DB.Document.Remove(obj);
            _DB.SaveChanges();
            return RedirectToAction("Index");



        }



    }
}