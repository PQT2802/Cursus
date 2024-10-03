using Cursus_Data.Context;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class OrderRepository : IOrderRepository
    {
        private readonly LMS_CursusDbContext _context;
        
        public OrderRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }


        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders
        .Include(o => o.OrderItems) // Include related OrderItems
        .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
        public async Task<dynamic> CreateOrderFromCart(string userId, List<Guid> selectedCartItemIds)
        {
            // Fetch the cart and cart items
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Course)
                .ThenInclude(cv => cv.CourseVersions)
                .ThenInclude(cvd => cvd.CourseVersionDetails)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                throw new Exception("Cart not found for the user.");

            var cartItems = cart.CartItems
                .Where(ci => selectedCartItemIds.Contains(ci.CartItemId) && !ci.IsDeleted)
                .ToList();

            if (!cartItems.Any())
                throw new Exception("No valid cart items found.");

            // Create the new order
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending.ToString() // Assuming OrderStatus is an enum
            };

            // Add order items
            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    CourseId = item.CourseId,
                    Amount = item.Course.CourseVersions.FirstOrDefault(cv => cv.IsUsed && cv.CourseId == item.CourseId).CourseVersionDetails.Price, // Assuming Course has a Price property
                    OrderDate = DateTime.UtcNow,
                    IsDeleted = false
                };

                order.OrderItems.Add(orderItem);
            }

            // Add the order to the context
            _context.Orders.Add(order);

            // Remove the cart items from the cart or mark them as deleted
            foreach (var item in cartItems)
            {
                item.IsDeleted = true;
                // Alternatively, you can remove them from the cart
                // _context.CartItems.Remove(item);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return new
            {
                OrderId = order.OrderId,
                TotalAmount = order.TotalAmount
            };
        }


        public async Task<dynamic> PayOrder(Guid orderId)
        {
            try
            {
                // Fetch the order asynchronously
                var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId.Equals(orderId));

                if (order == null)
                {
                    throw new Exception("Order not found");
                }

                // Update the order status
                order.Status = OrderStatus.Paid.ToString();
                 _context.Orders.Update(order);

                // Save changes asynchronously
                await _context.SaveChangesAsync();

                return order;
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "An error occurred while paying the order");

                // Throw a new exception with a user-friendly message
                throw new Exception("An error occurred while processing the payment. Please try again later.");
            }
        }

        public async Task<List<ViewOrder>> GetUserOrderListByUserId(string userId, OrderListConfig config)
        {
            try
            {
                var orderQuery = _context.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.UserId == userId)
                    .AsQueryable();

                if (config.Status.HasValue)
                {
                    var status = config.Status.Value.ToString();
                    orderQuery = orderQuery.Where(c => c.Status.Equals(status));
                }

                orderQuery = config.SortDirection == SortDirection.Ascending
                    ? orderQuery.OrderBy(o => o.OrderId)
                    : orderQuery.OrderByDescending(o => o.OrderId);

                orderQuery = orderQuery
                    .Skip(((config.PageIndex ?? 1) - 1) * (config.PageSize ?? 20))
                    .Take(config.PageSize ?? 20);

                var orders = await orderQuery.ToListAsync();

                var viewOrders = orders.Select(order => new ViewOrder
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    Status = order.Status,
                    TotalAmount = order.TotalAmount,
                    ViewOrderItems = order.OrderItems
                        .Where(item => !item.IsDeleted)
                        .Select(item => new ViewOrderItem
                        {
                            OrderItemId = item.OrderItemId,
                            CourseId = item.CourseId,
                            Amount = item.Amount,
                            OrderDate = item.OrderDate
                        })
                        .ToList()
                }).ToList();

                return viewOrders;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
