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
    public partial class Prices : Window
    {
        private PriceService priceService;
        private ICollectionView view;

        public Prices()
        {
            priceService = new PriceService();

            InitializeComponent();
            FillData();
        }

        // TODO: Korisničke lozinke ne bi trebalo prikazati
        private void FillData()
        {
            var guests = priceService.GetAllActivePrices();

            view = CollectionViewSource.GetDefaultView(guests);

            PricesDG.ItemsSource = null;
            PricesDG.ItemsSource = view;
            PricesDG.IsSynchronizedWithCurrentItem = true;
        }
        private void PricesDG_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            
            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            if (headername == "RoomType")
            {
                e.Column.Header = "Room type";
            }
            if (headername == "ReservationType")
            {
                e.Column.Header = "Reservation type";
            }
            if (headername == "PriceValue")
            {
                e.Column.Header = "Price value";
            }
        }


        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addPriceWindow = new AddEditPrice();

            Hide();
            if (addPriceWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedPrice = (Price)view.CurrentItem;

            if (selectedPrice != null)
            {
                var editPriceWindow = new AddEditPrice(selectedPrice);

                Hide();

                if (editPriceWindow.ShowDialog() == true)
                {
                    FillData();
                }

                Show();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (view.CurrentItem == null) { return; }

            var selectedPrice = view.CurrentItem as Price;

            if (MessageBox.Show($"Are you sure that you want to delete Guest {selectedPrice!.PriceValue}?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                selectedPrice.IsActive = false;
                FillData();
            }
            else
            {

            }
        }

    }
}
