using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class AuthorModel
    {
        //many to many example. Many books, many authors
        public string FullName { get; set; }
    }
    public class AuthorWithBooksModel
    {
        public string FullName { get; set; }
        public List<string> BookTitles { get; set; }
    }
}
