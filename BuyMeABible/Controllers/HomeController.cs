using BuyMeABible.Data.Interface;
using BuyMeABible.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BuyMeABible.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository _bookRepository;
        private IUserRepository _userRepository;

        public HomeController(IBookRepository _bookRepository, IUserRepository _userRepository)
        {
            this._bookRepository = _bookRepository;
            this._userRepository = _userRepository;
        }

        public async Task<ActionResult> Index()
        {
            var listings = await _bookRepository.GetActive();
            return View(listings);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<PartialViewResult> Comments(string bookId)
        {
            var book = await _bookRepository.Get(bookId);

            return PartialView(book.Comments);
        }
    }
}