using Document_Saver.Data;
using Document_Saver.Models;
using Microsoft.AspNetCore.Mvc;

namespace Document_Saver.Controllers
{
    public class AdminController : Controller
    {
        private readonly DocumentDetailsContext _DB;
        public AdminController(DocumentDetailsContext DB)
        {
            _DB = DB;
        }
        public IActionResult Index()
        {
            IEnumerable<User> objcategoriesList = _DB.UserDetails;
            return View(objcategoriesList);
          
        }
        
        public IActionResult Edit(int?User_Id)
        {
            if (User_Id == null || User_Id == 0)
            {
                return NotFound();
            }
            var objcategoriesList = _DB.UserDetails.Find(User_Id);

            if (objcategoriesList == null)
            {
                return NotFound();
            }
            return View(objcategoriesList);
        }
        //post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User obj)
        {

            if (ModelState.IsValid)
            {
                _DB.UserDetails.Update(obj);
                _DB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //get Delete
        public IActionResult Delete(int? User_Id)
        {
            if (User_Id == null || User_Id == 0)
            {
                return NotFound();
            }
            var UserTypeDb = _DB.UserDetails.Find(User_Id);
           
            if (UserTypeDb == null)
            {
                return NotFound();
            }
            return View(UserTypeDb);
        }
        //post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? User_Id)
        {
            var obj = _DB.UserDetails.Find(User_Id);
            if (obj == null)
            {
                return NotFound();
            }

            _DB.UserDetails.Remove(obj);
            _DB.SaveChanges();
            TempData["success"] = "Data Deleted Successfully";
            return RedirectToAction("Index");

        }
    }
   
}
