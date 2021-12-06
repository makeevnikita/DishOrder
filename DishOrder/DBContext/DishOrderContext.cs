using DishOrder.Model;
using System.Data.Entity;

namespace DishOrder.DBContext
{
    public class DishOrderContext : DbContext
    {
        public DishOrderContext()
            : base("CafeDbConnectionString")
        {

        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDish> OrderDishes { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new DishConfig());
            modelBuilder.Configurations.Add(new DishOrderConfig());
            modelBuilder.Configurations.Add(new DishCategoryConfig());
            modelBuilder.Configurations.Add(new OrderConfig());
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new TableConfig());
        }
    }
}
