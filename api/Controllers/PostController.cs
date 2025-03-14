using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Post;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    // Route attribute sets the base route for this controller to "api/post".
    // For example, if there's an action method [HttpGet] inside this controller, 
    // it will be accessible at "GET /api/post".
    [Route("api/post")] 
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public PostController(ApplicationDBContext context)
        {
            //We are applying the Databasecontext to this class which we created the database tables from
            //ApplicationDBContext is there so we can get data out of the database
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // ToList() startar själva exekveringen av frågan till databasen (SQL-fråga) som EF formulerar åt oss i bakgrunden när vi använder _context.Posts.
            // Detta kallas för deferred execution (fördröjd exekvering), vilket innebär att vi kan formulera frågan först i en query. Vi kan lägga till filtrering,
            // sortering och andra operationer innan vi faktiskt exekverar frågan. På så sätt gör vi bara en fråga till databasen istället för flera, vilket förbättrar prestanda.
            var posts = await _context.Posts.ToListAsync();
            var postDto = posts.Select(s => s.ToPostDto());
            //Returnera resultatet från databasen

            //Ok is just an IActionResult. We could as well declare it as a new IActionResult like so: IActionResult result = Ok(posts); and return result;
            return Ok(posts);
        }


        // Normally, we would manually set a value to a variable, like: int id = 5;
        // // However, in this case, the 'id' is automatically retrieved from the URL, 
        // // specifically from the part of the URL defined as {id}, and it's passed to the method 
        // // via the [FromRoute] attribute. In this case api/post/{id} (see Route at the top).
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            //Find is a form of search that is going to find by the id. You could use First or Default, but Find is best because we are searching for a certain id (and more obvious).
            var post = await _context.Posts.FindAsync(id);

            if(post == null)
            {
                //NotFound is a form of IActionResult which saves us having to type out a 404-response.
                return NotFound();
            }
            return Ok(post.ToPostDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequestDto postDto)
        {
            var postModel = postDto.ToPostFromCreateDto();
            await _context.AddAsync(postModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = postModel.Id }, postModel.ToPostDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePostRequestDto updateDto)
        {
            var postModel = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (postModel == null)
            {
                return NotFound();
            }

            postModel.Title = updateDto.Title;
            postModel.Body = updateDto.Body;
            postModel.CreatedOn = updateDto.CreatedOn;

            await _context.SaveChangesAsync();

            return Ok(postModel.ToPostDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var postModel = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (postModel == null)
            {
                return NotFound();
            }

            _context.Remove(postModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}