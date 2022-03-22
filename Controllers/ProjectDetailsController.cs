using Document_Saver.Data;
using Document_Saver.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Dashboard()
        {
            /*IEnumerable<ProjectDetails> objcategoriesList = _DB.ProjectDetails;*/
            return View();

        }
        public IActionResult Table()
        {

            IEnumerable<ProjectDetails> objProjectList = _DB.ProjectDetails;
            return View(objProjectList);
        }
    }
}
