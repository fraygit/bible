using BuyMeABible.Data.Interface;
using BuyMeABible.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BuyMeABible.Controllers
{
    public class ProfileController : Controller
    {
        private IUserRepository _userRepository;
        private IBookRepository _bookRepository;

        public ProfileController(IUserRepository _userRepository, IBookRepository _bookRepository)
        {
            this._userRepository = _userRepository;
            this._bookRepository = _bookRepository;
        }

        [Authorize]
        public async Task<ActionResult> Index(string userId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/login");
            }
            var user = await _userRepository.Get(userId);
            return View(user);
        }

        [Authorize]
        public async Task<ActionResult> SendEmail(string userId)
        {
            var user = await _userRepository.Get(userId);
            return View(user);
        }

        [Authorize]
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Send(string to, string subject, string message)
        {
            var userTo = await _userRepository.Get(to);
            var userFrom = await _userRepository.GetUser(User.Identity.Name);

            EmailUtility.SendMsg("francis.yanga@gmail.com", userTo.Email, subject, message, userFrom.Email);

            return View();
        }

        [Authorize]
        public async Task<ActionResult> MyListing()
        {
            var user = await _userRepository.GetUser(User.Identity.Name);

            var listing = await _bookRepository.GetByUserId(user.Id.ToString());

            return View(listing);
        }
    }
}