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
            try
            {
             
                Order order = new Order
                {
                   
                    OrderDate = dpOrderDate.SelectedDate.Value,
                    
                    Username = txtUsername.Text,
                    Address = txtAddress.Text,
                    Phone = txtPhone.Text,
                    Total = float.Parse(txtTotal.Text)
                };
                context.Orders.Add(order);
                int count = context.SaveChanges();
                if (count > 0)
                {
                    MessageBox.Show("Insert success");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Insert failure");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Insert error: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {             
                var order = lvOrder.SelectedItem as Order;
                if (order != null)
                {   
                    order.OrderDate = dpOrderDate.SelectedDate.Value;
                    order.Username = txtUsername.Text;
                    order.Address = txtAddress.Text;
                    order.Phone = txtPhone.Text;
                    order.Total = float.Parse(txtTotal.Text);
                    context.Orders.Update(order);
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show("Update success");
                        loadData();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Update error: " + ex.Message);
            }


        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var order = lvOrder.SelectedItem as Order;
            if (order != null)
            {
                MessageBoxResult messageResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageResult == MessageBoxResult.No)
                {
                    return;
                }
                try
                {
                    context.Orders.Remove(order);
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show("Delete success");
                        loadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please choose order to delele !");
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var listSearchUser = context.Orders.Where(o => o.Username.Contains(txtSearch.Text)).ToList();
            lvOrder.ItemsSource = listSearchUser;
            
        }

        private void btnViewDetail_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
                   
            var order = b.CommandParameter as Order;
            
            string orderId = order.OrderId.ToString();
            if (orderId!=null){
                ViewDetails viewDetails = new ViewDetails(orderId);
                viewDetails.ShowDialog();
            }
            else
            {
                MessageBox.Show("K co orderId");
            }

        }
    }
}
