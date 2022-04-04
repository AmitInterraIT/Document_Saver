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
using Document_Saver.Services;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

namespace Document_Saver.Controllers
{

    public class UserController : Controller
    {

        private IConfiguration _config;
        private readonly DocumentDetailsContext _DB;
        public UserController(IConfiguration config, DocumentDetailsContext DB)
        {
            _config = config;
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

                obj.Process_Id = Process.GetCurrentProcess().Id.ToString();

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
            public async Task<IActionResult> Login(User obj, string ReturnUrl, string User_Name)
            {

                var us = _DB.UserDetails.Where(x => x.User_Name.Equals(obj.User_Name)).FirstOrDefault();
                if (us.Status == 0)
                {
                    TempData["AlertMessage"] = "Your Account is not verified yet...";
                }

                if (us.User_Password != obj.User_Password)
                {
                    TempData["AlertMessage"] = "Please Enter Right Password";
                }
                else if (us.Status == 1)
                {
                   

                    JWTTokenServices jwt = new JWTTokenServices(_config);
                    string token = jwt.GenerateJSONWebToken(obj);

                {
                    using (var httpClient = new HttpClient())
                    {
                        StringContent stringContent = new StringContent(JsonConvert.SerializeObject(obj));
                        using (var response = await httpClient.PostAsync("http://localhost:9762/api/Login", stringContent))
                        {
                            
                            if (token != null)
                            {
                                HttpContext.Session.SetString("token", token);
                              
                                
                            }

                            return RedirectToAction("Dashboard", "ProjectDetails");
                           
                        }


                      

                    }

                }


            }

                if ((obj.User_Name == "Admin") && (obj.User_Password == "Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }


                return View();

            }
        //public async Task<IActionResult> Login(User obj)

        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        StringContent stringContent = new StringContent(JsonConvert.SerializeObject(obj));
        //        using (var response = await httpClient.PostAsync("http://localhost:9762/api/Login", stringContent))
        //        {
        //            string token = await response.Content.ReadAsStringAsync();
        //            if (token == "Invalid credentials")
        //            {
        //                return RedirectToAction("Login", "User");
        //            }

        //            HttpContext.Session.SetString("token", token);
        //        }

        //        return RedirectToAction("Dashboard", "ProjectDetails");

        //    }
        
        //}
            [Route("logout")]
            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("Login", "User");
            }

        public ActionResult AddUser(string searching)
        {
            return View(_DB.UserDetails.Where(x => x.User_Name.Contains(searching) || searching == null).ToList());
            //IEnumerable<User> objcategoriesList = _DB.UserDetails;
            //ViewData["objcategoriesList"] = objcategoriesList;
            //return View(objcategoriesList);


        }


    }
    }
    


   

