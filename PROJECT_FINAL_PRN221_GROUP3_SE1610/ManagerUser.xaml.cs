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
    /// Interaction logic for ManagerUser.xaml
    /// </summary>
    public partial class ManagerUser : Window
    {
        shoppingMilkPrn221Context context = new shoppingMilkPrn221Context();
        public ManagerUser()
        {
            InitializeComponent();
            loadData();
        }
        private void loadData()
        {
            lvUser.ItemsSource = context.Users.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUsername.Text)
                || !string.IsNullOrEmpty(txtPassword.Text)
                || !string.IsNullOrEmpty(txtFullname.Text)
                || !string.IsNullOrEmpty(txtAddress.Text)
                || !string.IsNullOrEmpty(txtEmail.Text)
                || !string.IsNullOrEmpty(txtPhone.Text)
                || !string.IsNullOrEmpty(txtGender.Text)
                || dpBirthDate.SelectedDate == null)
                {
                    MessageBox.Show("Add faild");
                    return;
                }
                User user = new User();
                user.Username = txtUsername.Text;
                user.Passwork = txtPassword.Text;
                user.Address = txtAddress.Text;
                user.Email = txtEmail.Text;
                user.Phone = txtPhone.Text;
                user.Gender = txtGender.Text;
                user.BirthDate = dpBirthDate.SelectedDate;
                context.Users.Add(user);
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
            catch(Exception ex)
            {
                MessageBox.Show("add fail");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (!string.IsNullOrEmpty(txtUsername.Text)
                || !string.IsNullOrEmpty(txtPassword.Text)
                || !string.IsNullOrEmpty(txtFullname.Text)
                || !string.IsNullOrEmpty(txtAddress.Text)
                || !string.IsNullOrEmpty(txtEmail.Text)
                || !string.IsNullOrEmpty(txtPhone.Text)
                || !string.IsNullOrEmpty(txtGender.Text)
                || dpBirthDate.SelectedDate == null)
                {
                    MessageBox.Show("Edit faild");
                    return;
                }
                var user = lvUser.SelectedItem as User;
                user.Username = txtUsername.Text;
                user.Passwork = txtPassword.Text;
                user.Address = txtAddress.Text;
                user.Email = txtEmail.Text;
                user.Phone = txtPhone.Text;
                user.Gender = txtGender.Text;
                user.BirthDate = dpBirthDate.SelectedDate;
                context.Users.Update(user);
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
                var user = lvUser.SelectedItem as User;
                if (user != null)
                {
                    MessageBoxResult messageResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageResult == MessageBoxResult.No)
                    {
                        return;
                    }
                    try
                    {
                        context.Users.Remove(user);
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

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = lvUser.SelectedItem as User;
                if (user != null)
                {
                    MessageBoxResult messageResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageResult == MessageBoxResult.No)
                    {
                        return;
                    }
                    try
                    {
                        context.Users.Remove(user);
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var listSearchUser = context.Users.Where(o => o.Username.Contains(txtSearch.Text)).ToList();
            lvUser.ItemsSource = listSearchUser;
        }
    }
}
