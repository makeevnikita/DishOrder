using DishOrder.DBContext;
using DishOrder.View;
using System.Linq;
using System.Windows;

namespace DishOrder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            using (DishOrderContext Context = new DishOrderContext())
            {
                if (Context.Tables.Count() == 0)
                {
                    var tables = new[]
                    {
                        new Model.Table { TableName="Стол 1" },
                        new Model.Table { TableName="Стол 2" },
                        new Model.Table { TableName="Стол 3" },
                        new Model.Table { TableName="Стол 4" },
                        new Model.Table { TableName="Стол 5" },
                        new Model.Table { TableName="Стол 6" },
                        new Model.Table { TableName="Стол 7" }
                    };

                    Context.Tables.AddRange(tables);
                    Context.SaveChanges();
                }

                if (Context.DishCategories.Count() == 0)
                {
                    var dishCategories = new[]
                    {
                    new Model.DishCategory { CategoryName="Мясные" },
                    new Model.DishCategory { CategoryName="Каши" },
                    new Model.DishCategory { CategoryName="Супы" },
                    new Model.DishCategory { CategoryName="Напитки" },
                    new Model.DishCategory { CategoryName="Десерты" }
                };

                    Context.DishCategories.AddRange(dishCategories);
                    Context.SaveChanges();
                }
            }

            InitializeComponent();

            frame.Navigate(new order());
        }

        private void Dishes(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new dish());
        }

        private void Orders(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new order());
        }

        private void Users(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new user());
        }
    }
}
