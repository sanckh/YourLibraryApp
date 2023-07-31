using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class UserBookModel
    {
        public int Id { get; set; }
        public int Title { get; set; }
        public bool isRead { get; set; }
        public int? Rating { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateRead { get; set; }

        //navigation props for book and user
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public BookModel Book { get; set; }
    }
}
