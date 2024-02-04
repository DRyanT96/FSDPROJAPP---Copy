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
    public class SubscriptionsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public SubscriptionsController(ApplicationDbContext context)
        public SubscriptionsController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Subscriptions
        [HttpGet]

        //Refactored
        //public async Task<ActionResult<IEnumerable<Subscription>>> GetSubscriptions()
        public async Task<IActionResult> GetSubscriptions()
        {
            //Refactored
            //if (_context.Subscriptions == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Subscriptions.ToListAsync();
            var Subscriptions = await _unitOfWork.Subscriptions.GetAll(includes: q => q.Include(x => x.Profile));
            return Ok(Subscriptions);
        }

        // GET: api/Subscriptions/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Subscription>> GetSubscription(int id)
        public async Task<IActionResult> GetSubscription(int id)
        {
            //Refactored
            //if (_context.Subscriptions == null)
            //{
            //    return NotFound();
            //}
            //var Subscription = await _context.Subscriptions.FindAsync(id);
            var Subscription = await _unitOfWork.Subscriptions.Get(q => q.Id == id);

            if (Subscription == null)
            {
                return NotFound();
            }

            //Refactored
            return Ok(Subscription);
        }

        // PUT: api/Subscriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscription(int id, Subscription Subscription)
        {
            if (id != Subscription.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(Subscription).State = EntityState.Modified;
            _unitOfWork.Subscriptions.Update(Subscription);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!SubscriptionExists(id))
                if (!await SubscriptionExists(id))
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

        // POST: api/Subscriptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subscription>> PostSubscription(Subscription Subscription)
        {
            //Refactored
            //if (_context.Subscriptions == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Subscriptions'  is null.");
            //}
            //  _context.Subscriptions.Add(Subscription);
            //  await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetSubscription", new { id = Subscription.Id }, Subscription);
            await _unitOfWork.Subscriptions.Insert(Subscription);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetSubscription", new { id = Subscription.Id }, Subscription);
        }

        // DELETE: api/Subscriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            //Refactored
            //if (_context.Subscriptions == null)
            //{
            //    return NotFound();
            //}
            //var Subscription = await _context.Subscriptions.FindAsync(id);
            //if (Subscription == null)
            //{
            //    return NotFound();
            //}
            var Subscription = await _unitOfWork.Subscriptions.Get(q => q.Id == id);
            if (Subscription == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Subscriptions.Remove(Subscription);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Subscriptions.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }
        //Refactored
        //private bool SubscriptionExists(int id)
        private async Task<bool> SubscriptionExists(int id)
        {
            //Refactored
            //return (_context.Subscriptions?.Any(e => e.Id == id)).GetValueOrDefault();
            var Subscription = await _unitOfWork.Subscriptions.Get(q => q.Id == id);
            return Subscription != null;
        }
    }
}