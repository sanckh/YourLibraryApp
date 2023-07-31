using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repository
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<Publisher> GetPublisherAsync(int publisherId);

    }
}
