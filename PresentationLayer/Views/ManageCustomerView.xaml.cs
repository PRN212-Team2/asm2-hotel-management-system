﻿using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Services;
using PresentationLayer.ViewModels;
using RepositoryLayer.Repositories;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class ManageCustomerView : UserControl
    {
        private readonly ICustomerService _customerService;

        public ManageCustomerView()
        {
            InitializeComponent();
        }

        private void updateCustomerPopup_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.CommandParameter is int customerId)
                {
                    int id = customerId;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when attempt to update customer info");
            }
        }

        private void deleteCustomerPopup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.CommandParameter is int customerId)
                {
                    int id = customerId;
                    DeleteCustomerPopupView popup = new DeleteCustomerPopupView(_customerService, this, id);
                    popup.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when attempt to delete a customer");
            }
        }
    }
}
