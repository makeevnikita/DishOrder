using DishOrder.DBContext;
using DishOrder.Model;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace DishOrder.ViewModel
{
    class DishViewModel : BaseViewModel
    {
        private IList<Dish> dishes;

        public DishOrderContext Context { get; }
        public Dish DishInfo { get; set; } = new Dish();
        public Dish SelectedDish { get; set; }
        public DishCategory SelectedDishCategory { get; set; }
        public IList<Dish> Dishes
        {
            get => dishes;
            set
            {
                dishes = value;
                OnPropertyChanged();
            }
        }

        public DishViewModel(DishOrderContext context)
        {
            Context = context;

            Context.DishCategories.Load();
            Dishes = ToObservableCollection(Context.Dishes.ToList());
        }

        public ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }

        private RelayCommand updateDishCommand;
        public ICommand UpdateDishCommand =>
            updateDishCommand ??
            (updateDishCommand = new RelayCommand(
                () =>
                {
                    if (DishInfo.DishName == "" || SelectedDish == null)
                        return;

                    SelectedDish.DishName = DishInfo.DishName;
                    SelectedDish.Cost = DishInfo.Cost;
                    SelectedDish.DishCategory = DishInfo.DishCategory;
                    Context.SaveChanges();
                }));


        private RelayCommand dishSelectionCommand;
        public ICommand SelectionChangedCommand =>
            dishSelectionCommand ??
            (dishSelectionCommand =
                new RelayCommand(
                    () =>
                    {
                        if (SelectedDish == null)
                            return;

                        DishInfo.DishName = SelectedDish.DishName;
                        DishInfo.Cost = SelectedDish.Cost;
                        DishInfo.DishCategory = SelectedDish.DishCategory;
                    }));


        private RelayCommand addDishCommand;
        public ICommand AddDishCommand =>
            addDishCommand ??
            (addDishCommand =
            new RelayCommand(
                () =>
                {
                    if (DishInfo.DishName == "" || DishInfo.DishCategory == null)
                        return;

                    Dish dish = new Dish
                    {
                        DishName = DishInfo.DishName,
                        Cost = DishInfo.Cost,
                        DishCategory = DishInfo.DishCategory
                    };

                    Context.Dishes.Add(dish);
                    Context.SaveChanges();

                    SelectedDish = dish;

                    if (SelectedDishCategory == null)
                    {
                        Dishes = ToObservableCollection(Context.Dishes.ToList());
                    }
                    else
                    {
                        Dishes = ToObservableCollection(Context.Dishes.Where(w =>
                    w.DishCategory.DishCategoryId == DishInfo.DishCategory.DishCategoryId));
                    }
                }));

        private RelayCommand removeDishCommand;
        public ICommand RemoveDishCommand =>
            removeDishCommand ??
            (removeDishCommand =
            new RelayCommand(
                () =>
                {
                    if (SelectedDish == null)
                        return;

                    Context.Dishes.Remove(SelectedDish);
                    Context.SaveChanges();

                    DishInfo.DishName = "";
                    DishInfo.Cost = 0;
                    DishInfo.DishCategory = null;
                }));

        private RelayCommand selectionDishCategory;
        public ICommand SelectionDishCategory =>
            selectionDishCategory ??
            (selectionDishCategory =
            new RelayCommand(
                () =>
                {
                    Dishes = ToObservableCollection(Context.Dishes.Where(w =>
                    w.DishCategory.DishCategoryId == SelectedDishCategory.DishCategoryId));
                }));
    }
}
