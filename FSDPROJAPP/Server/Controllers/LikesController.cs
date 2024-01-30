using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FSDPROJAPP.Server.Data;
using FSDPROJAPP.Shared.Domain;
using FSDPROJAPP.Server.IRepository;
using SQLitePCL;
using FSDPROJAPP.Server.IRepository;

namespace FSDPROJAPP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public LikesController(ApplicationDbContext context)
        public LikesController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Likes
        [HttpGet]

        //Refactored
        //public async Task<ActionResult<IEnumerable<Like>>> GetLikes()
        public async Task<IActionResult> GetLikes()
        {
            //Refactored
            //if (_context.Likes == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Likes.ToListAsync();
            var likes = await _unitOfWork.Likes.GetAll();
            return Ok(likes);
        }

        // GET: api/Likes/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Like>> GetLike(int id)
        public async Task<IActionResult> GetLike(int id)
        {
            //Refactored
            //if (_context.Likes == null)
            //{
            //    return NotFound();
            //}
            //var like = await _context.Likes.FindAsync(id);
            var like = await _unitOfWork.Likes.Get(q => q.Id == id);

            if (like == null)
            {
                return NotFound();
            }

            //Refactored
            return Ok(like);
        }

        // PUT: api/Likes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLike(int id, Like like)
        {
            if (id != like.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(like).State = EntityState.Modified;
            _unitOfWork.Likes.Update(like);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!LikeExists(id))
                if (!await LikeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Likes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Like>> PostLike(Like like)
        {
            //Refactored
            //if (_context.Likes == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Likes'  is null.");
            //}
            //  _context.Likes.Add(like);
            //  await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetLike", new { id = like.Id }, like);
            await _unitOfWork.Likes.Insert(like);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetLike", new { id = like.Id }, like);
        }

        // DELETE: api/Likes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLike(int id)
        {
            //Refactored
            //if (_context.Likes == null)
            //{
            //    return NotFound();
            //}
            //var like = await _context.Likes.FindAsync(id);
            //if (like == null)
            //{
            //    return NotFound();
            //}
            var like = await _unitOfWork.Likes.Get(q => q.Id == id);
            if (like == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Likes.Remove(like);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Likes.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }
        //Refactored
        //private bool LikeExists(int id)
        private async Task<bool> LikeExists(int id)
        {
            //Refactored
            //return (_context.Likes?.Any(e => e.Id == id)).GetValueOrDefault();
            var like = await _unitOfWork.Likes.Get(q => q.Id == id);
            return like != null;
        }
    }
}
