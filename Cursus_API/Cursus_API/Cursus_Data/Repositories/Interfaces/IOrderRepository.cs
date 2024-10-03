using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<dynamic> CreateOrderFromCart(string userId, List<Guid> selectedCartItemIds);

        Task<dynamic> PayOrder(Guid orderId);
        Task<List<ViewOrder>> GetUserOrderListByUserId(string userId, OrderListConfig config);
    }
}
