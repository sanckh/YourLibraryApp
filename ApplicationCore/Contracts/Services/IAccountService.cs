using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IAccountService
    {
        Task<int> Register(UserRegisterModel user);
        Task<UserLoginModel> Validate(string email, string password);
    }
}
