using Cursus_Business.Common;
using Cursus_Business.Common.VnPayment;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class VnPaymentService : IVnPaymentService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _config;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IWalletService _walletService;
        private readonly ISystemTransactionRepository _systemTransactionRepository;

        private readonly double minDepositMoney =10000;

        public VnPaymentService(IOrderRepository orderRepository, IConfiguration configuration,IPaymentRepository paymentRepository
            ,IWalletService walletService,ISystemTransactionRepository systemTransactionRepository) 
        {
         _orderRepository = orderRepository;
            _config = configuration;
            _paymentRepository = paymentRepository;
            _walletService = walletService;
            _systemTransactionRepository = systemTransactionRepository;
        }
        public async Task<dynamic> CreatePaymentUrl(HttpContext context, double depositMoney, string userId)
        {
            const double minDepositMoney = 10000; // Assuming this is defined somewhere globally or passed in the configuration
            if (depositMoney < minDepositMoney)
            {
                return Result.Failure(new Error("Wallet", "Deposit money must be larger than 10000"));
            }

            var tick = DateTime.Now.Ticks.ToString();
            var vnpay = new VnPayLibrary();

            string paymentBackReturnUrl = _config["VnPay:PaymentBackReturnUrl"];
            if (string.IsNullOrEmpty(paymentBackReturnUrl))
            {
                return Result.Failure(new Error("Configuration", "PaymentBackReturnUrl is not configured"));
            }

            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["VnPay:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (depositMoney * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _config["VnPay:Locale"]);
            vnpay.AddRequestData("vnp_OrderInfo", "Deposit to wallet");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", $"{paymentBackReturnUrl}?userId={userId}&depositMoney={depositMoney}");
            vnpay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl = vnpay.CreateRequestUrl(_config["VnPay:BaseUrl"], _config["VnPay:HashSecret"]);

            return Result.SuccessWithObject(paymentUrl);
        }

        public async Task<dynamic> PaymentExecute(IQueryCollection collections, string userId, double depositMoney)
        {
            var vnpay = new VnPayLibrary();
            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);
            if (!checkSignature)
            {
                return Result.Failure(new Error("Transaction", "Transaction failed due to invalid signature"));
            }

            if (vnp_ResponseCode == "00") // Assuming "00" is the success code from VnPay
            {
                Result depositResult = await _walletService.DepositMoneyToWallet(userId, depositMoney);

                if (depositResult.IsSuccess)
                {
                    var response = new VnPaymentResponseModel
                    {
                        PaymentMethod = "VnPay",
                        OrderDescription = vnp_OrderInfo,
                        OrderId = vnp_orderId.ToString(),
                        TransactionId = vnp_TransactionId.ToString(),
                        Token = vnp_SecureHash,
                        VnPayResponseCode = vnp_ResponseCode
                    };
                   // await _systemTransactionRepository.AddSystemTransaction(userId, userId, depositMoney, Common.Status.Deposit);

                    return Result.SuccessWithObject(response);
                }
                else
                {
                    return depositResult;
                }
            }
            else
            {
                return Result.Failure(new Error("Order", "Transaction failed with response code: " + vnp_ResponseCode));
            }
        }
    }
}
