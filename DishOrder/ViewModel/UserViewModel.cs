using DishOrder.DBContext;
using DishOrder.Model;
using GalaSoft.MvvmLight.Command;
using System.Data.Entity;
using System.Windows.Input;

namespace DishOrder.ViewModel
{
    class UserViewModel : BaseViewModel
    {
        public DishOrderContext Context { get; }
        public User UserInfo { get; set; } = new User();
        public User SelectedUser { get; set; }

        public UserViewModel(DishOrderContext context)
        {
            Context = context;
            Context.Users.Load();
        }

        private RelayCommand updateUserCommand;
        public ICommand UpdateUserCommand =>
            updateUserCommand ??
            (updateUserCommand = new RelayCommand(
                () =>
                {
                    if (UserInfo.Name == "" || UserInfo.Surname == "" || UserInfo.Patronymic == "")
                        return;

                    SelectedUser.Name = UserInfo.Name;
                    SelectedUser.Surname = UserInfo.Surname;
                    SelectedUser.Patronymic = UserInfo.Patronymic;

                    Context.SaveChanges();
                }));

        private RelayCommand userSelectionCommand;
        public ICommand SelectionChangedCommand =>
            userSelectionCommand ??
            (userSelectionCommand =
                new RelayCommand(
                    () =>
                    {
                        if (SelectedUser != null)
                        {
                            UserInfo.Name = SelectedUser.Name;
                            UserInfo.Surname = SelectedUser.Surname;
                            UserInfo.Patronymic = SelectedUser.Patronymic;
                        }
                    }));

        private RelayCommand addUserCommand;
        public ICommand AddUserCommand =>
            addUserCommand ??
            (addUserCommand =
            new RelayCommand(
                () =>
                {
                    if (UserInfo.Name == "" || UserInfo.Surname == "" || UserInfo.Patronymic == "")
                        return;

                    User user = new User
                    {
                        Name = UserInfo.Name,
                        Surname = UserInfo.Surname,
                        Patronymic = UserInfo.Patronymic
                    };

                    Context.Users.Add(user);
                    Context.SaveChanges();

                    SelectedUser = user;
                }));

        private RelayCommand removeUserCommand;
        public ICommand RemoveUserCommand =>
            removeUserCommand ??
            (removeUserCommand =
            new RelayCommand(
                () =>
                {
                    if (SelectedUser != null)
                    {
                        Context.Users.Remove(SelectedUser);
                        Context.SaveChanges();

                        UserInfo.Name = "";
                        UserInfo.Surname = "";
                        UserInfo.Patronymic = "";
                    }
                }));
    }
}
