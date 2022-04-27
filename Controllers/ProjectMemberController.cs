using Document_Saver.Data;
using Microsoft.AspNetCore.Mvc;

namespace Document_Saver.Controllers
{
    public class ProjectMemberController : Controller
    {
        private readonly DocumentDetailsContext _DB;
        public ProjectMemberController(DocumentDetailsContext DB)
        {
            _DB = DB;
        }
        public IActionResult Index()
        {
            return View();
        }
    }


       



 }
