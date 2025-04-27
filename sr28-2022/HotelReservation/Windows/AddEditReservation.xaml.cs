using HotelReservation.Model;
using HotelReservation.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for AddEditReservation.xaml
    /// </summary>
    public partial class AddEditReservation : Window
    {
        private ReservationService reservationService;

        private Reservation contextReservation;

        private GuestService guestService;   

        private Guest contextGuest;

        private ICollectionView guestsView;

        private PriceService priceService;
        public AddEditReservation(Reservation? reservation = null)
        {
            if (reservation == null)
            {
                contextReservation = new Reservation();
            }
            else
            {
                contextReservation = reservation.Clone();
            }
            contextGuest = new Guest();

            InitializeComponent();
            reservationService = new ReservationService();
            guestService = new GuestService();
            priceService = new PriceService();
            AdjustWindow(reservation);
            this.DataContext = contextReservation;

            if (contextReservation.Guests == null)
            {
                contextReservation.Guests = new List<Guest>{};
            }
            
            if(contextReservation.StartDateTime != default(DateTime))
            {
                StartDateDP.SelectedDate = contextReservation.StartDateTime.Date;
                EndDateDP.SelectedDate = contextReservation.EndDateTime.Date;
            }
            else
            {
                StartDateDP.DisplayDateStart = DateTime.Now;
                EndDateDP.DisplayDateStart = DateTime.Now;
            }
            GuestsDG.DataContext = contextReservation.Guests.Where(g => g.IsActive).ToList();
            this.FillGuestData();
            
        }

        public void FillGuestData()
        {
            guestsView = CollectionViewSource.GetDefaultView(contextReservation.Guests.Where(g => g.IsActive));
            GuestsDG.ItemsSource = null;
            GuestsDG.ItemsSource = guestsView;
            GuestsDG.DataContext = contextReservation.Guests.Where(g => g.IsActive).ToList();
        }

        public void AdjustWindow(Reservation? reservation = null)
        {
            if (contextReservation != null)
            {
                Title = "Edit Reservation";

            }
            else
            {

                Title = "Add Reservation";
            }
            var roomService = new RoomService();
            var rooms = roomService.GetAllActiveRooms();
            ReservationRoomsCB.ItemsSource = rooms;
            ReservationTypeCB.ItemsSource = Enum.GetValues(typeof(ReservationType));


        }




        private void SaveGuestBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GuestsDG.SelectedItem == null)
            {
                contextGuest = new Guest();
            }
            var name = GuestNameTB.Text;
            var surname = GuestSurnameTB.Text;
            var idNumber = GuestIDnumberTB.Text;
            if (string.IsNullOrWhiteSpace(name) ||
               string.IsNullOrWhiteSpace(surname) ||
               string.IsNullOrWhiteSpace(idNumber))
            {
                MessageBox.Show("All field has to be filled", "Validation error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }
            var isNewGuest = !contextReservation.Guests.Contains(contextGuest);
            if (contextReservation.Guests.Any(g => g.IDNumber == idNumber) && isNewGuest)
            {
                MessageBox.Show("id number has to be unique.", "Validation error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }

            contextGuest.Name = name;
            contextGuest.Surname = surname;
            contextGuest.IDNumber = idNumber;
            if (isNewGuest)
            {
                var newGuest = contextGuest.Clone();
                contextReservation.Guests.Add(newGuest);
                newGuest.reservation = contextReservation;
            }
            guestsView.Refresh();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (contextReservation.Guests.Count <= 0)
            {
                MessageBox.Show("At least one guest is required", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (contextReservation.Room == null)
            {
                MessageBox.Show("Room is not choosed", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var startDate = StartDateDP.SelectedDate.GetValueOrDefault();
            var endDate = EndDateDP.SelectedDate.GetValueOrDefault();
            if (startDate == default)
            {
                MessageBox.Show("start date is not choosed", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (endDate == default)
            {
                MessageBox.Show("end date is not choosed", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (startDate > endDate)
            {
                MessageBox.Show("end date mustn't be before start date", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            var reservationExist = reservationService.checkIfReservationAlreadyExistsByDateInterval(startDate, endDate, contextReservation.Room.Id);
            var reservationEdit = reservationService.GetAllActiveReservations().Any(r => r.Id == contextReservation.Id);
            if (reservationExist && !reservationEdit) 
            {

                MessageBox.Show("The reservation already exists for choosed dates", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            contextReservation.StartDateTime = startDate;
            contextReservation.EndDateTime = endDate;
            
            var totalPrice = priceService.CaculateTotalPrice(contextReservation);
            if (totalPrice == -1)
            {
                MessageBox.Show("Price for room is not defined", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            contextReservation.TotalPrice = totalPrice;
            reservationService.SaveReservation(contextReservation);

            foreach (Guest guest in contextReservation.Guests)
            {
                guestService.SaveGuest(guest);
            }
            
            DialogResult = true;
            Close();
        }

        private void GuestsDG_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            
            if (e.PropertyName.ToLower() == "Id".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }

            if (e.PropertyName.ToLower() == "reservation".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }

        }

        private void CancelGuestBtn_Click(object sender, RoutedEventArgs e)
        {
            this.contextGuest = new Guest();
            GuestsDG.SelectedItem = null;
            GuestNameTB.Text = "";
            GuestSurnameTB.Text = "";
            GuestIDnumberTB.Text = "";
            guestsView.Refresh();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void DeleteGuestBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedGuest = (Guest) GuestsDG.SelectedItem;
            if(selectedGuest == null )
            {
                return;
            }
            selectedGuest.IsActive = false;
            this.FillGuestData();
            guestsView.Refresh();
            
            
        }


        private void EditGuestBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedGuest = (Guest) GuestsDG.SelectedItem;

            if (selectedGuest == null)
                return;
            guestsView.Refresh();
            this.contextGuest = selectedGuest;
            GuestNameTB.Text = selectedGuest.Name;
            GuestSurnameTB.Text = selectedGuest.Surname;
            GuestIDnumberTB.Text = selectedGuest.IDNumber;
        }

        



    }

}

