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
    }
}
