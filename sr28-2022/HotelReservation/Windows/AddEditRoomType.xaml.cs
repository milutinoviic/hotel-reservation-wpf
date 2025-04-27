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
    public partial class AddEditRoomType : Window
    {
        private RoomTypeService roomTypeService;
        private RoomType contextRoomType;




        public AddEditRoomType(RoomType roomType = null)
        {
            if (roomType == null)
            {
                contextRoomType = new RoomType();
            }
            else
            {
                contextRoomType = roomType.Clone();
            }
            InitializeComponent();
            roomTypeService = new RoomTypeService();
            AdjustWindow(roomType);

            this.DataContext = contextRoomType;
        }

        public void AdjustWindow(RoomType? roomType = null)
        {
            if (roomType != null)
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

            if (string.IsNullOrEmpty(contextRoomType.Name)) 
            {
                MessageBox.Show("Fill required fields.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            roomTypeService.SaveRoomType(contextRoomType);

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
