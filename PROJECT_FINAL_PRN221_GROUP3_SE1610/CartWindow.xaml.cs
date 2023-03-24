﻿using PROJECT_FINAL_PRN221_GROUP3_SE1610.Models;
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

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        shoppingMilkPrn221Context context;

        ShoppingCart cartMilks = ShoppingCart.GetCart();

        public CartWindow()
        {
            InitializeComponent();

            context = new shoppingMilkPrn221Context();

            ShoppingCart cart = ShoppingCart.GetCart();
            lvCart.ItemsSource = cart.GetCartItems();
            txtTotal.Text = cart.GetTotal().ToString(".000.000 VNĐ");
            btnCheckout.IsEnabled = !string.IsNullOrEmpty(Settings.UserName) && cart.GetTotal() > 0;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Cart c = b.CommandParameter as Cart;
            cartMilks.RemoveFromCart(c.RecordId);
            lvCart.ItemsSource = cartMilks.GetCartItems();
            btnCheckout.IsEnabled = !string.IsNullOrEmpty(Settings.UserName) && cartMilks.GetTotal() > 0;
            txtTotal.Text = cartMilks.GetTotal().ToString(".000.000 VNĐ");
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            CheckoutWindow checkoutWindow = new CheckoutWindow();
            checkoutWindow.ShowDialog();
            this.Close();
        }
    }
}