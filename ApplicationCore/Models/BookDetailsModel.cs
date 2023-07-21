using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class BookDetailsModel
    {
        public string Title { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<string> Categories { get; set; } //May change to a List<>
        public string ThumbnailUrl { get; set; }
        public string PreviewLink { get; set; }
        public string InfoLink { get; set; }
    }
}
