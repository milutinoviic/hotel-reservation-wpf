﻿using HotelReservation.Model;
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
    public partial class AddEditGuest : Window
    {
        private GuestService guestService;
        private Guest contextGuest;




        public AddEditGuest(Guest guest = null)
        {
            if (guest == null)
            {
                contextGuest = new Guest();
            }
            else
            {
                contextGuest = guest.Clone();
            }
            InitializeComponent();
            guestService = new GuestService();
            AdjustWindow(guest);

            this.DataContext = contextGuest;
        }

        public void AdjustWindow(Guest? guest = null)
        {
            if (guest != null)
            {
                Title = "Edit Room";
            }
            else
            {
                Title = "Add Room";
            }

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(contextGuest.Name)) //mozda i Id
            {
                MessageBox.Show("Fill required fields.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            guestService.SaveGuest(contextGuest);

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
