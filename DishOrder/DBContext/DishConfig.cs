using DishOrder.Model;
using System.Data.Entity.ModelConfiguration;

namespace DishOrder.DBContext
{
    public class DishConfig : EntityTypeConfiguration<Dish>
    {
        public DishConfig()
        {
            HasKey(dish => dish.DishId);
            Property(dish => dish.DishName).IsRequired().HasMaxLength(20);
            Property(dish => dish.Cost).IsRequired().HasPrecision(10, 2);
            HasRequired(dish => dish.DishCategory).WithMany(dishCategory => dishCategory.Dish).WillCascadeOnDelete(true);

            ToTable("Dishes");
        }
    }
}
