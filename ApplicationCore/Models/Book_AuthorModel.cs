using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class Book_AuthorModel
    {
        //many to many example, many books many authors. This model is our join model

        public int Id { get; set; }

        //navigation props for book and author
        public int BookId { get; set; }
        public BookModel Book { get; set; }
        public int AuthorId { get; set; }
        public AuthorModel Author { get; set; }

    }
}

