using DishOrder.DBContext;
using DishOrder.ViewModel;
using System.Windows.Controls;

namespace DishOrder.View
{
    /// <summary>
    /// Логика взаимодействия для dish.xaml
    /// </summary>
    public partial class dish : Page
    {
        private DishOrderContext Context { get; }

        public dish()
        {
            InitializeComponent();
            Context = new DishOrderContext();
            DataContext = new DishViewModel(Context);
        }

        private void Money(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Cost.Text == ".")
            {
                Cost.Text = "";
            }

            if (e.Text == "." && Cost.Text.Length == 0)
            {
                e.Handled = true;
                return;
            }

            if (Cost.Text.Contains(".") && e.Text == ".")
            {
                e.Handled = true;
                return;
            }

            if (!char.IsDigit(e.Text, 0) && e.Text != ".")
            {
                e.Handled = true;
                return;
            }
        }
    }
}
