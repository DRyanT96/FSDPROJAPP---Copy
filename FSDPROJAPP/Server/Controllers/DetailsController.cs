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
    public class DetailsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public DetailsController(ApplicationDbContext context)
        public DetailsController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Details
        [HttpGet]

        //Refactored
        //public async Task<ActionResult<IEnumerable<Detail>>> GetDetails()
        public async Task<IActionResult> GetDetails()
        {
            //Refactored
            //if (_context.Details == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Details.ToListAsync();
            var details = await _unitOfWork.Details.GetAll();
            return Ok(details);
        }

        // GET: api/Details/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Detail>> GetDetail(int id)
        public async Task<IActionResult> GetDetail(int id)
        {
            //Refactored
            //if (_context.Details == null)
            //{
            //    return NotFound();
            //}
            //var detail = await _context.Details.FindAsync(id);
            var detail = await _unitOfWork.Details.Get(q => q.Id == id);

            if (detail == null)
            {
                return NotFound();
            }

            //Refactored
            return Ok(detail);
        }

        // PUT: api/Details/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetail(int id, Detail detail)
        {
            if (id != detail.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(detail).State = EntityState.Modified;
            _unitOfWork.Details.Update(detail);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!DetailExists(id))
                if (!await DetailExists(id))
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

        // POST: api/Details
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detail>> PostDetail(Detail detail)
        {
            //Refactored
            //if (_context.Details == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Details'  is null.");
            //}
            //  _context.Details.Add(detail);
            //  await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetDetail", new { id = detail.Id }, detail);
            await _unitOfWork.Details.Insert(detail);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetDetail", new { id = detail.Id }, detail);
        }

        // DELETE: api/Details/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetail(int id)
        {
            //Refactored
            //if (_context.Details == null)
            //{
            //    return NotFound();
            //}
            //var detail = await _context.Details.FindAsync(id);
            //if (detail == null)
            //{
            //    return NotFound();
            //}
            var detail = await _unitOfWork.Details.Get(q => q.Id == id);
            if (detail == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Details.Remove(detail);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Details.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }
        //Refactored
        //private bool DetailExists(int id)
        private async Task<bool> DetailExists(int id)
        {
            //Refactored
            //return (_context.Details?.Any(e => e.Id == id)).GetValueOrDefault();
            var detail = await _unitOfWork.Details.Get(q => q.Id == id);
            return detail != null;
        }
    }
}
