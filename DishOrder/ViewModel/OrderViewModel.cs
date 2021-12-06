using DishOrder.DBContext;
using DishOrder.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DishOrder.ViewModel
{
    class OrderViewModel : BaseViewModel
    {
        private ObservableCollection<OrderDish> orderDish;

        public DishOrderContext Context { get; }
        public Order OrderInfo { get; set; } = new Order();
        public Order SelectedOrder { get; set; }
        public Dish SelectedDish { get; set; }
        public OrderDish SelectedOrderDish { get; set; }
        public DishCategory SelectedDishCategory { get; set; }
        public ObservableCollection<OrderDish> OrderDish
        {
            get { return orderDish; }
            set
            {
                orderDish = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Dish> dishes;
        public ObservableCollection<Dish> Dishes
        {
            get { return dishes; }
            set
            {
                dishes = value;
                OnPropertyChanged();
            }
        }

        public OrderViewModel(DishOrderContext context)
        {
            Context = context;
            Context.Orders.Load();
            Context.Tables.Load();
            Context.Dishes.Load();
            Context.OrderDishes.Load();
            Context.Users.Load();
            Context.DishCategories.Load();

            OrderDish = new ObservableCollection<OrderDish>();

            Dishes = ToObservableCollection(Context.Dishes.ToList());
        }

        private RelayCommand updateOrderCommand;
        public ICommand UpdateOrderCommand =>
            updateOrderCommand ??
            (updateOrderCommand = new RelayCommand(
                () =>
                {
                    if (SelectedOrder == null)
                        return;

                    SelectedOrder.Paid = OrderInfo.Paid;
                    SelectedOrder.Cooked = OrderInfo.Cooked;
                    SelectedOrder.Cook = OrderInfo.Cook;
                    SelectedOrder.Table = OrderInfo.Table;
                    SelectedOrder.TotalCost = 0;


                    foreach (var item in Context.OrderDishes.Where(w => w.FKOrder == SelectedOrder.OrderId))
                    {
                        Context.OrderDishes.Remove(item);
                    }

                    Context.SaveChanges();

                    UpdateDishOrder(SelectedOrder.OrderId);

                    OrderInfo.TotalCost = Context.Orders.Where(w => w.OrderId == SelectedOrder.OrderId).FirstOrDefault().TotalCost;

                    Context.SaveChanges();

                    OrderDish = ToObservableCollection(Context.OrderDishes.Where(w => w.Order.OrderId == SelectedOrder.OrderId));
                }));

        private RelayCommand orderSelectionCommand;
        public ICommand SelectionChangedCommand =>
            orderSelectionCommand ??
            (orderSelectionCommand =
                new RelayCommand(
                    () =>
                    {
                        if (SelectedOrder != null)
                        {
                            OrderInfo.Paid = SelectedOrder.Paid;
                            OrderInfo.Cooked = SelectedOrder.Cooked;
                            OrderInfo.Cook = SelectedOrder.Cook;
                            OrderInfo.Date = SelectedOrder.Date;
                            OrderInfo.Table = SelectedOrder.Table;
                            OrderInfo.TotalCost = Context.Orders
                                                                .Where(w => w.OrderId == SelectedOrder.OrderId)
                                                                .FirstOrDefault().TotalCost;

                            OrderDish = ToObservableCollection(Context.OrderDishes.Where(w => w.Order.OrderId == SelectedOrder.OrderId));
                        }
                    }));

        public ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }

        private RelayCommand addOrderCommand;
        public ICommand AddOrderCommand =>
            addOrderCommand ??
            (addOrderCommand =
            new RelayCommand(
                () =>
                {
                    if (OrderDish.Count() == 0 || OrderInfo.Cook == null || OrderInfo.Table == null)
                        return;

                    Order order = new Order
                    {
                        Paid = OrderInfo.Paid,
                        Cooked = OrderInfo.Cooked,
                        Cook = OrderInfo.Cook,
                        Date = DateTime.Now,
                        Table = OrderInfo.Table
                    };

                    Context.Orders.Add(order);

                    Context.SaveChanges();

                    int OrderId = Context.Orders.Max(w => w.OrderId);

                    UpdateDishOrder(OrderId);

                    Context.SaveChanges();

                    SelectedOrder = order;

                    OrderInfo.OrderDishes = Context.OrderDishes.Where(w => w.FKOrder == OrderId).ToList();
                }));

        private RelayCommand removeOrderCommand;
        public ICommand RemoveOrderCommand =>
            removeOrderCommand ??
            (removeOrderCommand =
            new RelayCommand(
                () =>
                {
                    if (SelectedOrder != null)
                    {
                        Context.Orders.Remove(SelectedOrder);
                        Context.SaveChanges();
                    }
                }));

        private RelayCommand addDishToOrderCommand;
        public ICommand AddDishToOrderCommand =>
            addDishToOrderCommand ??
            (addDishToOrderCommand =
            new RelayCommand(
                () =>
                {
                    if (SelectedDish == null) return;

                    if (OrderDish.Any(a => a.FKDish == SelectedDish.DishId))
                    {
                        OrderDish.FirstOrDefault(w => w.FKDish == SelectedDish.DishId).Count += 1;
                    }
                    else
                    {
                        Dish dish = Context.Dishes.FirstOrDefault(w => w.DishId == SelectedDish.DishId);

                        OrderDish.Add(new OrderDish { FKDish = dish.DishId, Dish = dish });
                        OrderDish.FirstOrDefault(w => w.FKDish == SelectedDish.DishId).Count += 1;
                    }
                }));

        private RelayCommand removeDishFromOrderCommand;
        public ICommand RemoveDishFromOrderCommand =>
            removeDishFromOrderCommand ??
            (removeDishFromOrderCommand =
            new RelayCommand(
                () =>
                {
                    if (SelectedOrderDish == null) return;

                    if (SelectedOrderDish.Count == 1)
                    {
                        OrderDish.Remove(SelectedOrderDish);
                    }
                    else
                    {
                        OrderDish.FirstOrDefault(w => w.FKDish == SelectedOrderDish.FKDish).Count -= 1;
                    }
                }));

        private RelayCommand removeAllDishesCommand;
        public ICommand RemoveAllDishesCommand =>
            removeAllDishesCommand ??
            (removeAllDishesCommand =
            new RelayCommand(
                () =>
                {
                    OrderDish.Clear();
                }));

        private RelayCommand selectionDishCategory;
        public ICommand SelectionDishCategory =>
            selectionDishCategory ??
            (selectionDishCategory =
            new RelayCommand(
                () =>
                {
                    Dishes = ToObservableCollection(Context.Dishes.
                        Where(w => w.DishCategory.DishCategoryId == SelectedDishCategory.DishCategoryId));
                }));

        private void UpdateDishOrder(int OrderId)
        {
            int tmpDish = 0;

            foreach (var item in this.OrderDish.OrderBy(o => o.FKDish))
            {
                int count = this.OrderDish.FirstOrDefault(w => w.FKDish == item.FKDish).Count;

                if (tmpDish == 0 || tmpDish != item.FKDish)
                {
                    Context.OrderDishes.Add(new OrderDish
                    {
                        FKDish = item.FKDish,
                        FKOrder = OrderId,
                        Count = count
                    });
                }

                tmpDish = item.FKDish;

                Context.Orders.Where(w => w.OrderId == SelectedOrder.OrderId).FirstOrDefault().TotalCost
                +=
                Context.Dishes.Where(w => w.DishId == item.FKDish).FirstOrDefault().Cost
                *
                item.Count;
            }
        }
    }
}
