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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository authorRepository;
        public AuthorService(IAuthorRepository _auth)
        {
            authorRepository = _auth;
        }

        public async Task<int> InsertAuthorAsync(AuthorModel author)
        {
            Author _author = new Author();

            _author.FullName = author.FullName;
            return await authorRepository.InsertAsync(_author);
        }

        public async Task<AuthorWithBooksModel> GetAuthorWithBooksAsync(int id)
        {
            var author = await authorRepository.GetByIdAsync(id);
            if(author == null)
            {
                return null;
            }
            var authorWithBooks = new AuthorWithBooksModel
            {
                FullName = author.FullName,
                BookTitles = author.Book_Authors.Select(n => n.Book.Title).ToList()
            };

            return authorWithBooks;
        } 



        //public async Task<int> UpdateAuthorAsync(AuthorModel author)
        //{
        //    var existingAuthor = await authorRepository.GetByIdAsync(author.Id);
        //    if (existingAuthor == null)
        //    {
        //        return 0;
        //    }

        //    existingAuthor.FullName = author.FullName;

        //    await authorRepository.UpdateAsync(existingAuthor);

        //    return await authorRepository.SaveChangesAsync();
        //}

        public async Task<int> DeleteAuthorAsync(int id)
        {
            var author = await authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return 0;
            }

            await authorRepository.DeleteAsync(id);

            return await authorRepository.SaveChangesAsync();
        }
    }
}
