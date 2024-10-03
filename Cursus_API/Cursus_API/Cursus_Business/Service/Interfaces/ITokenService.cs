using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface ITokenService
    {
        Task<SecurityToken> GenerateTokenAsync(CurrentUserObject currentUserObject);
        Task<dynamic> RenewTokenAsync(RenewTokenDTO tokenDTO);
        Task<string> GenerateAccessTokenAsync(SecurityToken token);
    }
}
