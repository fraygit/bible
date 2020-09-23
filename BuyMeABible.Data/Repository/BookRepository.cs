using BuyMeABible.Data.Interface;
using BuyMeABible.Data.Model;
using BuyMeABible.Data.Service;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMeABible.Data.Repository
{
    public class BookRepository : EntityService<Book>, IBookRepository
    {
        public async Task<List<Book>> GetActive()
        {
            try
            {
                var builder = Builders<Book>.Filter;
                var filter = builder.Gt(n => n.Expires, DateTime.UtcNow);
                var books = await ConnectionHandler.MongoCollection.Find(filter).SortByDescending(n => n.RecentChange).ToListAsync();
                if (books.Any())
                    return books;
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Book>> GetByUserId(string userId)
        {
            try
            {
                var builder = Builders<Book>.Filter;
                var filter = builder.Eq(n => n.UserId, userId);
                var books = await ConnectionHandler.MongoCollection.Find(filter).SortByDescending(n => n.Expires).ToListAsync();
                if (books.Any())
                    return books;
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
