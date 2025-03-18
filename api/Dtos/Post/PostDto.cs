using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;

namespace api.Dtos.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        // This last line is not needed for our PostDto, we do not want it
        // public List<api.Models.Comment> Comments { get; set; } = new List<api.Models.Comment>();

        //This line is not implemented until step 12 Comment System
        public List<CommentDto> Comments { get; set; }
    }
}