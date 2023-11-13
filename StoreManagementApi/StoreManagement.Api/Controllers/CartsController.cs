using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Data;
using StoreManagement.Core.Models;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CartsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
          if (_context.Carts == null)
          {
              return NotFound();
          }
            return await _context.Carts.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(Guid id)
        {
          if (_context.Carts == null)
          {
              return NotFound();
          }
            // var cart = await _context.Carts.FindAsync(id);
            var cart = await _context.Carts.Include(e => e.CartItems).FirstOrDefaultAsync(e => e.Id == id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(Guid id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
          if (_context.Carts == null)
          {
              return Problem("Entity set 'ApiDbContext.Carts'  is null.");
          }
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(Guid id)
        {
            return (_context.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}