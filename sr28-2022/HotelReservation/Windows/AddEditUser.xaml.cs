using HotelReservation.Model;
using HotelReservation.Service;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddEditUser.xaml
    /// </summary>
    public partial class AddEditUser : Window
    {
        private UserService userService;
        private User contextUser;




        public AddEditUser(User user = null)
        {
            if (user == null)
            {
                contextUser = new User();
            }
            else
            {
                contextUser = user.Clone();
            }
            InitializeComponent();
            userService = new UserService();
            AdjustWindow(user);

            this.DataContext = contextUser;
        }

        private void AdjustWindow(User user = null)
        {
            UserTypeCB.Items.Add(typeof(Receptionist).Name);
            UserTypeCB.Items.Add(typeof(Administrator).Name);

            if (user != null)
            {
                Title = "Edit user";

                UserTypeCB.SelectedItem = user.UserType;
                UserTypeCB.IsEnabled = false;
            }
            else
            {
                Title = "Add user";
            }

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(contextUser.UserType))
            {
                MessageBox.Show("Fill required fields.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            else if (string.IsNullOrEmpty(contextUser.Username))
            {
                MessageBox.Show("Fill required fields.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            else if (string.IsNullOrEmpty(contextUser.Password))
            {
                MessageBox.Show("Fill required fields.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrEmpty(contextUser.JMBG))
            {
                MessageBox.Show("Fill required fields.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var uniqueJmbg= userService.GetAllActiveUsers().Any(r => r.JMBG == contextUser.JMBG);
            var uniqueUsername = userService.GetAllActiveUsers().Any(u => u.Username == contextUser.Username);
            var edituser = userService.GetAllActiveUsers().Any(u => u.Id == contextUser.Id);

            if (uniqueJmbg && !edituser)
            {
                MessageBox.Show("JMBG must be unique", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (uniqueUsername && !edituser)
            {
                MessageBox.Show("Username is already reserved", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            userService.SaveUser(contextUser);

            DialogResult = true;
            Close();
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
