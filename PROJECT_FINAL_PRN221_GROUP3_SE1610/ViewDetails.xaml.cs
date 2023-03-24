using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for ViewDetails.xaml
    /// </summary>
    public partial class ViewDetails : Window
    {
        shoppingMilkPrn221Context context = new shoppingMilkPrn221Context();
        int selectedOrderId;
        public ViewDetails()
        {
            InitializeComponent();
        }
        public ViewDetails(string orderId)
        {
            InitializeComponent();
            selectedOrderId = int.Parse(orderId);
            loadData();
        }

        private void loadData()
        {
            lvOrderDetail.ItemsSource = context.OrderDetails.Include(s => s.Milk).Where(s => s.OrderId.Equals(selectedOrderId)).ToList();
            
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
             var orderDetails =  lvOrderDetail.SelectedItem as OrderDetail;
             orderDetails.Quantity = int.Parse( txtQuantity.Text);
            context.Update(orderDetails);

            if (context.SaveChanges() > 0)
            {
                MessageBox.Show("Update Succesfull");
            }
            else
            {
                MessageBox.Show("Update Fail!");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var orderDetails = lvOrderDetail.SelectedItem as OrderDetail;
            if (orderDetails == null)
            {
                MessageBox.Show("Please choose order detail to delete !");
            }
            else
            {
                MessageBoxResult messageResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageResult == MessageBoxResult.No)
                {
                    return;
                }
                context.Remove(orderDetails);
                loadData();
                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("Delete Succesfull");
                }
                else
                {
                    MessageBox.Show("Delete Fail!");
                }
            }
            
        }

        private void lvOrderDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbTotal.Content = "Total of this Order Details: " + (int.Parse(txtPrice.Text) * (int.Parse(txtQuantity.Text))).ToString() + " $";
        }
    }
}
