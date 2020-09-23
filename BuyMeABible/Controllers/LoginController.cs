using BuyMeABible.Data.Interface;
using BuyMeABible.Data.Model;
using BuyMeABible.Data.Security;
using BuyMeABible.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BuyMeABible.Controllers
{
    public class LoginController : Controller
    {
        private IUserRepository _userRepository;

        public LoginController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<JsonResult> Validate(ReqLogin loginModel)
        {
            ViewBag.ErrorMessage = "";
            var existingUser = await _userRepository.GetUser(loginModel.Email);
            if (existingUser != null)
            {
                if (existingUser.Status == "Registered")
                {
                    ViewBag.ErrorMessage = "Account not yet activated.";
                    return Json(new JsonGenericResult
                    {
                        IsSuccess = false,
                        Message = "Email not verified."
                    }, JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("Index", new ReqLogin { Message = "Email not verified.", IsInvalid = true });
                }
            }

            var isUserValid = Membership.ValidateUser(loginModel.Email, loginModel.Password);
            if (isUserValid)
            {
                FormsAuthentication.SetAuthCookie(loginModel.Email, true);
                return Json(new JsonGenericResult
                {
                    IsSuccess = true
                }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index", "Home");
            }
            else
            {
                return Json(new JsonGenericResult
                {
                    IsSuccess = false,
                    Message = "Invalid username or password."
                }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index", new ReqLogin { Message = "Invalid username or password.", IsInvalid = true });
            }
        }

        [HttpPost]
        public async Task<JsonResult> LoginRegister(ReqRegisterUser request)
        {
            User user;
            user = await _userRepository.GetUser(request.Email);
            if (user == null)
            {
                user = new User
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    VerificationCode = ObjectId.GenerateNewId().ToString(),
                    IsOnline = true,
                    Password = CustomCrypto.HashSha256("MyPassword1239!3")
                };

                await _userRepository.CreateSync(user);
            }

            var isUserValid = Membership.ValidateUser(user.Email, "MyPassword1239!3");
            if (isUserValid)
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);
                return Json(new JsonGenericResult
                {
                    IsSuccess = true
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new JsonGenericResult
            {
                IsSuccess = true,
                Message = user.Id.ToString()
            });

        }


        [HttpPost]
        public async Task<JsonResult> RegisterUser(ReqRegisterUser request)
        {
            var findExistingEmail = await _userRepository.GetUser(request.Email);
            if (findExistingEmail == null)
            {
                var user = new User
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    VerificationCode = ObjectId.GenerateNewId().ToString(),
                    IsOnline = true,
                    Password = CustomCrypto.HashSha256(request.Password)
                };

                await _userRepository.CreateSync(user);

                return Json(new JsonGenericResult
                {
                    IsSuccess = true,
                    Message = user.Id.ToString()
                });
            }
            return Json(new JsonGenericResult
            {
                IsSuccess = false,
                Message = "Email with the same user already exists."
            });

        }
    }
}