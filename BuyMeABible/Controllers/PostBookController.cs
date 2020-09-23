using BuyMeABible.Data.Interface;
using BuyMeABible.Data.Model;
using BuyMeABible.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BuyMeABible.Controllers
{
    public class PostBookController : Controller
    {
        private IBookRepository _bookRepository;
        private IUserRepository _userRepository;

        public PostBookController(IBookRepository _bookRepository, IUserRepository _userRepository)
        {
            this._bookRepository = _bookRepository;
            this._userRepository = _userRepository;
        }

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/login");
            }
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AddComment(ReqAddComment request)
        {
            var book = await _bookRepository.Get(request.BookId);
            var user = await _userRepository.GetUser(User.Identity.Name);
            if (book != null)
            {
                if (book.Comments == null)
                {
                    book.Comments = new List<Comment>();
                }
                book.Comments.Add(new Comment
                {
                    CommentDateTime = DateTime.UtcNow,
                    Details = request.Comment,
                    User = user.Email.ToLower(),
                    UserId = user.Id.ToString(),
                    UserDisplayName = user.FirstName + " " + user.LastName
                });
                book.RecentChange = DateTime.UtcNow;
                await _bookRepository.Update(book.Id.ToString(), book);
            }
            return Json(book);
        }

        [HttpPost]
        public async Task<JsonResult> Like(string bookId)
        {
            var book = await _bookRepository.Get(bookId);
            var user = await _userRepository.GetUser(User.Identity.Name.ToLower());
            if (book != null)
            {
                if (book.Likes == null)
                {
                    book.Likes = new List<Like>();
                }
                if (!book.Likes.Any(n => n.Email.ToLower() == User.Identity.Name.ToLower()))
                {
                    book.Likes.Add(new Data.Model.Like
                    {
                        DateTime = DateTime.UtcNow,
                        Email = User.Identity.Name.ToLower(),
                        UserId = user.Id.ToString(),
                        DisplayName = user.FirstName + " " + user.LastName
                    });
                }
                book.RecentChange = DateTime.UtcNow;
                await _bookRepository.Update(book.Id.ToString(), book);
            }
            return Json(book);
        }

        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Post(List<HttpPostedFileBase> file, string txtTitle, string ddExpires, string ddTranslation, string txtDesc, string ddCountry, string ddType)
        {

            var userDetails = await _userRepository.GetUser(User.Identity.Name);

            var newBook = new Book
            {
                UserId = userDetails.Id.ToString(),
                Email = User.Identity.Name.ToLower(),
                Title = txtTitle,
                Details = txtDesc,
                Country = ddCountry,
                Translation = ddTranslation,
                Expires = DateTime.UtcNow.AddDays(int.Parse(ddExpires)),
                Type = ddType,
                UserName = string.Format("{0} {1}", userDetails.FirstName, userDetails.LastName),
                RecentChange = DateTime.UtcNow,
                IsApproved = false,
                ListingType = "Give"
            };

            if (file != null)
            {
                newBook.Images = new List<string>();
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

            await _bookRepository.CreateSync(newBook);

            return View();
        }
    }
}