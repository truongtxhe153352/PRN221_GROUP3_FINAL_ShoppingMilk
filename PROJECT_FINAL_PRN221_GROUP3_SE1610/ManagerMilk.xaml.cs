using Microsoft.Win32;
using PROJECT_FINAL_PRN221_GROUP3_SE1610.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ManagerMilk.xaml
    /// </summary>
    public partial class ManagerMilk : Window
    {
        shoppingMilkPrn221Context context = new shoppingMilkPrn221Context();

        public ManagerMilk()
        {
            InitializeComponent();
            lvMilk.ItemsSource = context.Milk.ToList();
            cbCategory.ItemsSource = context.Categories.ToList().Select(r => r.Name).ToList();
        }

        private void Button_Refresh(object sender, RoutedEventArgs e)
        {
            txtMilkName.Clear();
            txtUrl.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            dpkDate.SelectedDate = null;
            txtDescription.Clear();
            cbCategory.SelectedIndex = -1;
        }

        private void clearData()
        {
            txtMilkName.Clear();
            txtUrl.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            dpkDate.SelectedDate = null;
            txtDescription.Clear();
            cbCategory.SelectedIndex = -1;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Milk milk = new Milk();
                Category cate = context.Categories.SingleOrDefault(c => c.Name.Equals(cbCategory.Text));
                milk.CateId = cate.CategoryId;
                milk.Name = txtMilkName.Text;
                milk.Published = dpkDate.SelectedDate;
                milk.Decription = txtDescription.Text;
                milk.Price = double.Parse(txtPrice.Text);
                milk.Quantity = long.Parse(txtQuantity.Text);
                //insert image
                string workingDirectory = Environment.CurrentDirectory;
                string imageSaveDestination = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                string filePath = txtUrl.Text;
                string fileName = System.IO.Path.GetFileName(filePath);
                milk.ImageUrl = "/Images/" + fileName;
                Uri fileUri = new Uri(filePath);
                System.IO.File.Copy(filePath, imageSaveDestination.ToString() + "//Images//"
                    + fileName, true);
                context.Milk.Add(milk);
                context.SaveChanges();
                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("Insert milk successfull");
                }
                LoadMilk();
                clearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var milk = lvMilk.SelectedItem as Milk;
            if (milk != null)
            {
                MessageBoxResult messageResult = System.Windows.MessageBox.Show($"Are you sure delete { milk.Name}?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageResult == MessageBoxResult.No)
                {
                    return;
                }
                try
                {
                    context.Milk.Remove(milk);
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show($"{milk.Name} Delete success");
                        LoadMilk();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Have error here: " + ex.Message);
                }
            }
        }

        private void LoadMilk()
        {
            lvMilk.ItemsSource = context.Milk.ToList();
            cbCategory.ItemsSource = context.Categories.ToList().Select(r => r.Name).ToList();
        }

        private void btnUrl_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*jpg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                txtUrl.Text = openFileDialog.FileName;
                imagePicture.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Milk select = lvMilk.SelectedItem as Milk;
            Milk milk = context.Milk.SingleOrDefault(milk => milk.MilkId == select.MilkId);
            Category cate = context.Categories.SingleOrDefault(c => c.Name.Equals(cbCategory.Text));
            //test
            if (milk != null && cate != null)
            {
                milk.CateId = cate.CategoryId;
                milk.Name = txtMilkName.Text;
                milk.Published = dpkDate.SelectedDate;
                milk.Decription = txtDescription.Text;
                milk.Price = double.Parse(txtPrice.Text);
                milk.Quantity = long.Parse(txtQuantity.Text);
                //insert into folder project
                string workingDirectory = Environment.CurrentDirectory;
                string imageSaveDestination = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                string filePath = txtUrl.Text;
                if (!string.IsNullOrEmpty(filePath) && !filePath.StartsWith("/Images"))
                {
                    string fileName = System.IO.Path.GetFileName(filePath);
                    Uri fileUri = new Uri(filePath);
                    System.IO.File.Copy(filePath, imageSaveDestination.ToString() + "//Images//"
                        + fileName, true);
                    milk.ImageUrl = "/Images/" + fileName;
                }
                context.Milk.Update(milk);
                context.SaveChanges();
                MessageBox.Show("Update milk successfull");
                LoadMilk();
                clearData();
            }
        }

        private void lvMilk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Milk select = lvMilk.SelectedItem as Milk;
            txtUrl.Text = select.ImageUrl;
        }
    }
}
