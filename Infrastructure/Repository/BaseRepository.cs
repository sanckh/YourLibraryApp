﻿using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Contracts.Repository;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _db;
        public BaseRepository(AppDbContext _con)
        {
            _db = _con;
        }
        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _db.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _db.Set<T>().Remove(entity);
                await _db.SaveChangesAsync();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> filter)
        {
            return await _db.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);

        }

        public async Task<int> InsertAsync(T entity)
        {
            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();
            return 1;
        }


        public async Task<int> UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return 1;
        }
    }
}
