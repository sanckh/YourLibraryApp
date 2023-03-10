using ApplicationCore.Entities;
using ApplicationCore.Contracts.Repository;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(AppDbContext _con) : base(_con)
        {

        }
    }
}
