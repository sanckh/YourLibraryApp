using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repository
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> GetByNameAsync(string name);

        Task<Author> GetAuthorAsync(int id);



    }
}
