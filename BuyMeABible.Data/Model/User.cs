using BuyMeABible.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMeABible.Data.Model
{
    public class User : MongoEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsOnline { get; set; }
        public string ProfilePhoto { get; set; }
        public string ProfilePhotoFilename { get; set; }
        public string Status { get; set; }
        public string VerificationCode { get; set; }
        public bool IsAdmin { get; set; }
    }
}
