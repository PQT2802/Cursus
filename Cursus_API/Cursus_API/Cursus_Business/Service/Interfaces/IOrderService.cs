using Cursus_Data.Models.DTOs.CommonObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IOrderService
    {
        Task<dynamic> CreateOrderFromCart(string userId, List<Guid> selectedCartItemIds);
        Task<dynamic>PayUserOrder(string userId, Guid orderId);
        Task<dynamic> ViewOrder(string userId ,OrderListConfig config);
    }
}
