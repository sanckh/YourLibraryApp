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

        public Task<IEnumerable<BookModel>> GetAllBooksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertBookAsync(BookModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateBookAsync(BookModel model)
        {
            throw new NotImplementedException();
        }
    }
}
