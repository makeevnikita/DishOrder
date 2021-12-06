using System;
using System.Collections.Generic;

namespace DishOrder.Model
{
    public class Order : BaseViewModel
    {
        private int orderId;
        private bool paid;
        private bool cooked;
        private User cook;
        private DateTime date;
        private Table table;
        private decimal totalCost;
        private ICollection<OrderDish> dishOrders;

        public int OrderId
        {
            get => orderId;
            set
            {
                if (value == orderId) return;
                orderId = value;
                OnPropertyChanged();
            }
        }

        public bool Paid
        {
            get => paid;
            set
            {
                if (value == paid) return;
                paid = value;
                OnPropertyChanged();
            }
        }

        public bool Cooked
        {
            get => cooked;
            set
            {
                if (value == cooked) return;
                cooked = value;
                OnPropertyChanged();
            }
        }

        public User Cook
        {
            get => cook;
            set
            {
                if (value == cook) return;
                cook = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                if (value == date) return;
                date = value;
                OnPropertyChanged();
            }
        }

        public Table Table
        {
            get => table;
            set
            {
                if (value == table) return;
                table = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalCost
        {
            get => totalCost;
            set
            {
                if (value == totalCost) return;
                totalCost = value;
                OnPropertyChanged();
            }
        }

        public ICollection<OrderDish> OrderDishes
        {
            get => dishOrders;
            set
            {
                if (dishOrders == value) return;
                dishOrders = value;
                OnPropertyChanged();
            }
        }

    }
}
