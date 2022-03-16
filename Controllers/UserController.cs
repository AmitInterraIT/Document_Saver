using Document_Saver.Data;
using Document_Saver.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;


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
        public async Task< IActionResult> Login (User obj,string ReturnUrl,string User_Name)
        {
           
                        var us = _DB.UserDetails.Where(x => x.User_Name.Equals(obj.User_Name)).FirstOrDefault();
                        if (us.Status == 0)
                        {
                            TempData["AlertMessage"] = "Your Account is not verified yet...";
                        }
                     
                        if (us.User_Password!= obj.User_Password)
                        {
                            TempData["AlertMessage"] = "Please Enter Right Password";
                        }
           else if (us.Status == 1)
           {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, obj.User_Name),
                   
                };
                var claimsIdentity = new ClaimsIdentity(
                    
                    
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var props = new AuthenticationProperties();
 
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Dashboard", "ProjectDetails");

                            
           }
            if ((obj.User_Name == "Admin") && (obj.User_Password == "Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }


            return View();
            
        }
       [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "User");
        }
        
    }
 }
  
   

