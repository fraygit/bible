using BuyMeABible.Data.Interface;
using BuyMeABible.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BuyMeABible.Controllers
{
    public class BookController : Controller
    {
        private IUserRepository _userRepository;
        private IBookRepository _bookRepository;

        public BookController(IUserRepository _userRepository, IBookRepository _bookRepository)
        {
            this._userRepository = _userRepository;
            this._bookRepository = _bookRepository;
        }


        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Edit(string bookId)
        {
            var book = await _bookRepository.Get(bookId);
            if (book.Email.ToLower() != User.Identity.Name)
            {
                Response.Redirect("/");
            }
            return View(book);
        }

        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> EditBook(List<HttpPostedFileBase> file, string txtTitle, string ddTranslation, string txtDesc, string ddCountry, string ddType, string bookId)
        {

            var userDetails = await _userRepository.GetUser(User.Identity.Name);

            var newBook = await _bookRepository.Get(bookId);
            newBook.Title = txtTitle;
            newBook.Details = txtDesc;
            newBook.Translation = ddTranslation;
            newBook.Country = ddCountry;
            newBook.Type = ddType;

            if (file != null)
            {
                if (newBook.Images == null)
                {
                    newBook.Images = new List<string>();
                }
                foreach (var uploadedFile in file)
                {
                    if (uploadedFile != null)
                    {
                        var savePath = Path.Combine(Server.MapPath("~/BookPhotos"), uploadedFile.FileName);
                        newBook.Images.Add(savePath);
                        uploadedFile.SaveAs(savePath);
                    }
                }
            }

            await _bookRepository.Update(bookId, newBook);

            return View();
        }
    }
}