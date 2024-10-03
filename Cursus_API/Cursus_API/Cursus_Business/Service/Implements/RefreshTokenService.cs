using Cursus_Business.Service.Interfaces;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<string> GenerateRefreshTokenAsync()
        {
            var random = new byte[32]; 
            using (var rng = RandomNumberGenerator.Create()) 
            {
                rng.GetBytes(random); //dien so vao mang
                return Convert.ToBase64String(random); //chuyen mang byte thanh base64
            }
        }

        public async Task SaveRefreshTokenAsync(string tokenId, string userId, string refreshToken)
        {
            _refreshTokenRepository.AddRefreshTokenAsync(tokenId, userId, refreshToken);
        }


    }
}
