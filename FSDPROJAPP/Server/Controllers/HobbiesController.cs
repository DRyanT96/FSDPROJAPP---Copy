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
    public class HobbysController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public HobbysController(ApplicationDbContext context)
        public HobbysController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Hobbys
        [HttpGet]

        //Refactored
        //public async Task<ActionResult<IEnumerable<Hobby>>> GetHobbys()
        public async Task<IActionResult> GetHobbys()
        {
            //Refactored
            //if (_context.Hobbys == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Hobbys.ToListAsync();
            var hobbys = await _unitOfWork.Hobbys.GetAll();
            return Ok(hobbys);
        }

        // GET: api/Hobbys/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Hobby>> GetHobby(int id)
        public async Task<IActionResult> GetHobby(int id)
        {
            //Refactored
            //if (_context.Hobbys == null)
            //{
            //    return NotFound();
            //}
            //var hobby = await _context.Hobbys.FindAsync(id);
            var hobby = await _unitOfWork.Hobbys.Get(q => q.Id == id);

            if (hobby == null)
            {
                return NotFound();
            }

            //Refactored
            return Ok(hobby);
        }

        // PUT: api/Hobbys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHobby(int id, Hobby hobby)
        {
            if (id != hobby.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(hobby).State = EntityState.Modified;
            _unitOfWork.Hobbys.Update(hobby);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!HobbyExists(id))
                if (!await HobbyExists(id))
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

        // POST: api/Hobbys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hobby>> PostHobby(Hobby hobby)
        {
            //Refactored
            //if (_context.Hobbys == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Hobbys'  is null.");
            //}
            //  _context.Hobbys.Add(hobby);
            //  await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetHobby", new { id = hobby.Id }, hobby);
            await _unitOfWork.Hobbys.Insert(hobby);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetHobby", new { id = hobby.Id }, hobby);
        }

        // DELETE: api/Hobbys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHobby(int id)
        {
            //Refactored
            //if (_context.Hobbys == null)
            //{
            //    return NotFound();
            //}
            //var hobby = await _context.Hobbys.FindAsync(id);
            //if (hobby == null)
            //{
            //    return NotFound();
            //}
            var hobby = await _unitOfWork.Hobbys.Get(q => q.Id == id);
            if (hobby == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Hobbys.Remove(hobby);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Hobbys.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }
        //Refactored
        //private bool HobbyExists(int id)
        private async Task<bool> HobbyExists(int id)
        {
            //Refactored
            //return (_context.Hobbys?.Any(e => e.Id == id)).GetValueOrDefault();
            var hobby = await _unitOfWork.Hobbys.Get(q => q.Id == id);
            return hobby != null;
        }
    }
}
