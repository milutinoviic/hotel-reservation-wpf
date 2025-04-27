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
    public partial class Guests : Window
    {
        private GuestService guestService;
        private ICollectionView view;

        public Guests()
        {
            guestService = new GuestService();

            InitializeComponent();
            FillData();
        }

        // TODO: Korisničke lozinke ne bi trebalo prikazati
        private void FillData()
        {
            var guests = guestService.GetAllActiveGuests();

            view = CollectionViewSource.GetDefaultView(guests);

            GuestsDG.ItemsSource = null;
            GuestsDG.ItemsSource = view;
            GuestsDG.IsSynchronizedWithCurrentItem = true;
        }

        private void GuestsDG_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }

            if (e.PropertyName.ToLower() == "reservation".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            if (headername == "IDNumber")
            {
                e.Column.Header = "Id number";
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addGuestWindow = new AddEditGuest();

            Hide();
            if (addGuestWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedGuest = (Guest)view.CurrentItem;

            if (selectedGuest != null)
            {
                var editGuestWindow = new AddEditGuest(selectedGuest);

                Hide();

                if (editGuestWindow.ShowDialog() == true)
                {
                    FillData();
                }

                Show();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (view.CurrentItem == null) { return; }

            var selectedGuest = view.CurrentItem as Guest;

            if (MessageBox.Show($"Are you sure that you want to delete Guest {selectedGuest!.Name}?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                selectedGuest.IsActive = false;
                FillData();
            }
            else
            {

            }
        }

    }
}
