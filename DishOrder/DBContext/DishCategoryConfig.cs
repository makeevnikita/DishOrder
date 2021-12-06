using DishOrder.Model;
using System.Data.Entity.ModelConfiguration;

namespace DishOrder.DBContext
{
    public class DishCategoryConfig : EntityTypeConfiguration<DishCategory>
    {
        public DishCategoryConfig()
        {
            HasKey(dishCategory => dishCategory.DishCategoryId);
            Property(dishCategory => dishCategory.CategoryName).IsRequired().HasMaxLength(15);
        }
    }
}
