﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Model
{
    [Serializable]
    public class Room
    {
        public int Id { get; set; }

        private string roomNumber = string.Empty;
        public string RoomNumber
        {
            get { return roomNumber; }
            set
            {
                //if (value == null || value == "")
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("It's required");
                }

                roomNumber = value;
            }
        }
        public bool HasTV { get; set; }
        public bool HasMiniBar { get; set; }
        public RoomType RoomType { get; set; }
        public bool IsActive { get; set; } = true;

        public override string ToString()
        {
            return $"{RoomNumber} with bed count {RoomType.BedCount}";
        }

        public Room Clone()
        {
            var clone = new Room();
            clone.Id = Id;
            clone.RoomNumber = RoomNumber;
            clone.HasTV = HasTV;
            clone.HasMiniBar = HasMiniBar;
            clone.RoomType = RoomType;
            clone.IsActive = IsActive;

            return clone;
        }
    }
}
