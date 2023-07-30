using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repository
{
    public interface IBookRepository:IRepository<Book>
    {
        //Task<Book> GetAllBooks(int id);
        Task<UserBook> GetUserBookAsync(int userId, int bookId);
        Task DeleteUserBookAsync(UserBook userBook);
        Task<IEnumerable<UserBook>> GetAllBooksByUserIdAsync(int userId);
    }
}
