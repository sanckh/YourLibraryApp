using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Contracts.Repository;
using System.Data;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

namespace Infrastructure.Repository
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        AppDbContext _db;
        public AuthorRepository(AppDbContext _con) : base(_con)
        {
            _db = _con;
        }

        public async Task<Author> GetByNameAsync(string name)
        {
            return await _db.Authors
                .Where(a => a.FullName == name)
                .FirstOrDefaultAsync();
        }

        public async Task<Author> GetAuthorAsync(int id)
        {
            return await _db.Authors
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

    }
}
