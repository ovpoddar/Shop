using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.Configuration
{
    public class PaymentTypeDataConfiguration : IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder.HasData(new Balance()
            {
                Id = 1,
                Ammount = 500,
                Date = DateTime.Now,
                ProductId = null,
                Quantity = 0,
                Incoming = 500,
                PaymentTypeId = 2,
                Outgoing = 0,
            });
        }
    }
}
