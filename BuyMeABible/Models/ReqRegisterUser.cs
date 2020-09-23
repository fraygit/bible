using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyMeABible.Models
{
    public class ReqRegisterUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}