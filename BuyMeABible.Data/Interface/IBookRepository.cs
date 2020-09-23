using BuyMeABible.Data.Model;
using BuyMeABible.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMeABible.Data.Interface
{
    public interface IBookRepository : IEntityService<Book>
    {
        Task<List<Book>> GetActive();
        Task<List<Book>> GetByUserId(string userId);
    }
}
