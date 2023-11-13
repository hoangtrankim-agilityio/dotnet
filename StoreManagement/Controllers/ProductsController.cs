using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Data;
using StoreManagement.Models;
using StoreManagement.Filters;
using StoreManagement.Wrappers;
using StoreManagement.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ProductsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct([FromQuery] PaginationFilter filter)
        {
            if (_context.Products == null)
            {
                  return NotFound();
            }
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Products
                .OrderBy(c => c.Name)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Products.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Product>(pagedData, validFilter, totalRecords);
            return Ok(pagedReponse);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
          if (_context.Products == null)
          {
              return Problem("Entity set 'ApiDbContext.Product'  is null.");
          }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
