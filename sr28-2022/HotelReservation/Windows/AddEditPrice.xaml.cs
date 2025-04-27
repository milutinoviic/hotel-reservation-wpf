using HotelReservation.Model;
using HotelReservation.Service;
using System;
using System.Collections.Generic;
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

    public partial class AddEditPrice : Window
    {
        private PriceService priceService;
        private Price contextPrice;




        public AddEditPrice(Price price = null)
        {
            if (price == null)
            {
                contextPrice = new Price();
            }
            else
            {
                contextPrice = price.Clone();
            }
            InitializeComponent();
            priceService = new PriceService();
            AdjustWindow(price);

            this.DataContext = contextPrice;
        }

        public void AdjustWindow(Price? price = null)
        {
            if (price != null)
            {
                Title = "Edit Room";
            }
            else
            {
                Title = "Add Room";
            }

            var roomTypeService = new RoomTypeService();
            var roomTypes = roomTypeService.GetAllActiveRoomTypes();
            RoomTypesCB.ItemsSource = roomTypes;

            var reservationTypes = Enum.GetValues(typeof(ReservationType));
            ReservationTypeCB.ItemsSource = reservationTypes;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var chc = priceService.GetAllActivePrices().Any(price => price.ReservationType == contextPrice.ReservationType && price.RoomType == contextPrice.RoomType);
            var chc2 = priceService.GetAllActivePrices().Any(p => p.Id == contextPrice.Id);
            if (chc && !chc2)
            {
                MessageBox.Show("Price already exists", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            priceService.SavePrice(contextPrice);

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
