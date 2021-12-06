using DishOrder.Model;
using System.Data.Entity.ModelConfiguration;

namespace DishOrder.DBContext
{
    class TableConfig : EntityTypeConfiguration<Table>
    {
        public TableConfig()
        {
            HasKey(table => table.TableId);
            Property(table => table.TableName).IsRequired().HasMaxLength(10);
        }
    }
}
