using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyMeABible.Models
{
    public class ReqAddComment
    {
        public string BookId { get; set; }
        public string Comment { get; set; }
    }
}