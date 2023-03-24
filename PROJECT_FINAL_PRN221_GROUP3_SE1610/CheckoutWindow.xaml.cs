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
    /// Interaction logic for CheckoutWindow.xaml
    /// </summary>

    public partial class CheckoutWindow : Window
    {
        shoppingMilkPrn221Context context;
        public CheckoutWindow()
        {
            InitializeComponent();
            context = new shoppingMilkPrn221Context();
            User user = context.Users.Where(u => u.Username == Settings.UserName).FirstOrDefault();
            txtUsername.Text = user.Username;
            txtFullname.Text = user.FullName;
            txtAddress.Text = user.Address;
            txtPhone.Text = user.Phone;
            txtTotal.Text = ShoppingCart.GetCart().GetTotal().ToString(".000.000 VNĐ");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            User user = context.Users.Where(u => u.Username == Settings.UserName).FirstOrDefault();
            txtUsername.Text = user.Username;
            Order order = new Order
            {
                Username = Settings.UserName,
                OrderDate = DateTime.Now,
                Address = txtAddress.Text,
                Phone = txtPhone.Text,
                UserId = user.UserId,
                Total = ShoppingCart.GetCart().GetTotal()

            };
            ShoppingCart cart = ShoppingCart.GetCart();
            int orderId = cart.CreateOrder(order);
            MessageBox.Show($"Đã đặt hàng thành công, OrderId của bạn là =  {orderId} ");
            this.Close();
        }
    }
}
