using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Enums;
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
        private readonly IAuthorService _authorService;
        public BookService(IBookRepository bookRepository, IBook_AuthorRepository book_AuthorRepository, IAuthorRepository authorRepository, IAuthorService authorService)
        {
            _bookRepository = bookRepository;
            _book_AuthorRepository = book_AuthorRepository;
            _authorRepository = authorRepository;
            _authorService = authorService;
        }
        public async Task<int> DeleteBookAsync(int id)
        {
            var entity = await _bookRepository.DeleteAsync(id);
            return entity.Id;
        }

        public async Task<PagedResult<BookModel>> GetAllBooksAsync(int pageNumber, int pageSize, string sortColumn, SortDirection sortDirection)
        {
            var books = await _bookRepository.GetAllAsync();

            var totalCount = books.Count();

            //Apply sorting
            books = sortDirection == SortDirection.Ascending
                    ? books.OrderBy(b => b.GetType().GetProperty(sortColumn).GetValue(b))
                    : books.OrderByDescending(b => b.GetType().GetProperty(sortColumn).GetValue(b));

            // Apply pagination
            books = books.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var bookModels = books.Select(b => new BookModel
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

            return new PagedResult<BookModel>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                Results = bookModels.ToList(),
            };
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
            if (books == null)
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

        //now in the service level, let's create the logic behind that idea
        public async Task<int> InsertBookWithAuthorAsync(BookWithAuthorsModel book)
        {
            //lets get our authors
            var author = await _authorService.GetAuthorByNameAsync(book.AuthorNames.FirstOrDefault());

            //null check
            if (author == null)
            {
                // we have a null author when we insert a new book, so lets add the author!
                author = new Author { FullName = book.AuthorNames.FirstOrDefault() };
                await _authorService.InsertAuthorAsync(new AuthorModel { FullName = author.FullName });
                author = await _authorService.GetAuthorByNameAsync(book.AuthorNames.FirstOrDefault());
            }

            Book _book = new Book();
            _book.Title = book.Title;
            _book.Description = book.Description;
            _book.Genre = book.Genre;
            _book.isRead = book.isRead;
            _book.DateRead = book.DateRead;
            _book.Rating = book.Rating;
            _book.CoverUrl = book.CoverUrl;
            _book.DateAdded = book.DateAdded;

            await _bookRepository.InsertAsync(_book);

            //now lets add the id of the book and the id of the author to our db! Great example of many to many relationships.
            Book_Author bookAuthor = new Book_Author();
            bookAuthor.BookId = _book.Id;
            bookAuthor.AuthorId = author.Id;
            await _book_AuthorRepository.InsertAsync(bookAuthor);

            //dont forget to save!
            return await _bookRepository.SaveChangesAsync();
        }
    }
}
