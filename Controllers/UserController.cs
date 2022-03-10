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
                var isEmailAlreadyExists = _DB.UserDetails.Any(x => x.User_Email == obj.User_Email);
                if (isEmailAlreadyExists)
                {
                    TempData["AlertMessage"] = "Email Id is Already Registered...";
                    return View(obj);
                }

                User newobj = new User();
                newobj.User_Emp_Id = obj.User_Emp_Id;
                newobj.User_Email = obj.User_Email;
                _DB.UserDetails.Add(obj);
                _DB.SaveChanges();
                TempData["AlertMessage"] = "Registered Successfully...";
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

            var us = _DB.UserDetails.Where(x => x.User_Name.Equals(obj.User_Name)).FirstOrDefault();
            if (us.Status == 0)
            {
                TempData["AlertMessage"] = "Your Account is not verified yet...";
            }
            var usS = _DB.UserDetails.Where(x => x.User_Password.Equals(obj.User_Password)).FirstOrDefault();
            if (usS == null)
            {
                TempData["AlertMessage"] = "Please Enter Right Password";
            }
            else if (us.Status == 1)
            {

                return RedirectToAction("Dashboard", "ProjectDetails");
            }



            return View();
        }
      
    }
 }

