using Cursus_Data.Context;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly LMS_CursusDbContext _context;

        public RefreshTokenRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }
        public async Task AddRefreshTokenAsync(string tokenId, string userId, string refreshToken)
        {
            RefreshToken rfToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                JwtID = tokenId,
                UserId = userId,
                Token = refreshToken,
                IsUsed = false,
                IsRevoke = false,   
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMinutes(30)
            };
            await _context.RefreshTokens.AddAsync(rfToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> CheckRefreshTokenAsync(RenewTokenDTO tokenDTO)
        {
            return _context.RefreshTokens.FirstOrDefault(x => x.Token == tokenDTO.RefreshToken);

        }

        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
        }
    }
}



