using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PublisherModel
    {
        //one to many example. One publisher, multiple books
        public int Id { get; set; }
        public string Name { get; set; }

        //navigation properties
        //defines the relationship between models
        public List<Book> Books { get; set; }


    }
}
