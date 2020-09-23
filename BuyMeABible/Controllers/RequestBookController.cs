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
    public class RequestBookController : Controller
    {

        private IBookRepository _bookRepository;
        private IUserRepository _userRepository;

        public RequestBookController(IBookRepository _bookRepository, IUserRepository _userRepository)
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
                ListingType = "Request"
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