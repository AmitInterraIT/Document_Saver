using Microsoft.AspNetCore.Mvc;

namespace Document_Saver.Controllers
{
    public class ProjectMemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
