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
    /// Interaction logic for Reservations.xaml
    /// </summary>
    public partial class Reservations : Window
    {
        private ReservationService reservationService;
        private ICollectionView view;
        public Reservations()
        {
            reservationService = new ReservationService();

            InitializeComponent();
            FillData();
        }

        private void FillData()
        {
            var reservations = reservationService.GetAllActiveReservations();

            view = CollectionViewSource.GetDefaultView(reservations);
            view.Filter = DoFilter;

            ReservationDG.ItemsSource = null;
            ReservationDG.ItemsSource = view;
            ReservationDG.IsSynchronizedWithCurrentItem = true;
        }

        private bool DoFilter(object reservationObject)
        {
            var reservation = reservationObject as Reservation;

            var ReservationSearchParam = ReservationSearchTB.Text;

            if (reservation.Room.RoomNumber.Contains(ReservationSearchParam))
            {
                return true;
            }

            return false;



        }



        private void ReservationDG_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }

            if (e.PropertyName.ToLower() == "Guests".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addReservationWindow = new AddEditReservation();

            Hide();
            if (addReservationWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedReservation = (Reservation)view.CurrentItem;

            if (selectedReservation != null)
            {
                var editReservationWindow = new AddEditReservation(selectedReservation);

                Hide();

                if (editReservationWindow.ShowDialog() == true)
                {
                    FillData();
                }

                Show();
            }
        }

        private void ReservationSearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (view.CurrentItem == null) { return; }

            var selectedReservation = view.CurrentItem as Reservation;

            if (MessageBox.Show($"Are you sure that you want to delete reservation?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                selectedReservation.IsActive = false;
                reservationService.SaveReservation(selectedReservation);
                
                FillData();
            }
        }



    }
}
