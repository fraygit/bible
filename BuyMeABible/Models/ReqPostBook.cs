using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyMeABible.Models
{
    public class ReqPostBook
    {
        public string Title { get; set; }
        public DateTime DateExpires { get; set; }
    }
}