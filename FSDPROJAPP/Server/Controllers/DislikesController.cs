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
    public class DislikesController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public DislikesController(ApplicationDbContext context)
        public DislikesController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Dislikes
        [HttpGet]

        //Refactored
        //public async Task<ActionResult<IEnumerable<Dislike>>> GetDislikes()
        public async Task<IActionResult> GetDislikes()
        {
            //Refactored
            //if (_context.Dislikes == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Dislikes.ToListAsync();
            var dislikes = await _unitOfWork.Dislikes.GetAll();
            return Ok(dislikes);
        }

        // GET: api/Dislikes/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Dislike>> GetDislike(int id)
        public async Task<IActionResult> GetDislike(int id)
        {
            //Refactored
            //if (_context.Dislikes == null)
            //{
            //    return NotFound();
            //}
            //var dislike = await _context.Dislikes.FindAsync(id);
            var dislike = await _unitOfWork.Dislikes.Get(q => q.Id == id);

            if (dislike == null)
            {
                return NotFound();
            }

            //Refactored
            return Ok(dislike);
        }

        // PUT: api/Dislikes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDislike(int id, Dislike dislike)
        {
            if (id != dislike.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(dislike).State = EntityState.Modified;
            _unitOfWork.Dislikes.Update(dislike);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!DislikeExists(id))
                if (!await DislikeExists(id))
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

        // POST: api/Dislikes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dislike>> PostDislike(Dislike dislike)
        {
            //Refactored
            //if (_context.Dislikes == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Dislikes'  is null.");
            //}
            //  _context.Dislikes.Add(dislike);
            //  await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetDislike", new { id = dislike.Id }, dislike);
            await _unitOfWork.Dislikes.Insert(dislike);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetDislike", new { id = dislike.Id }, dislike);
        }

        // DELETE: api/Dislikes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDislike(int id)
        {
            //Refactored
            //if (_context.Dislikes == null)
            //{
            //    return NotFound();
            //}
            //var dislike = await _context.Dislikes.FindAsync(id);
            //if (dislike == null)
            //{
            //    return NotFound();
            //}
            var dislike = await _unitOfWork.Dislikes.Get(q => q.Id == id);
            if (dislike == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Dislikes.Remove(dislike);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Dislikes.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }
        //Refactored
        //private bool DislikeExists(int id)
        private async Task<bool> DislikeExists(int id)
        {
            //Refactored
            //return (_context.Dislikes?.Any(e => e.Id == id)).GetValueOrDefault();
            var dislike = await _unitOfWork.Dislikes.Get(q => q.Id == id);
            return dislike != null;
        }
    }
}
