﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Models
{
    public class RoomInformation
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDetailDescription { get; set;}
        public int RoomMaxCapacity { get; set; }
        public int RoomTypeID { get; set; }
        public RoomType RoomType { get; set; }
        public bool RoomStatus { get; set; }
        public decimal RoomPricePerDay { get; set; }
    }
}
