using HotelReservation.Model;
using HotelReservation.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelReservation
{

    /// <summary>
    /// Interaction logic for AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        public AdministratorWindow()
        {
            InitializeComponent();
        }

        private void RoomsMI_Click(object sender, RoutedEventArgs e)
        {
            var roomsWindow = new Rooms();
            roomsWindow.Show();
        }

        private void UsersMI_Click(object sender, RoutedEventArgs e)
        {
            var usersWindow = new Users();
            usersWindow.Show();
        }

        private void GuestsMI_Click(object sender, RoutedEventArgs e)
        {
            var guestsWindow = new Guests();
            guestsWindow.Show();
        }

        private void RoomTypesMI_Click(object sender, RoutedEventArgs e)
        {
            var roomTypesWindow = new RoomTypes();
            roomTypesWindow.Show();
        }

        private void PricesTypesMI_Click(object sender, RoutedEventArgs e)
        {
            var priceWindow = new Prices();
            priceWindow.Show();
        }

        private void ReservationsTypeMI_Click(object sender, RoutedEventArgs e)
        {
            var reservationWindow = new Reservations();
            reservationWindow.Show();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e) 
        {
            Hotel.GetInstance().Logout();

            var loginWindow = new Login();
            loginWindow.Show();

            Close();


        }
    }
}
