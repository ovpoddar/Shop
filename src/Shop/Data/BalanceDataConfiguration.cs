using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
