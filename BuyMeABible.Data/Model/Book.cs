using BuyMeABible.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMeABible.Data.Model
{
    public class Book : MongoEntity
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Details { get; set; }
        public string Url { get; set; }
        public string Translation { get; set; }
        public string Country { get; set; } 
        public DateTime Expires { get; set; }
        public List<string> Images { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string ListingType { get; set; }
        public int Status { get; set; }
        public bool IsApproved { get; set; }
        public List<Comment> Comments { get; set; }
        public DateTime RecentChange { get; set; }
        public List<Like> Likes { get; set; }
    }
}
