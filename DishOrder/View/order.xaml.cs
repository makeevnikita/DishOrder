using DishOrder.DBContext;
using DishOrder.ViewModel;
using System.Windows.Controls;

namespace DishOrder.View
{
    /// <summary>
    /// Логика взаимодействия для order.xaml
    /// </summary>
    public partial class order : Page
    {
        private DishOrderContext Context { get; }

        public order()
        {
            InitializeComponent();
            Context = new DishOrderContext();
            DataContext = new OrderViewModel(Context);
        }
    }
}
