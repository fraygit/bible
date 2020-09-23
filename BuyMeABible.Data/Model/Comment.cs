using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMeABible.Data.Model
{
    public class Comment
    {
        public string UserId { get; set; }
        public string User { get; set; }
        public string UserDisplayName { get; set; }
        public string Details { get; set; }
        public DateTime CommentDateTime { get; set; }
        public int Like { get; set; }
    }
}
