using Microsoft.AspNetCore.Http;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor contextAccessor;
        public UserService(IUserRepository _user, IHttpContextAccessor contextAccessor)
        {
            userRepository = _user;
            this.contextAccessor = contextAccessor;
        }

        public async Task<CurrentUserModel> GetCurrentUser(){

            var email = contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            if (email != null)
            {
                var currentUser = await userRepository.GetUserByEmail(email);
                if (currentUser != null)
                {
                    var userResponse = new CurrentUserModel
                    {
                        Id = currentUser.Id,
                        FirstName = currentUser.FirstName,
                        LastName = currentUser.LastName,
                        Email = currentUser.Email,
                        DateOfBirth = currentUser.DateOfBirth.GetValueOrDefault()
                        //Add more from the entity here
                    };
                    return userResponse;
                }
            }

            return null;
        }
    }
}
