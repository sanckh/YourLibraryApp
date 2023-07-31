using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repository
{
    public interface IBookRepository:IRepository<Book>
    {
        //Task<Book> GetAllBooks(int id);
        Task<UserBook> GetUserBookAsync(int userId, int bookId);
        Task DeleteUserBookAsync(UserBook userBook);
        Task<IEnumerable<UserBook>> GetAllBooksByUserIdAsync(int userId);
        Task<Book> AddBookAsync(BookModel newBook);
        Task<Book> GetBookAsync(int bookId);
        Task<UserBook> AddBookToUserLibraryAsync(int userId, int bookId, string title);


    }
}
