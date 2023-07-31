using ApplicationCore.Entities;
using ApplicationCore.Contracts.Repository;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

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
                    .ThenInclude(b => b.BookAuthors)
                        .ThenInclude(ab => ab.Author)
                .Include(x => x.Book)
                    .ThenInclude(b => b.Publisher)
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

        public async Task<Book> AddBookAsync(BookModel newBook)
        {
            var book = new Book
            {
                Id = newBook.Id,
                Title = newBook.Title,
                Description = newBook.Description,
                Genre = newBook.Genre,
                isRead = false,
                DateRead = newBook.DateRead,
                Rating = newBook.Rating,
                CoverUrl = newBook.CoverUrl,
                DateAdded = newBook.DateAdded,
                PublisherId = newBook.PublisherId,
            };

            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();

            return book;
        }

        public async Task<UserBook> AddBookToUserLibraryAsync(int userId, int bookId, string title)
        {
            var userBook = new UserBook
            {
                UserId = userId,
                BookId = bookId,
                DateAdded  = DateTime.UtcNow,
                DateRead = null,
                Rating = 0,
                Title = title,
                isRead = false,
            };

            await _dbContext.UserBooks.AddAsync(userBook);
            await _dbContext.SaveChangesAsync();

            return userBook;
        }

        public async Task<Book> GetBookAsync(int bookId)
        {
            return await _dbContext.Books
                .Include(b => b.Publisher)  
                .Include(b => b.BookAuthors)
                    .ThenInclude(ab => ab.Author)
                .FirstOrDefaultAsync(b => b.Id == bookId);
        }

    }
}
