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
        public string Name { get; set; }

    }
    public class PublisherWithBooksAndAuthorsModel
    {
        public string Name { get; set; }
        public List<BookAuthorModel> BookAuthors { get; set; }
    }

    public class BookAuthorModel
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }

    }
}
