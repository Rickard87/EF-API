using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Post;
using api.Models;

namespace api.Mappers
{
    public static class PostMappers
    {
        public static PostDto ToPostDto(this Post postModel)
        {
            return new PostDto
            {
                Id = postModel.Id,
                Title = postModel.Title,
                Body = postModel.Body,
                CreatedOn = postModel.CreatedOn
            };
        }
        public static Post ToPostFromCreateDto(this CreatePostRequestDto postDto)
        {
            return new Post
            {
                Title = postDto.Title,
                Body = postDto.Body,
                CreatedOn = postDto.CreatedOn
            };
        }
    }
}