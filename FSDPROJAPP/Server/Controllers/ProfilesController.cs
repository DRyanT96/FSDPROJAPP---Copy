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
    public class ProfilesController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public ProfilesController(ApplicationDbContext context)
        public ProfilesController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Profiles
        [HttpGet]

        //Refactored
        //public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        public async Task<IActionResult> GetProfiles()
        {
            //Refactored
            //if (_context.Profiles == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Profiles.ToListAsync();
            var profiles = await _unitOfWork.Profiles.GetAll(includes: q => q.Include(x => x.Hobby).Include(x => x.Detail).Include(x => x.Dislike));
            return Ok(profiles);
        }

        // GET: api/Profiles/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Profile>> GetProfile(int id)
        public async Task<IActionResult> GetProfile(int id)
        {
            //Refactored
            //if (_context.Profiles == null)
            //{
            //    return NotFound();
            //}
            //var profile = await _context.Profiles.FindAsync(id);
            var profile = await _unitOfWork.Profiles.Get(q => q.Id == id);

            if (profile == null)
            {
                return NotFound();
            }

            //Refactored
            return Ok(profile);
        }

        // PUT: api/Profiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(int id, Profile profile)
        {
            if (id != profile.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(profile).State = EntityState.Modified;
            _unitOfWork.Profiles.Update(profile);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!ProfileExists(id))
                if (!await ProfileExists(id))
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

        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profile>> PostProfile(Profile profile)
        {
            //Refactored
            //if (_context.Profiles == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Profiles'  is null.");
            //}
            //  _context.Profiles.Add(profile);
            //  await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
            await _unitOfWork.Profiles.Insert(profile);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
        }

        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            //Refactored
            //if (_context.Profiles == null)
            //{
            //    return NotFound();
            //}
            //var profile = await _context.Profiles.FindAsync(id);
            //if (profile == null)
            //{
            //    return NotFound();
            //}
            var profile = await _unitOfWork.Profiles.Get(q => q.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Profiles.Remove(profile);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Profiles.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }
        //Refactored
        //private bool ProfileExists(int id)
        private async Task<bool> ProfileExists(int id)
        {
            //Refactored
            //return (_context.Profiles?.Any(e => e.Id == id)).GetValueOrDefault();
            var profile = await _unitOfWork.Profiles.Get(q => q.Id == id);
            return profile != null;
        }
    }
}
