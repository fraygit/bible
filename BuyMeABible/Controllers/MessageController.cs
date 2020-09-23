using BuyMeABible.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BuyMeABible.Controllers
{
    public class MessageController : Controller
    {
        private IUserRepository _userRepository;

        public MessageController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }

        public async Task<ActionResult> Index(string email)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/login");
            }
            var user = await _userRepository.GetUser(email);
            return View(user);
        }
    }
}