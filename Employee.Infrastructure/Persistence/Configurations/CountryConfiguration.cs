using Employee.Model;
using Employee.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Persistence.Configurations;
public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {

        builder.HasKey(x => x.Id);
        builder.HasData(new
        {
            Id = 1,
            CountryName = "Bangladesh",
            Currency = "BDT",
            Created = DateTimeOffset.Now,
            CreatedBy = "1",
            LastModified = DateTimeOffset.Now,
            Status = EntityStatus.Created
        });
    }
}