using DishOrder.Model;
using System.Data.Entity.ModelConfiguration;

namespace DishOrder.DBContext
{
    class OrderConfig : EntityTypeConfiguration<Order>
    {
        public OrderConfig()
        {
            HasKey(order => order.OrderId);
            HasRequired(order => order.Cook);
            HasRequired(table => table.Table);

            ToTable("Orders");
        }
    }
}
