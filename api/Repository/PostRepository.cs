using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Post;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDBContext _context;
        public PostRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Post> CreateAsync(Post postModel)
        {
            await _context.Posts.AddAsync(postModel);
            await _context.SaveChangesAsync();
            return postModel;
        }

        public async Task<Post?> DeleteAsync(int id)
        {
            var postModel = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (postModel == null)
            {
                return null;
            }

            _context.Posts.Remove(postModel);
            await _context.SaveChangesAsync();
            return postModel;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _context.Posts.Include(c => c.Comments).ToListAsync();
            // code before step 12 Comment System below
            // return await _context.Posts.ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            //Find does not work with include
            return await _context.Posts.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
            // code before step 12 Comment System
            // return await _context.Posts.FindAsync(id);
        }

        public async Task<bool> PostExists(int id)
        {
            return await _context.Posts.AnyAsync(s => s.Id == id);
        }

        public async Task<Post?> UpdateAsync(int id, UpdatePostRequestDto postDto)
        {
            var existingPost = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if(existingPost == null)
            {
                return null;
            }
            existingPost.Title = postDto.Title;
            existingPost.Body = postDto.Body;
            existingPost.CreatedOn = postDto.CreatedOn;

            await _context.SaveChangesAsync();

            return existingPost;
        }
    }
}