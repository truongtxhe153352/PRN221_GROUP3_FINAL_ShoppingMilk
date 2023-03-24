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
    /// Interaction logic for ManageCategory.xaml
    /// </summary>
    public partial class ManageCategory : Window
    {
        shoppingMilkPrn221Context context = new shoppingMilkPrn221Context();
        public ManageCategory()
        {
            InitializeComponent();
            loadData();
        }
        private void loadData()
        {
            lvCate.ItemsSource = context.Categories.Include(c => c.Brand).ToList();
            cbBrand.ItemsSource = context.Brands.ToList().Select(r => r.BrandName).ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var listSearchCate = context.Categories.Where(o => o.Name.Contains(txtSearch.Text)).Include(c => c.Brand).ToList();
            lvCate.ItemsSource = listSearchCate;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCateName.Text) || cbBrand.SelectedIndex == null)
                {
                    MessageBox.Show("Add faild");
                    return;
                }
                Brand brand = context.Brands.Where(b => b.BrandName == cbBrand.Text).FirstOrDefault();

                Category cate = new Category();
                cate.Name = txtCateName.Text;
                cate.BrandId = brand.BrandId;
                context.Categories.Add(cate);
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
            catch (Exception ex)
            {
                MessageBox.Show("add fail");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCateName.Text) || cbBrand.SelectedIndex == null)
                {
                    MessageBox.Show("Update faild");
                    return;
                }
                Brand brand = context.Brands.Where(b => b.BrandName == cbBrand.Text).FirstOrDefault();
                Category cate = lvCate.SelectedItem as Category;
                cate.Name = txtCateName.Text;
                cate.BrandId = brand.BrandId;
                context.Categories.Update(cate);
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
            catch (Exception ex)
            {
                MessageBox.Show("Update fail");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cate = lvCate.SelectedItem as Category;
                if (cate != null)
                {
                    MessageBoxResult messageResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageResult == MessageBoxResult.No)
                    {
                        return;
                    }
                    try
                    {
                        context.Categories.Remove(cate);
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
            catch (Exception ex)
            {

            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
            this.Close();
        }
    }
}
