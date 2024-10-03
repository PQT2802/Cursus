using Cursus_Business.Common;
using Cursus_Data.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IVnPaymentService
    {
      Task<dynamic> CreatePaymentUrl(HttpContext context, double depositMoney, string userId);
      Task<dynamic> PaymentExecute(IQueryCollection collections, string userId, double depositMoney);
    }
}
