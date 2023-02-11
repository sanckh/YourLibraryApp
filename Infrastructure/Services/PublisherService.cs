using ApplicationCore.Contracts.Repository;
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
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;
        public PublisherService(IPublisherRepository _pub)
        {
            _publisherRepository = _pub;
        }
        public Task<PublisherModel> GetAllPublishersAsync(PublisherModel model)
        {
            throw new NotImplementedException();
        }
    }
}
