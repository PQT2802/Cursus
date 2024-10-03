using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cursus_Data.Models.DTOs;
using Cursus_Business.Service.Interfaces;
using Cursus_Business.Service.Implements;
using Microsoft.AspNetCore.Cors;

namespace Cursus_API.Controllers
{
    [Route("api/v1.0/")]
    [ApiController]
    [EnableCors]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService) 
        {
            _tokenService = tokenService;
        }

        [HttpPost("Token/renew-token")]
        public async Task<IActionResult> RenewToken(RenewTokenDTO tokenDTO)
        {
            TokenResponseDTO t = await _tokenService.RenewTokenAsync(tokenDTO);
            return Ok(t);
        }
    }
}
