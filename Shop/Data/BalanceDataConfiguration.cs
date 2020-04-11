using Microsoft.EntityFrameworkCore;
using Shop.Entities;

namespace Shop.Data
{
    public static class BalanceDataConfiguration
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentType>().HasData(new PaymentType
            {
                Id = 1,
                Name = "Card"
            },
            new PaymentType
            {
                Id = 2,
                Name = "Cash"
            });
        }
    }
}
