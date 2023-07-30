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
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
