using Document_Saver.Data;
using Document_Saver.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Document_Saver.Controllers
{

    public class ProjectDetailsController : Controller
    {
        private readonly DocumentDetailsContext _DB;
        public ProjectDetailsController(DocumentDetailsContext DB)
        {
            _DB = DB;
        }



        public IActionResult Dashboard()
        {
            return View();
            /*var accessToken = HttpContext.Session.GetString("JWTOken");
            var Url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization= new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",accessToken);
            string jsonstr =await client.GetStringAsync(Url);
            var res = JsonConvert.DeserializeObject<List<IActionResult>>(jsonstr);
            return res;*/


        }
        public IActionResult Table()
        {

            IEnumerable<ProjectDetails> objProjectList = _DB.ProjectDetails;
            return View(objProjectList);
        }

        public IActionResult Create(ProjectDetails obj)
        {
            if (ModelState.IsValid)
            {
                _DB.ProjectDetails.Add(obj);
                int projectid = _DB.SaveChanges();
                //foreach (var item in obj.ProjectMembers)
                //{

                //    ProjectMember pm = new ProjectMember();
                //    pm.Project_Id = projectid;
                //    pm.User_Id = item;
                //    _DB.ProjectMember.Add(pm);
                //    if (obj?.ProjectMembers != null && obj?.ProjectMembers.Count != 0)
                //    {
                //        _DB.SaveChanges();
                //    }



                TempData["success"] = "Data Created Successfully";
                return RedirectToAction("Table");
            }
            return View(obj);
        }
        //GET
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var ProjectDetailsFromDb = _DB.ProjectDetails.Find(Id);

            if (ProjectDetailsFromDb == null)
            {
                return NotFound();
            }
            return View(ProjectDetailsFromDb);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProjectDetails obj)
        {

            if (ModelState.IsValid)
            {
                _DB.ProjectDetails.Update(obj);
                _DB.SaveChanges();
                TempData["success"] = "Data Updated Successfully";
                return RedirectToAction("Table");
            }
            return View(obj);
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var ProjectDetailsFromDb = _DB.ProjectDetails.Find(Id);
            /*var categoryFromDbFirst = _DB.categories.FirstOrDefault(u => u.Id == Id);
            var categoryFromDbsingle = _DB.categories.SingleOrDefault(u => u.Id == Id);*/
            if (ProjectDetailsFromDb == null)
            {
                return NotFound();
            }
            return View(ProjectDetailsFromDb);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Project_Id)
        {
            var obj = _DB.ProjectDetails.Find(Project_Id);
            if (obj == null)
            {
                return NotFound();
            }

            _DB.ProjectDetails.Remove(obj);
            _DB.SaveChanges();
            TempData["success"] = "Data Deleted Successfully";
            return RedirectToAction("Table");



        }
        public IActionResult Projects()
        {

            IEnumerable<ProjectDetails> objProjectList = _DB.ProjectDetails;
            return View(objProjectList);
        }
        [HttpPost]
        public IActionResult AddMember(List<User> MemberList)
        {
            return View();
        }
    }



    /*  public static string baseUrl = "http://localhost:9762/api/Login";
      [HttpGet]*/
    /*public async Task <IActionResult> Index()
    {

        return View();

    }*/

}

