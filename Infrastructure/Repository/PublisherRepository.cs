using ApplicationCore.Entities;
using ApplicationCore.Contracts.Repository;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        private readonly AppDbContext _dbContext;
        public PublisherRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Publisher> GetPublisherAsync(int publisherId)
        {
            return await _dbContext.Publishers.FindAsync(publisherId);
        }


    }
}
