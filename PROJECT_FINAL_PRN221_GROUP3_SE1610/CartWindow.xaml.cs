using PROJECT_FINAL_PRN221_GROUP3_SE1610.Models;
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
            txtTotal.Text = cart.GetTotal().ToString(".00");
            btnCheckout.IsEnabled = !string.IsNullOrEmpty(Settings.UserName) && cart.GetTotal() > 0;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
