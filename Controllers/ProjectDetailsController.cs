using Document_Saver.Data;
using Document_Saver.Models;
using Microsoft.AspNetCore.Mvc;

namespace Document_Saver.Controllers
{
    public class ProjectDetailsController : Controller
    {
        private readonly DocumentDetailsContext _DB;
        public ProjectDetailsController(DocumentDetailsContext DB)
        {
            _DB = DB;
        }
        public IActionResult Index()
        {
            IEnumerable<ProjectDetails> objcategoriesList = _DB.ProjectDetails;
            return View(objcategoriesList);
        }
        public IActionResult Dashboard()
        {
            IEnumerable<ProjectDetails> objcategoriesList = _DB.ProjectDetails;
            return View(objcategoriesList);
        }

    }
}
