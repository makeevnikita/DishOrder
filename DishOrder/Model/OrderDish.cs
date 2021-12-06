using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DishOrder.Model
{
    public class OrderDish : BaseViewModel
    {
        private int count;
        private Dish dish;
        private Order order;
        private int fkDish;
        private int fkOrder;

        [Key]
        [ForeignKey("Order")]
        [Column(Order = 10)]
        public int FKOrder
        {
            get => fkOrder;
            set
            {
                if (value == fkOrder) return;
                fkOrder = value;
                OnPropertyChanged();
            }
        }

        [Key]
        [ForeignKey("Dish")]
        [Column(Order = 20)]
        public int FKDish
        {
            get => fkDish;
            set
            {
                if (value == fkDish) return;
                fkDish = value;
                OnPropertyChanged();
            }
        }

        public Order Order
        {
            get => order;
            set
            {
                if (value == order) return;
                order = value;
                OnPropertyChanged();
            }
        }

        public Dish Dish
        {
            get => dish;
            set
            {
                if (value == dish) return;
                dish = value;
                OnPropertyChanged();
            }
        }

        public int Count
        {
            get => count;
            set
            {
                if (value == count) return;
                count = value;
                OnPropertyChanged();
            }
        }
    }
}
