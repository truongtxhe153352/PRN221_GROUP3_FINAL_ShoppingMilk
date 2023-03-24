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
    /// Interaction logic for ManageBrand.xaml
    /// </summary>
    public partial class ManageBrand : Window
    {
        shoppingMilkPrn221Context context = new shoppingMilkPrn221Context();
        public ManageBrand()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            lvBrand.ItemsSource = context.Brands.ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var listSearchBrand = context.Brands.Where(o => o.BrandName.Contains(txtSearch.Text)).ToList();
            lvBrand.ItemsSource = listSearchBrand;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBrandName.Text))
                {
                    var brand = new Brand();
                    brand.BrandName = txtBrandName.Text;
                    context.Add(brand);
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show("add successfully");
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show("add fail");
                    }
                }
                else
                {
                    MessageBox.Show("Brand Name is empty");
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBrandName.Text))
                {
                    var brand = lvBrand.SelectedItem as Brand;
                    brand.BrandName = txtBrandName.Text;
                    context.Update(brand);
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show("Update successfully");
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show("Update fail");
                    }
                }
                else
                {
                    MessageBox.Show("Brand Name is empty");
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var brand = lvBrand.SelectedItem as Brand;
                if (brand != null)
                {
                    MessageBoxResult messageResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageResult == MessageBoxResult.No)
                    {
                        return;
                    }
                    try
                    {
                        context.Brands.Remove(brand);
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
            }
            catch(Exception ex)
            {

            }
        }
    }
}
