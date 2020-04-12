using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Entities;

namespace Shop.Data
{
    public class BalanceDataConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasData(new PaymentType()
            {
                Id = 1,
                Name = "Card"
            },
            new PaymentType()
            {
                Id = 2,
                Name = "Cash"
            });
        }
    }
}
