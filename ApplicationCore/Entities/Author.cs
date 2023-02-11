using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Author
    {
        //many to many example. Many books, many authors
        public int Id { get; set; }
        public string FullName { get; set; }


        //navigation props
        public List<Book_Author> Book_Authors { get; set; }
    }
}
