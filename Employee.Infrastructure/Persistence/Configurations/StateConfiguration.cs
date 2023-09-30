using Employee.Model;
using Employee.Shared.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Persistence.Configurations;
public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Country).WithMany(x => x.State).HasForeignKey(x => x.CountryId).IsRequired(true);
        builder.HasData(new
        {
            Id = 1,
            StateName = "Dhaka",
            CountryId = 1,
            Created = DateTimeOffset.Now,
            CreatedBy = "1",
            LastModified = DateTimeOffset.Now,
            Status = EntityStatus.Created
        });
    }
}


