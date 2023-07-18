﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Book
    {
        public int Id { get; set; }
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
        public Publisher Publisher { get; set; }
        public List<UserBook> UserBooks { get; set; }

        //book author nav prop
        public List<Book_Author> Book_Authors { get; set; }
    }
}
