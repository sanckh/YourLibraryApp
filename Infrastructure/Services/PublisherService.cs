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
        public async Task<PublisherModel> GetAllPublishersAsync(PublisherModel model)
        {
            var publishers = await _publisherRepository.GetAllAsync();
            var publisherModels = new List<PublisherModel>();
            foreach (var publisher in publishers)
            {
                publisherModels.Add(new PublisherModel
                {
                    Id = publisher.Id,
                    Name = publisher.Name
                });
            }
            model.Publishers = publisherModels;
            return model;
        }
    }
}
