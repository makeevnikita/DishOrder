using DishOrder.DBContext;
using DishOrder.ViewModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace DishOrder.View
{
    /// <summary>
    /// Логика взаимодействия для user.xaml
    /// </summary>
    public partial class user : Page
    {
        DishOrderContext Context { get; }
        public user()
        {
            InitializeComponent();
            Context = new DishOrderContext();
            DataContext = new UserViewModel(Context);
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[+_\\><,.'~`!@#$%^&*()?]");

            if (char.IsDigit(e.Text, 0) || regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }
    }
}
