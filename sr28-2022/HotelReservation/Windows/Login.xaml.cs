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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private UserService _userService;
        public Login()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void Login_Click(object sender, RoutedEventArgs e) 
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            var  loggedInUser = _userService.SearchUser(username, password);

            if (loggedInUser != null)
            {

                Hotel.GetInstance().currentlyLoggedInUser = loggedInUser;
                if(loggedInUser.UserType == "Administrator")
                {
                    var AdministratorWindow = new AdministratorWindow();
                    AdministratorWindow.Show();
                }
                else
                {
                    var receptionistWindow = new ReceptionistWindow();
                    receptionistWindow.Show();
                }

                this.Close();
                

            }

            else 
            {
                MessageBox.Show("Invalid username or password. Please try again.");

            }

        }
    }
}
