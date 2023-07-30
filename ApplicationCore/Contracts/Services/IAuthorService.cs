using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface IAuthorService
    {
        Task<int> InsertAuthorAsync(AuthorModel book);
        //Task<int> UpdateAuthorAsync(AuthorModel book);
        Task<int> DeleteAuthorAsync(int id);
        Task<Author> GetAuthorByNameAsync(string name);
    }
}
