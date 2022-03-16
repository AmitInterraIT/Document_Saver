using Document_Saver.Data;
using Document_Saver.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Document_Saver.Controllers
{

    [Authorize]
    public class ProjectDetailsController : Controller
    {
        /*        private readonly DocumentDetailsContext _DB;
                public ProjectDetailsController(DocumentDetailsContext DB)
                {
                    _DB = DB;
                }
                public IActionResult Index()
                {

                    return View();
                }*/
        public IActionResult Dashboard()
        {
            /*IEnumerable<ProjectDetails> objcategoriesList = _DB.ProjectDetails;*/
            return View();

        }
    }
}
