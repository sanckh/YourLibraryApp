﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repository
{
    public interface IBook_AuthorRepository : IRepository<Book_Author>
    {
        Task AddBookAuthorAsync(int authorId, int bookId);
    }
}
