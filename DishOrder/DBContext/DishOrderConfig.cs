using DishOrder.Model;
using System.Data.Entity.ModelConfiguration;

namespace DishOrder.DBContext
{
    class DishOrderConfig : EntityTypeConfiguration<OrderDish>
    {
        public DishOrderConfig()
        {
            HasRequired(dish => dish.Dish).WithMany(orderDish => orderDish.DishOrders).WillCascadeOnDelete(true);
            HasRequired(order => order.Order).WithMany(orderDish => orderDish.OrderDishes).WillCascadeOnDelete(true);

            ToTable("DishOrders");
        }
    }
}
