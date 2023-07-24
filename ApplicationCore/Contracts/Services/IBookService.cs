﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.Enums;

namespace ApplicationCore.Contracts.Services
{
    public interface IBookService
    {
        Task<PagedResult<BookModel>> GetAllBooksAsync(int pageNumber, int pageSize, int userId, string sortColumn, SortDirection sortDirection);
        Task<int> DeleteBookAsync(int userId, int bookId);
        Task<BookModel> UpdateBookAsync(int bookId, int userId, BookModel updatedBook);
        Task<BookWithAuthorsModel> GetBookByIdAsync(int bookId);

        //new task!
        //we want to be able to insert a new book with an author. If the other exists, great. If it doesnt, we want to add the author also.
        Task<int> InsertBookWithAuthorAsync(BookWithAuthorsModel book);

        Task<List<BookModel>> GetRecentlyAddedBooksAsync(int days, int userId);
    }
}
