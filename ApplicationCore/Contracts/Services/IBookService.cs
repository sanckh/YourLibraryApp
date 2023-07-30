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
        Task<PagedResult<UserBookModel>> GetAllBooksAsync(int pageNumber, int pageSize, int userId, string sortColumn, SortDirection sortDirection);
        Task<int> DeleteBookAsync(int userId, int bookId);
        Task<UserBookModel> UpdateBookAsync(int bookId, int userId, UserBookModel updatedUserBook);
        Task<List<UserBookModel>> GetRecentlyAddedBooksAsync(int days, int userId);
        Task<PagedResult<UserBookModel>> SearchUserBooksAsync(int pageNumber, int pageSize, int userId, string searchQuery, string sortColumn, SortDirection sortDirection);
        Task<UserBookModel> GetBookByIdAsync(int bookId, int userId);
        Task<List<UserBookModel>> GetUnreadBooksAsync(int userId);


    }
}
