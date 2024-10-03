using Cursus_Data.Context;
using Cursus_Data.Models.DTOs.Student;
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
    public class CartRepository : ICartRepository
    {

        private readonly LMS_CursusDbContext _context;
        public CartRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }

        public async Task<dynamic> AddCourseToCart(string userId, string courseId)
        {
                try
                {
                    var cart = await _context.Carts
                        .Include(c => c.CartItems)
                        .FirstOrDefaultAsync(c => c.UserId == userId);

                    if (cart == null)
                    {
                        cart = new Cart
                        {
                            CartId = Guid.NewGuid(),
                            UserId = userId,
                            CreateDate = DateTime.UtcNow,
                            UpdateDate = DateTime.UtcNow,
                            CartItems = new List<CartItem>()
                        };
                        _context.Carts.Add(cart);
                    }

                    var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.CourseId == courseId);
                    if (existingCartItem == null)
                    {
                        var cartItem = new CartItem
                        {
                            CartItemId = Guid.NewGuid(),
                            CartId = cart.CartId,
                            CourseId = courseId,
                            IsDeleted = false,
                            CreateDate = DateTime.UtcNow,
                            UpdateDate = DateTime.UtcNow
                        };
                         _context.CartItems.Add(cartItem);
                    }

                    await _context.SaveChangesAsync();
                    return new { SuccMessage = "Course added to cart successfully." };
                }

                catch (Exception)
                {
                    throw new Exception("An error occurred while adding the course to the cart.");
                }
            
        }


        public async Task<dynamic> GetCartItemById(Guid id)
        {
            return await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartItemId == id);
        }

        public async Task<dynamic> ViewCart(string userId)
        {
            try
            {
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Course)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    return null;
                }

                var cartInfor = new CartInfor
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId,
                    UpdateDate = cart.UpdateDate,
                    CartItems = cart.CartItems.Select(ci => new CartItemInfo
                    {
                        CartItemId = ci.CartItemId,
                        CourseId = ci.CourseId,
                        CreateDate = ci.CreateDate,
                    }).ToList()
                };

                return new { Success = true, Cart = cartInfor };
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return new { Success = false, Message = "An error occurred while retrieving the cart.", Details = ex.Message };
            }


        }
    }
}
