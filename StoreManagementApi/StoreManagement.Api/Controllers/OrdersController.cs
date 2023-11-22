using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Data;
using StoreManagement.Core.Models;
using StoreManagement.Api.Resources;
using StoreManagement.Core.Services;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(ApiDbContext context, IMapper mapper, IOrderService orderService)
        {
            _context = context;
            _mapper = mapper;
            _orderService = orderService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders([FromQuery] string? UserId)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var orders = await  _orderService.GetOrders();
            if (UserId != null) {
                orders = await _orderService.GetOrdersByUserId(UserId);
            }
            var result = _mapper.Map<List<Order>, List<OrderResource>>(orders);
            return Ok(result);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
             }
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<Order, OrderResource>(order);
            return Ok(result);
        }


        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, [FromBody] OrderStatus status)
        {
            try
            {
                await _orderService.UpdateOrder(id, status);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'ApiDbContext.Orders'  is null.");
          }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(Guid id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
