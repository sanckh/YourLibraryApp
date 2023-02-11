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
        private readonly IBookRepository _bookRepository;
        private readonly IBook_AuthorRepository _book_AuthorRepository;
        private readonly IAuthorRepository _authorRepository;
        public BookService(IBookRepository bookRepository, IBook_AuthorRepository book_AuthorRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _book_AuthorRepository = book_AuthorRepository;
            _authorRepository = authorRepository;
        }
        public async Task<int> DeleteBookAsync(int id)
        {
            var entity = await _bookRepository.DeleteAsync(id);
            return entity.Id;
        }

        public async Task<IEnumerable<BookModel>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();

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
            await _bookRepository.AddAsync(book);
            return await _bookRepository.SaveChangesAsync();
        }

        public async Task<int> UpdateBookAsync(BookModel model)
        {
            var book = await _bookRepository.GetByIdAsync(model.Id);

            if (book == null)
                throw new ArgumentException("Book not found");

            book.Title = model.Title;
            book.Description = model.Description;
            book.Genre = model.Genre;
            book.isRead = model.isRead;
            book.DateRead = model.DateRead;
            book.Rating = model.Rating;
            book.CoverUrl = model.CoverUrl;

            await _bookRepository.UpdateAsync(book);
            return await _bookRepository.SaveChangesAsync();
        }

        public async Task<BookModel> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if(book == null)
            {
                throw new ArgumentException("Book not found");
            }
            var bookModel = new BookModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Genre = book.Genre,
                isRead = book.isRead,
                DateRead = book.DateRead,
                Rating = book.Rating,
                CoverUrl = book.CoverUrl,
                DateAdded = book.DateAdded,
                PublisherId = book.PublisherId,
            };

            var book_Authors = await _book_AuthorRepository.GetAllAsync();
            var authorModels = new List<AuthorModel>();
            foreach(var book_Author in book_Authors.Where(x => x.BookId == id))
            {
                var author = await _authorRepository.GetByIdAsync(book_Author.AuthorId);
                var authorModel = new AuthorModel
                {
                    Id = author.Id,
                    FullName = author.FullName
                };
                authorModels.Add(authorModel);
            }
            bookModel.Authors = authorModels;
            return bookModel;
        }
    }
}
