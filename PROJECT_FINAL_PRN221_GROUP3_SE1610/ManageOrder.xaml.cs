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
    /// Interaction logic for ManageOrder.xaml
    /// </summary>
    public partial class ManageOrder : Window
    {
        shoppingMilkPrn221Context context = new shoppingMilkPrn221Context();
        public ManageOrder()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            lvOrder.ItemsSource = context.Orders.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var order = new Order();
            order.OrderDate = dpOrderDate.SelectedDate.Value;
            order.Username = txtUsername.Text;
            order.Address = txtAddress.Text;
            order.Phone = txtPhone.Text;
            order.Total = float.Parse(txtTotal.Text);

            var user = context.Users.Where(u => u.Username.Equals(txtUsername)).FirstOrDefault();
            order.UserId = user.UserId;
            
            context.Orders.Add(order); 

            if(context.SaveChanges() > 0)
            {
                MessageBox.Show("add successfully");
            }
            else
            {
                MessageBox.Show("add fail");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var order = lvOrder.SelectedItem as Order;

           
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var listSearchUser = context.Orders.Where(o => o.Username.Contains(txtSearch.Text)).ToList();
            lvOrder.ItemsSource = listSearchUser;
        }
    }
}
