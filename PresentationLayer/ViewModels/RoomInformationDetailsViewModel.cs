﻿using BusinessServiceLayer.Interfaces;
using PresentationLayer.Commands;
using PresentationLayer.Models;
using PresentationLayer.Views;
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
        private readonly UpdateRoomInformationViewModel _updateRoomInformationViewModel;
        private readonly DeleteRoomInformationViewModel _deleteRoomInformationViewModel;

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
        private string _errorMessage;

        public RelayCommand ShowUpdateRoomInformationWindow { get; set; }
        public RelayCommand ShowDeleteRoomInformationWindow { get; set; }
        public RelayCommand AddToBasketCommand { get; set; }

        public UpdateRoomInformationPopupView updateRoomInformationWin;

        public DeleteRoomInformationPopupView deleteRoomInformationWin;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public RoomInformationDetailsViewModel(UpdateRoomInformationViewModel updateRoomInformationViewModel,
            DeleteRoomInformationViewModel deleteRoomInformationViewModel, IRoomService roomService)
        {
            _updateRoomInformationViewModel = updateRoomInformationViewModel;
            _deleteRoomInformationViewModel = deleteRoomInformationViewModel;
            ShowUpdateRoomInformationWindow = new RelayCommand(async o => await ShowUpdateWindow(o), o => true);
            ShowDeleteRoomInformationWindow = new RelayCommand(ShowDeleteWindow, o => true);
            _roomService = roomService;
            AddToBasketCommand = new RelayCommand(AddToBasket, CanExecuteAddToBasket);
        }

        private async Task ShowUpdateWindow(object roomId)
        {
            if (roomId != null)
            {
                await _updateRoomInformationViewModel.LoadRoomInformationDetail((int)roomId);
                updateRoomInformationWin = new UpdateRoomInformationPopupView(_updateRoomInformationViewModel);
                updateRoomInformationWin.Show();
            }
            else
            {
                MessageBox.Show("Room ID not found");
            }

        }

        private void ShowDeleteWindow(object roomId)
        {
            if (roomId != null)
            {
                _deleteRoomInformationViewModel.RoomID = (int)roomId;
                deleteRoomInformationWin = new DeleteRoomInformationPopupView(_deleteRoomInformationViewModel);
                deleteRoomInformationWin.Show();
            }
            else
            {
                MessageBox.Show("Room ID not found");
            }
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

        private bool CanExecuteAddToBasket(object obj)
        {
            if (StartDate > EndDate)
            {
                ErrorMessage = "Start Date cannot be after End Date";
                return false;
            }
            else
            {
                ErrorMessage = "";
                return true;
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
