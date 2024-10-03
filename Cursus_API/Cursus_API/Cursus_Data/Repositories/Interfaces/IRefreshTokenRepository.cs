using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshTokenAsync(string tokenId, string userId, string refreshToken);
        Task<RefreshToken> CheckRefreshTokenAsync(RenewTokenDTO tokenDTO);
        Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
    }
}
