using System.Collections.Generic;

namespace DishOrder.Model
{
    public class Dish : BaseViewModel
    {
        private int dishId;
        private string dishName;
        private decimal cost;
        private ICollection<OrderDish> dishOrders;
        private DishCategory dishCategory;

        public int DishId
        {
            get => dishId;
            set
            {
                if (value == dishId) return;
                dishId = value;
                OnPropertyChanged();
            }
        }

        public string DishName
        {
            get => dishName;
            set
            {
                if (value == dishName) return;
                dishName = value;
                OnPropertyChanged();
            }
        }

        public decimal Cost
        {
            get => cost;
            set
            {
                if (value == cost) return;
                cost = value;
                OnPropertyChanged();
            }
        }

        public ICollection<OrderDish> DishOrders
        {
            get => dishOrders;
            set
            {
                if (Equals(value, dishOrders)) return;
                dishOrders = value;
                OnPropertyChanged();
            }
        }

        public DishCategory DishCategory
        {
            get => dishCategory;
            set
            {
                if (dishCategory == value) return;
                dishCategory = value;
                OnPropertyChanged();
            }
        }
    }
}
