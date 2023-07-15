using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Contracts.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext _con) : base(_con)
        {
            _db = _con;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _db.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
        
        public async Task<User> GetUserByIdAsync(int id){
            return await _db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
