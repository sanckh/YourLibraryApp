using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface IAuthorService
    {
        Task<AuthorWithBooksModel> GetAuthorWithBooksAsync(int id);
        Task<int> InsertAuthorAsync(AuthorModel book);
        Task<int> UpdateAuthorAsync(AuthorModel book);
        Task<int> DeleteAuthorAsync(int id);
    }
}
