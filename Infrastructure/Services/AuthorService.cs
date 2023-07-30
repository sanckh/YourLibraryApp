﻿using ApplicationCore.Contracts.Repository;
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


    }
}
