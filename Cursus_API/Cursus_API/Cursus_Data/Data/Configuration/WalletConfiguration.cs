using Cursus_Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Cursus_Data.Data.Configuration
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasData(
                new Wallet
                {
                    WlId = "WL00000001",
                    UserId = "US00000001",
                    Amount = 0.0,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "system",
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = "system"
                },
                new Wallet
                {
                    WlId = "WL00000002",
                    UserId = "US00000002",
                    Amount = 0.0,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "system",
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = "system"
                },
                new Wallet
                {
                    WlId = "WL00000003",
                    UserId = "US00000003",
                    Amount = 0.0,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "system",
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = "system"
                },
                new Wallet
                {
                    WlId = "WL00000004",
                    UserId = "US00000004",
                    Amount = 0.0,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "system",
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = "system"
                }
            );
        }
    }
}
