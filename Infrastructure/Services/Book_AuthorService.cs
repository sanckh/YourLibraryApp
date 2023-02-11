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
    public class Book_AuthorService : IBook_AuthorService
    {
        private readonly IBook_AuthorRepository _book_AuthorRepository;

        public Book_AuthorService(IBook_AuthorRepository book_AuthorRepository)
        {
            _book_AuthorRepository = book_AuthorRepository;
        }

        //this needs to be updated, its trash. But the compiler wasn't happy until I implemented this service.
        public async Task<IEnumerable<Book_AuthorModel>> GetAllBooksWithAuthorsAsync()
        {
            var book_Authors = await _book_AuthorRepository.GetAllAsync();

            return book_Authors.Select(b => new Book_AuthorModel
            {
                Id = b.Id

            });
        }
    }
}
