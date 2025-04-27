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
    public partial class RoomTypes : Window
    {
        private RoomTypeService roomTypeService;
        private ICollectionView view;

        public RoomTypes()
        {
            roomTypeService = new RoomTypeService();

            InitializeComponent();
            FillData();
        }

        // TODO: Korisničke lozinke ne bi trebalo prikazati
        private void FillData()
        {
            var roomTypes = roomTypeService.GetAllActiveRoomTypes();

            view = CollectionViewSource.GetDefaultView(roomTypes);

            RoomTypesDG.ItemsSource = null;
            RoomTypesDG.ItemsSource = view;
            RoomTypesDG.IsSynchronizedWithCurrentItem = true;
        }

        private void RoomTypesDG_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();

            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }

            if(headerName == "BedCount")
            {
                e.Column.Header = "Bed count";
            }

        }



        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addRoomTypeWindow = new AddEditRoomType();

            Hide();
            if (addRoomTypeWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRoomType = (RoomType)view.CurrentItem;

            if (selectedRoomType != null)
            {
                var editRoomTypeWindow = new AddEditRoomType(selectedRoomType);

                Hide();

                if (editRoomTypeWindow.ShowDialog() == true)
                {
                    FillData();
                }

                Show();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (view.CurrentItem == null) { return; }

            var selectedRoomType = view.CurrentItem as RoomType;

            if (MessageBox.Show($"Are you sure that you want to delete room type {selectedRoomType!.Name}?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                selectedRoomType.IsActive = false;
                FillData();
            }
            else
            {

            }
        }

    }
}
