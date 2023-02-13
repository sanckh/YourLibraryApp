using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookModel>> GetAllBooksAsync();
        void InsertBookWithAuthorAsync(BookModel model);
        Task<int> DeleteBookAsync(int id);
        Task<Book> UpdateBookByIdAsync(int bookId, BookModel model);
        Task<BookWithAuthorsModel> GetBookByIdAsync(int bookId);
    }
}
