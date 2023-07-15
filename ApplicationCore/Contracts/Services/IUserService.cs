using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.Enums;

namespace ApplicationCore.Contracts.Services
{
    public interface IUserService
    {
        Task<CurrentUserModel> GetCurrentUser();
    }
}
