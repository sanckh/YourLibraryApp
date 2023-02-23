using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.Enums;

namespace ApplicationCore.Contracts.Services
{
    public interface IBookService
    {
        Task<PagedResult<BookModel>> GetAllBooksAsync(int pageNumber, int pageSize, string sortColumn, SortDirection sortDirection);
        void InsertBookWithAuthorAsync(BookModel model);
        Task<int> DeleteBookAsync(int id);
        Task<Book> UpdateBookByIdAsync(int bookId, BookModel model);
        Task<BookWithAuthorsModel> GetBookByIdAsync(int bookId);
    }
}
