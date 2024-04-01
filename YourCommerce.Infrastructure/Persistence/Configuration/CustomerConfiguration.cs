using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCommerce.Domain.Customers;
using YourCommerce.Domain.ValueObjects;

namespace YourCommerce.Infrastructure.Persistence.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasConversion(
            CustomerId => CustomerId.value,
            value => new CustomerId(value)
        );

        builder.Property(e => e.Name).HasMaxLength(50);

        builder.Property(e => e.LastName).HasMaxLength(50);

        builder.Property(e => e.Email).HasMaxLength(255);

        builder.HasIndex(e => e.Email).IsUnique();

        builder.Ignore(e => e.FullName);

        builder.Property(e => e.PhoneNumber).HasConversion(
            phoneNumber => phoneNumber.Value,
            value => PhoneNumber.Create(value)!
        ).HasMaxLength(12);

        builder.OwnsOne(e => e.Address, addressBuilder => {

            addressBuilder.Property(a => a.Country).HasMaxLength(3);

            addressBuilder.Property(a => a.Street1).HasMaxLength(100);

            addressBuilder.Property(a => a.Street2).HasMaxLength(100).IsRequired(false);

            addressBuilder.Property(a => a.City).HasMaxLength(40);

            addressBuilder.Property(a => a.State).HasMaxLength(40);

            addressBuilder.Property(a => a.ZipCode).HasMaxLength(10).IsRequired(false);

        });

        builder.Property(e => e.Active);
    }
}