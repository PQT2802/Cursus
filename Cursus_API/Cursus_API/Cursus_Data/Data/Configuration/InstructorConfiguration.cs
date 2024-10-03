using Cursus_Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Data.Configuration
{

    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasData(
                new Instructor
                {
                    InstructorId = "INS00000001",
                    UserId = "US00000003",
                    TaxNumber = "Tax001",
                    CardNumber = "Card001",
                    CardName = "CardName1",
                    CardProvider = "CardProvider1",
                    IsAccepted = true,
                    Certification = "Certification1"
                },
                new Instructor
                {
                    InstructorId = "INS00000002",
                    UserId = "US00000004",
                    TaxNumber = "Tax002",
                    CardNumber = "Card002",
                    CardName = "CardName2",
                    CardProvider = "CardProvider2",
                    IsAccepted = true,
                    Certification = "Certification2"
                }
            );
        }
    }
}
