using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class UserBook
    {
        //many to many example, many books many authors. This model is our join model

        public int Id { get; set; }
        public string Title { get; set; }
        public bool isRead { get; set; }
        public int? Rating { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateRead { get; set; }


        //navigation props for book and user
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
