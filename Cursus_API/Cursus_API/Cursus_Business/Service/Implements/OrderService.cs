using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class OrderService : IOrderService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ISystemTransactionRepository _systemTransactionRepository;
        private readonly IEnrollCourseRepository _enrollCourseRepository;
        private readonly ICourseVersionRepository _courseVersionRepository;
        private readonly ICourseVersionDetailRepository _courseVersionDetailRepository;
        public OrderService (ICourseRepository courseRepository, IUserRepository userRepository,IOrderRepository orderRepository
            ,ICartRepository cartRepository,IWalletRepository walletRepository,IPaymentRepository paymentRepository
            ,ISystemTransactionRepository systemTransactionRepository,IEnrollCourseRepository enrollCourseRepository
            ,ICourseVersionRepository courseVersionRepository,ICourseVersionDetailRepository courseVersionDetailRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _courseRepository = courseRepository;
            _cartRepository = cartRepository;
            _walletRepository = walletRepository;
            _paymentRepository = paymentRepository;
            _systemTransactionRepository = systemTransactionRepository;
            _enrollCourseRepository = enrollCourseRepository;
            _courseVersionRepository = courseVersionRepository;
            _courseVersionDetailRepository = courseVersionDetailRepository;
        }
        public async Task<dynamic> CreateOrderFromCart(string userId, List<Guid> selectedCartItemIds)
        {
           var checkUser = await _userRepository.GetUserByIdAsync(userId);
            if (checkUser == null)
            {
                return Result.Failure(UserErrors.UserIsNotExist);
            }
            foreach (var cartItem in selectedCartItemIds)
            {
                var check = await _cartRepository.GetCartItemById(cartItem);
                if (check == null)
                {
                    return Result.Failure(new Error("CartItem", $"CartItem with ID {cartItem} does not exist"));
                }
            }
            var createOder = await _orderRepository.CreateOrderFromCart(userId, selectedCartItemIds);
            if (createOder != null)
            {
                return Result.SuccessWithObject(createOder);
            }
            return Result.FailureWithObjects(createOder);

        }

        public async Task<dynamic> PayUserOrder(string userId, Guid orderId)
        {
            var checkUser = await _userRepository.GetUserByIdAsync(userId); if (checkUser == null) return Result.Failure(UserErrors.UserIsNotExist);
            Wallet getWallet = await _walletRepository.GetWalletByUserId(userId);
            Order getOrder = await _orderRepository.GetOrderByIdAsync(orderId);
            if (getOrder == null) { return Result.Failure(new Error("Order", "Order does not exist")); }
            if (getOrder.Status == OrderStatus.Paid.ToString()) { return Result.Failure(new Error("Order", "Order already paid")); }
            if (getWallet.Amount < getOrder.TotalAmount)
            {
                return Result.Failure(new Error("Wallet", "Your wallet does not enough to pay order, please deposit !!!"));
            }
            List<string> listPurchasedCourse = new List<string>();
            var updateOrder = _orderRepository.PayOrder(orderId);
            if(updateOrder != null)
            {
                var payment = new Payment()
                {
                    PaymentId = Guid.NewGuid(),
                    OrderId = orderId,
                    Amount = getOrder.TotalAmount,
                    Code =  GeneratePaymentCode(), // Assume you have a method to generate payment codes
                    TransactionDate = DateTime.UtcNow,
                    TransactionId = Guid.NewGuid()
                };
                getWallet.Amount -= payment.Amount;
               await  _walletRepository.UpdateWallet(getWallet);
               await _paymentRepository.CreatePayment(payment);
               await _systemTransactionRepository.AddSystemTransaction(userId, userId, payment.Amount, Common.Status.Purchased);
                
                foreach (var item in getOrder.OrderItems)
                {
                    var courseVersionId = await _courseVersionRepository.GetCourseVersionIdInUseByCourseId(item.CourseId);
                    EnrollCourse enrollCourse = new EnrollCourse()
                    {
                        EnrollCourseId = await _enrollCourseRepository.AutoGenerateEnrollCourseId(),
                        UserId = userId,
                        CourseId = item.CourseId,
                        CourseVersionId = courseVersionId,
                        EndEnrollDate = DateTime.UtcNow,
                        CreatedDate = DateTime.UtcNow,
                        Status = Common.Status.Purchased,
                        Process = 0
                    };

                    await _enrollCourseRepository.AddEnrollCourse(enrollCourse);
                    await _courseVersionDetailRepository.TrackingAlreadyEnrolled(courseVersionId);
                    listPurchasedCourse.Add(item.CourseId);
                }
            }

            return Result.SuccessWithObject(listPurchasedCourse);
        }

        public async Task<dynamic> ViewOrder(string userId, Cursus_Data.Models.DTOs.CommonObject.OrderListConfig config)
        {
            var checkUser = await _userRepository.GetUserByIdAsync(userId);
            if (checkUser == null) 
            {
                return Result.Failure(UserErrors.UserIsNotExist);
            }
            var list = await _orderRepository.GetUserOrderListByUserId(userId,config);
            return Result.SuccessWithObject(list);
        }

        private string GeneratePaymentCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
        }
    }
}
