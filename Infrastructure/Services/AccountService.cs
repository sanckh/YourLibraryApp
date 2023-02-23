using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;
        public AccountService(IUserRepository _user)
        {
            userRepository = _user;
        }
        public async Task<int> Register(UserRegisterModel user)
        {
            var existingUser = await userRepository.GetUserByEmail(user.Email);
            if(existingUser != null)
            {
                throw new Exception("This email exists!");
            }

            var salt = GenerateSalt();
            var password = GetHashedPassword(user.Password, salt);

            User u = new User()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                HashedPassword = password,
                Salt = salt,
                DateOfBirth = DateTime.UtcNow,
            };

            await userRepository.InsertAsync(u);

            var newUser = await userRepository.GetUserByEmail(user.Email);
            if (newUser == null)
            {
                throw new Exception("User not found after registration.");
            }

            if (newUser.Salt != salt)
            {
                throw new Exception($"Salt value does not match. Expected: {salt}. Actual: {newUser.Salt}");
            }

            return newUser.Id;
        }
        public async Task<UserLoginModel> Validate(string email, string password)
        {
            var user = await userRepository.GetUserByEmail(email);
            if(user == null)
            {
                throw new Exception("Email does not exist!");
            }

            var hashPassword = GetHashedPassword(password, user.Salt);
            if(hashPassword == user.HashedPassword)
            {
                return new UserLoginModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = email,
                    DateOfBirth = user.DateOfBirth.GetValueOrDefault(),
                };
            }
            return null;
        }
        private string GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using(var randomSalt = RandomNumberGenerator.Create())
            {
                randomSalt.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
        private string GetHashedPassword(string password, string salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, Convert.FromBase64String(salt), KeyDerivationPrf.HMACSHA512, 100000, 128 / 8));
            return hashed;
        }
    }
}
