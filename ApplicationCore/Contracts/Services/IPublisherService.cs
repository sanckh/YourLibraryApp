using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface IPublisherService
    {
        public void AddPublisher(PublisherModel publisher);
       
    }
}
