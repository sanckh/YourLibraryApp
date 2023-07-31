using ApplicationCore.Entities;
using ApplicationCore.Contracts.Repository;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Book_AuthorRepository : BaseRepository<Book_Author>, IBook_AuthorRepository
    {
        private readonly AppDbContext _dbContext;

        public Book_AuthorRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddBookAuthorAsync(int authorId, int bookId)
        {
            var bookAuthor = new Book_Author
            {
                AuthorId = authorId,
                BookId = bookId
            };

            _dbContext.Book_Authors.Add(bookAuthor);
            await _dbContext.SaveChangesAsync();

        }
    }
}
