using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

        public void InsertBookWithAuthorAsync(BookModel book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                Genre = book.Genre,
                isRead = book.isRead,
                DateRead = book.DateRead,
                Rating = book.Rating,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };
             _bookRepository.AddAsync(_book);
             _bookRepository.SaveChangesAsync();

            // Populate the Book property for each Book_AuthorModel
            foreach (var id in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _book_AuthorRepository.AddAsync(_book_author);
                _bookRepository.SaveChangesAsync();
            }
        }


        public async Task<Book> UpdateBookByIdAsync(int bookId, BookModel book)
        {
            var books = await _bookRepository.GetAllAsync();
            var _book = books.FirstOrDefault(n => n.Id == bookId);

            if (_book == null)
                throw new ArgumentException("Book not found");

            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.DateRead = book.isRead ? book.DateRead.Value : null;
                _book.Genre = book.Genre;
                _book.isRead = book.isRead;
                _book.Rating = book.isRead ? book.Rating.Value : null;
                _book.CoverUrl = book.CoverUrl;

               await _bookRepository.SaveChangesAsync();
            }
            return _book;
        }

        public async Task<BookWithAuthorsModel> GetBookByIdAsync(int bookId)
        {
            var books = await _bookRepository.GetAllAsync();
            if(books == null)
            {
                throw new ArgumentException("Book not found");
            }
            var _book = books.Where(n => n.Id == bookId)
                 .Select(book => new BookWithAuthorsModel()
                 {
                     Title = book.Title,
                     Description = book.Description,
                     Genre = book.Genre,
                     isRead = book.isRead,
                     DateRead = book.DateRead,
                     Rating = book.Rating,
                     CoverUrl = book.CoverUrl,
                     PublisherName = book.Publisher?.Name,
                     AuthorNames = book.Book_Authors?.Select(n => n.Author.FullName).ToList() ?? new List<string>()
                 }).FirstOrDefault();


            if (_book == null)
            {
                throw new ArgumentException("Book not found");
            }

            return _book;
        }
    }
}
