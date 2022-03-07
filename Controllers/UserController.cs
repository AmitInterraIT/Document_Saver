using Document_Saver.Data;
using Document_Saver.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace Document_Saver.Controllers
{
    public class UserController : Controller
    {

        private readonly DocumentDetailsContext _DB;
        public UserController(DocumentDetailsContext DB)
        {
            _DB = DB;
        }
        public IActionResult Index()
        {
            IEnumerable<User> objcategoriesList = _DB.UserDetails;
            return View(objcategoriesList);
        }
        //post
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult VerifyMsg()
        {
            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User obj)
        {
            if (ModelState.IsValid)
            {
                _DB.UserDetails.Add(obj);
                _DB.SaveChanges();
                return RedirectToAction("VerifyMsg");

            }
            return View(obj);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User obj)
        {

            var us = _DB.UserDetails.Where(x => x.User_Email.Equals(obj.User_Email) && x.User_Password.Equals(obj.User_Password)).FirstOrDefault();
            if (us.Status == 0)
            {
                ViewBag.Status = "Not Verified";
            }
            else if (us.Status == 1)
            {
                TempData["UserName"] = us.User_Name;
                return RedirectToAction("Dashboard");
            }
            else if(us.User_Name=="Admin" && us.User_Password=="Admin")
            {
                return RedirectToAction("Index");
            }


            return View();
        }
       
    }
 }

