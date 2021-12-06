using DishOrder.Model;
using System.Data.Entity.ModelConfiguration;

namespace DishOrder.DBContext
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            HasKey(user => user.UserId);
            Property(user => user.Name).HasMaxLength(10).IsRequired();
            Property(user => user.Surname).HasMaxLength(10).IsRequired();
            Property(user => user.Patronymic).HasMaxLength(10).IsRequired();

            ToTable("Users");
        }
    }
}
