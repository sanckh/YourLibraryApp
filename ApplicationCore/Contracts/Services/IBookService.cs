using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookModel>> GetAllBooksAsync();
        Task<int> InsertBookAsync(BookModel model);
        Task<int> DeleteBookAsync(int id);
        Task<int> UpdateBookAsync(BookModel model);
        Task<BookModel> GetBookByIdAsync(int id);
    }
}
