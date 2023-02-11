using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        public BookService(IBookRepository _boo)
        {
            bookRepository = _boo;
        }
        public Task<int> DeleteBookAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookModel>> GetAllBooksAsync()
        {
            var books = await bookRepository.GetAllAsync();

            return books.Select(b => new BookModel
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Genre = b.Genre,
                isRead = b.isRead,
                DateRead = b.DateRead,
                Rating = b.Rating,
                CoverUrl = b.CoverUrl,
                DateAdded = b.DateAdded,
            });
        }

        public async Task<int> InsertBookAsync(BookModel model)
        {
            var book = new Book
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Genre = model.Genre,
                isRead = model.isRead,
                DateRead = model.DateRead,
                Rating = model.Rating,
                CoverUrl = model.CoverUrl,
                DateAdded = model.DateAdded,
            };
            await bookRepository.AddAsync(book);
            return await bookRepository.SaveChangesAsync();
        }

        public async Task<int> UpdateBookAsync(BookModel model)
        {
            var book = await bookRepository.GetByIdAsync(model.Id);
            book.Title = model.Title;
            book.Description = model.Description;
            book.Genre = model.Genre;
            book.isRead = model.isRead;
            book.DateRead = model.DateRead;
            book.Rating = model.Rating;
            book.CoverUrl = model.CoverUrl;

            book.Update(book);
            return await bookRepository.SaveChangesAsync();
        }
    }
}
