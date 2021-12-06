using System.Collections.Generic;

namespace DishOrder.Model
{
    public class DishCategory : BaseViewModel
    {
        private int dishCategoryId;
        private string categoryName;
        private ICollection<Dish> dish;

        public int DishCategoryId
        {
            get => dishCategoryId;
            set
            {
                if (value == dishCategoryId) return;
                dishCategoryId = value;
                OnPropertyChanged();
            }
        }

        public string CategoryName
        {
            get => categoryName;
            set
            {
                if (value == categoryName) return;
                categoryName = value;
                OnPropertyChanged();
            }
        }

        public ICollection<Dish> Dish
        {
            get => dish;
            set
            {
                if (Equals(value, dish)) return;
                dish = value;
                OnPropertyChanged();
            }
        }
    }
}
