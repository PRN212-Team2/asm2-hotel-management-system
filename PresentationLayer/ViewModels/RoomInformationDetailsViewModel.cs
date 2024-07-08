﻿using BusinessServiceLayer.Interfaces;
using PresentationLayer.Commands;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class RoomInformationDetailsViewModel : ViewModelBase 
    {
        private readonly IRoomService _roomService;
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDetailDescription { get; set; }
        public int RoomMaxCapacity { get; set; }
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public string TypeDescription { get; set; }
        public string TypeNote { get; set; }
        public bool RoomStatus { get; set; }
        public decimal RoomPricePerDay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public RelayCommand AddToBasketCommand { get; set; }

        public RoomInformationDetailsViewModel(IRoomService roomService)
        {
            _roomService = roomService;
            AddToBasketCommand = new RelayCommand(AddToBasket, o => true);
        }
        
        public async Task LoadRoomDetails(int roomId)
        {
            var room = await _roomService.GetRoomByIdWithTypeAsync(roomId);
            if (room != null) 
            {
                RoomID = room.RoomID;
                RoomNumber = room.RoomNumber;
                RoomDetailDescription = room.RoomDetailDescription;
                RoomMaxCapacity = room.RoomMaxCapacity;
                RoomTypeID = room.RoomTypeID;
                RoomTypeName = room.RoomTypeName;
                TypeDescription = room.TypeDescription;
                TypeNote = room.TypeNote;
                RoomStatus = room.RoomStatus;
                RoomPricePerDay = room.RoomPricePerDay;
            }
            else
            {
                MessageBox.Show("Room not found!");
            }
            
        }

        private void AddToBasket(object obj)
        {
            BasketItem basketItem = new BasketItem()
            {
                RoomID = this.RoomID,
                RoomNumber = this.RoomNumber,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                Price = this.RoomPricePerDay
            };
            BasketManager.AddBasketItem(basketItem);
        }
    }
}
