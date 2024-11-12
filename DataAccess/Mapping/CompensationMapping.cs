using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;

namespace DataAccess.Mapping;

public class CompensationMapping : IEntityTypeConfiguration<Compensation>
{
    public void Configure(EntityTypeBuilder<Compensation> builder)
    {
        var instantConverter =
        new ValueConverter<Instant, DateTime>(v =>
         v.ToDateTimeUtc(),
         v => Instant.FromDateTimeUtc(v));

        builder.Property(e => e.CompensationRequestedAtUtc)
            .HasConversion(instantConverter);
        //builder.Property(e => e.CompensationRequestedForYearAndMonth)
        //    .HasConversion(instantConverter);
    }
}