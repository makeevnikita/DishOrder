namespace DishOrder.Model
{
    public class User : BaseViewModel
    {
        private int userId;
        private string name;
        private string surname;
        private string patronymic;

        public int UserId
        {
            get => userId;
            set
            {
                if (value == userId) return;
                userId = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                if (value == surname) return;
                surname = value;
                OnPropertyChanged();
            }
        }

        public string Patronymic
        {
            get => patronymic;
            set
            {
                if (value == patronymic) return;
                patronymic = value;
                OnPropertyChanged();
            }
        }
    }
}
