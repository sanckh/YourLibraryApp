using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class BookModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public bool isRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rating { get; set; }
        public string CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        //navigation props
        public int? PublisherId { get; set; }
        public List<int> AuthorIds { get; set; }

    }

    public class BookWithAuthorsModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public bool isRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rating { get; set; }
        public string CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        //represent new db schema
        public string PublisherName { get; set; }
        public List<string> AuthorNames { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}
