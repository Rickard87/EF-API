using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}