using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyMeABible.Models
{
    public class ReqLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsInvalid { get; set; }
        public string Message { get; set; }
    }
}