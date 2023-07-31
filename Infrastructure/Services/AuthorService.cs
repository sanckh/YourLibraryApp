using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public async Task<Author> GetAuthorByNameAsync(string name)
        {
            var author = await authorRepository.GetAllAsync();
            return author.Where(x => x.FullName == name).FirstOrDefault();
        }

        public async Task<Author> AddAuthorAsync(AuthorModel newAuthor)
        {
            var author = new Author
            {
                Id = newAuthor.Id,
                FullName = newAuthor.FullName,
                // Fill in other fields as needed
            };

            await authorRepository.AddAsync(author);
            await authorRepository.SaveChangesAsync();

            return author;
        }



    }
}
