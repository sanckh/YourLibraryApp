using ApplicationCore.Entities;
using ApplicationCore.Contracts.Repository;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Book_AuthorRepository : BaseRepository<Book_Author>, IBook_AuthorRepository
    {
        public Book_AuthorRepository(AppDbContext _con) : base(_con)
        {

        }
    }
}
