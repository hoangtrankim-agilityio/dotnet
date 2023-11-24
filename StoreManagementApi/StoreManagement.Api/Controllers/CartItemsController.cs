using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Data;
using StoreManagement.Core.Models;
using StoreManagement.Core.Services;
using StoreManagement.Api.Resources;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ICartItemService _cartItemService;

        private readonly IMapper _mapper;

        public CartItemsController(ApiDbContext context, IMapper mapper, ICartItemService cartItemService)
        {
            _context = context;
            _mapper = mapper;
            _cartItemService = cartItemService;
        }

        // GET: api/CartItems
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems()
        // {
        //   if (_context.CartItems == null)
        //   {
        //       return NotFound();
        //   }
        //     return await _context.CartItems.ToListAsync();
        // }

        // GET: api/CartItems/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<CartItem>> GetCartItem(Guid id)
        // {
        //   if (_context.CartItems == null)
        //   {
        //       return NotFound();
        //   }
        //     var cartItem = await _context.CartItems.FindAsync(id);

        //     if (cartItem == null)
        //     {
        //         return NotFound();
        //     }

        //     return cartItem;
        // }

        // PUT: api/CartItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutCartItem(Guid id, CartItem cartItem)
        // {
        //     if (id != cartItem.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(cartItem).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!CartItemExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/CartItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CartItem>> PostCartItem(CartItem cartItem)
        {
            if (_context.CartItems == null)
            {
                return Problem("Entity set 'ApiDbContext.CartItems'  is null.");
            }
            var result = await _cartItemService.CreateCartItem(cartItem);
            var cartItemResource = _mapper.Map<CartItem, CartItemResource>(result);
            return Ok(cartItemResource);
        }

        // DELETE: api/CartItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(Guid id)
        {
            if (_context.CartItems == null)
            {
                return NotFound();
            }
            var cartItem = await _cartItemService.GetCartById(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            await _cartItemService.DeleteCartItem(cartItem);

            return NoContent();
        }

        private bool CartItemExists(Guid id)
        {
            return (_context.CartItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
