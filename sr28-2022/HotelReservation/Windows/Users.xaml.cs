using HotelReservation.Model;
using HotelReservation.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelReservation.Windows
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        private UserService userService;
        private ICollectionView view;

        public Users()
        {
            userService = new UserService();

            InitializeComponent();
            FillData();
        }

        // TODO: Korisničke lozinke ne bi trebalo prikazati
        private void FillData()
        {
            var users = userService.GetAllActiveUsers();

            view = CollectionViewSource.GetDefaultView(users);
            view.Filter = DoFilter;

            UsersDG.ItemsSource = null;
            UsersDG.ItemsSource = view;
            UsersDG.IsSynchronizedWithCurrentItem = true;

            
        }

        private bool DoFilter(object userObject)
        {
            var user = userObject as User; 

            var UsernameSearchParam = UsernameSearchTB.Text;

            if (user.Username.Contains(UsernameSearchParam))
            {
                return true;
            }

            return false;

            

        }



        private void UsersDG_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addUserWindow = new AddEditUser();

            Hide();
            if (addUserWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = (User)view.CurrentItem;

            if (selectedUser != null)
            {
                var editUserWindow = new AddEditUser(selectedUser);

                Hide();

                if (editUserWindow.ShowDialog() == true)
                {
                    FillData();
                }

                Show();
            }
        }

        private void UsernameSearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (view.CurrentItem == null) { return; }

            var selectedUser = view.CurrentItem as User;

            if (MessageBox.Show($"Are you sure that you want to delete User {selectedUser!.Name}?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                selectedUser.IsActive = false;
                FillData();
            }
        }
        private void UsernameSearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
