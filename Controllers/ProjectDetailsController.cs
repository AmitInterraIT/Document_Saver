using Document_Saver.Data;
using Document_Saver.Models;
using Document_Saver.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Document_Saver.Controllers
{
    
    public class ProjectDetailsController : Controller
    {
        private readonly DocumentDetailsContext _DB;
        private readonly JWTTokenServices _jWTTokenServices;
        private readonly UserRepository _userRepository;
        private IConfiguration _config;
        public ProjectDetailsController(IConfiguration config,DocumentDetailsContext DB, JWTTokenServices JWTTokenServices, UserRepository userRepository)
        {
            _DB = DB;
            _jWTTokenServices = JWTTokenServices;
            _userRepository = userRepository;
            _config = config;
        }
        [HttpGet]
        [Authorize]

        private User GetUser(User userInfo)
        {
            return _userRepository.GetUser(userInfo);
        }
        public IActionResult Dashboard()
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return (RedirectToAction("Index"));
            }


            if (!_jWTTokenServices.IsTokenValid(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), token))
            {
                return (RedirectToAction("Index"));
            }
            ViewBag.Message = BuildMessage(token, 50);
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
}

