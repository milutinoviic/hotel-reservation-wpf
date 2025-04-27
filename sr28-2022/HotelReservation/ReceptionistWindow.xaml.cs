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
using System.Windows.Shapes;

namespace HotelReservation
{
    /// <summary>
    /// Interaction logic for ReceptionistWindow.xaml
    /// </summary>
    public partial class ReceptionistWindow : Window
    {
        public ReceptionistWindow()
        {
            InitializeComponent();
        }

        private void GuestsMI_Click(object sender, RoutedEventArgs e)
        {
            var guestsWindow = new Guests();
            guestsWindow.Show();
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
