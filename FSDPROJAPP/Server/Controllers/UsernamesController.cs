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
    public class UsernamesController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public UsernamesController(ApplicationDbContext context)
        public UsernamesController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Usernames
        [HttpGet]

        //Refactored
        //public async Task<ActionResult<IEnumerable<Username>>> GetUsernames()
        public async Task<IActionResult> GetUsernames()
        {
            //Refactored
            //if (_context.Usernames == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Usernames.ToListAsync();
            var usernames = await _unitOfWork.Usernames.GetAll();
            return Ok(usernames);
        }

        // GET: api/Usernames/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Username>> GetUsername(int id)
        public async Task<IActionResult> GetUsername(int id)
        {
            //Refactored
            //if (_context.Usernames == null)
            //{
            //    return NotFound();
            //}
            //var username = await _context.Usernames.FindAsync(id);
            var username = await _unitOfWork.Usernames.Get(q => q.Id == id);

            if (username == null)
            {
                return NotFound();
            }

            //Refactored
            return Ok(username);
        }

        // PUT: api/Usernames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsername(int id, Username username)
        {
            if (id != username.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(username).State = EntityState.Modified;
            _unitOfWork.Usernames.Update(username);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!UsernameExists(id))
                if (!await UsernameExists(id))
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

        // POST: api/Usernames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Username>> PostUsername(Username username)
        {
            //Refactored
            //if (_context.Usernames == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Usernames'  is null.");
            //}
            //  _context.Usernames.Add(username);
            //  await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetUsername", new { id = username.Id }, username);
            await _unitOfWork.Usernames.Insert(username);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetUsername", new { id = username.Id }, username);
        }

        // DELETE: api/Usernames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsername(int id)
        {
            //Refactored
            //if (_context.Usernames == null)
            //{
            //    return NotFound();
            //}
            //var username = await _context.Usernames.FindAsync(id);
            //if (username == null)
            //{
            //    return NotFound();
            //}
            var username = await _unitOfWork.Usernames.Get(q => q.Id == id);
            if (username == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Usernames.Remove(username);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Usernames.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }
        //Refactored
        //private bool UsernameExists(int id)
        private async Task<bool> UsernameExists(int id)
        {
            //Refactored
            //return (_context.Usernames?.Any(e => e.Id == id)).GetValueOrDefault();
            var username = await _unitOfWork.Usernames.Get(q => q.Id == id);
            return username != null;
        }
    }
}
