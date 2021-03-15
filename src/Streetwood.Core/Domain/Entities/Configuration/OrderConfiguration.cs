﻿namespace Streetwood.Core.Domain.Entities.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Streetwood.Core.Constants;

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd();
            builder.Property(s => s.BasePrice)
                .HasColumnType(ConstantValues.PriceDecimalType);
            builder.Property(s => s.FinalPrice)
                .HasColumnType(ConstantValues.PriceDecimalType);

            builder.HasMany(s => s.ProductOrders)
                .WithOne(s => s.Order)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.OrderDiscount)
                .WithMany(s => s.Orders)
                .HasForeignKey("OrderDiscountId");

            builder.HasOne(s => s.User)
                .WithMany(s => s.Orders)
                .HasForeignKey("UserId");

            builder.HasOne(s => s.Address)
                .WithMany(s => s.Orders)
                .HasForeignKey("AddressId");

            builder.HasOne(x => x.OrderPayment)
                .WithOne()
                .HasForeignKey<OrderPayment>("OrderId");

            builder.HasOne(x => x.OrderShipment)
                .WithOne()
                .HasForeignKey<OrderShipment>("OrderId");
        }
    }
}
