using ApplicationCore.Entities;
using ApplicationCore.Contracts.Repository;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly AppDbContext _dbContext;
        public BookRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserBook> GetUserBookAsync(int userId, int bookId)
        {
            return await _dbContext.UserBooks
                .Include(x => x.Book)
                .Include(x => x.User)  // Include this if necessary
                .FirstOrDefaultAsync(x => x.Book.Id == bookId && x.UserId == userId);
        }

        public async Task DeleteUserBookAsync(UserBook userBook)
        {
            _dbContext.UserBooks.Remove(userBook);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserBook>> GetAllBooksByUserIdAsync(int userId)
        {
            return await _dbContext.UserBooks
                .Include(x => x.User)
                .Include(x => x.Book)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

    }
}
