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

        public IActionResult Create(ProjectDetails obj)
        {
            if (ModelState.IsValid)
            {
                _DB.ProjectDetails.Add(obj);
                _DB.SaveChanges();
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

    }
}
