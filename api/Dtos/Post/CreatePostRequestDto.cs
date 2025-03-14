using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Post
{
    public class CreatePostRequestDto
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}