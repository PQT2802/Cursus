using Cursus_Business.Common;
using Cursus_Business.Service.Implements;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Cursus_API.Controllers
{
    [Route("api/v1.0/")]
    [ApiController]
    [EnableCors]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IInstructorService _instructorService;

        public WalletController(IWalletService walletService, IInstructorService instructorService)
        {
            _walletService = walletService;
            _instructorService = instructorService;
        }
        #region 26 Earning analytics - LDQ
        [Authorize(Policy = "RequireInstructorRole")]
        [HttpGet("Wallet/received-money-each-course")]
        public async Task<IActionResult> AvailableMoneyOfEachCourse()
        {
            try
            {
                CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                string instructorid = await _instructorService.GetInstructorId(c.UserId); 
                var results = await _walletService.AvailableMoneyEachCourseOfInstructor(instructorid);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [Authorize(Policy = "RequireInstructorRole")]
        [HttpGet("Wallet/received-money-all-course")]
        public async Task<IActionResult> AvailableMoneyOfAllCourse()
        {
            try
            {
                CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                string instructorid = await _instructorService.GetInstructorId(c.UserId);
                var results = await _walletService.AvailableMoneyAllCourseOfInstructor(instructorid);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Policy = "RequireInstructorRole")]
        [HttpGet("Wallet/available-money-of-instructor")]
        public async Task<IActionResult> AvailableMoneyCanWithdraw()
        {
            try
            {
                CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                var results = await _walletService.AvailableMoneyCanWithdrawOfInstructor(c.UserId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Policy = "RequireInstructorRole")]
        [HttpPut("Wallet/withdraw-to-wallet/{money}")]
        public async Task<IActionResult> WithdrawToWallet(double money)
        {
            try
            {
                CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                var results = await _walletService.WithdrawMoneyToWallet(c.UserId, money);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("Wallet/deposit-to-wallet/{money}")]
        public async Task<IActionResult> DepositToWallet(double money)
        {
            try
            {
                CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                var results = await _walletService.WithdrawMoneyToWallet(c.UserId, money);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        #endregion

    }
}
