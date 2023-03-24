using PROJECT_FINAL_PRN221_GROUP3_SE1610.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {

        shoppingMilkPrn221Context context = new shoppingMilkPrn221Context();

        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = new User();
                bool isDuplicate = context.Users.Any(x => x.Username == txtUsername.Text);

                if (isDuplicate)
                {
                    // Thông tin đã trùng lặp với thông tin trong cơ sở dữ liệu
                    MessageBox.Show("Register account : " + user.Username + "Exist. Please new usename!!!!!!");

                }
                else 
                {
                    string pattern = @"^(\+[0-9]{1,3}[- ]?)?([0-9]{10})$";
                    //int phone = int.Parse(txtPhone.Text);
                    string patternEmail = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";

                    DateTime selectedDate = dpkBirthDate.SelectedDate ?? DateTime.MaxValue;
                    int year = selectedDate.Year;
                    //&& year > 1900 || year < DateTime.Now.Year

                    if (txtPassword.Password == txtReEnterPassword.Password && !string.IsNullOrEmpty(txtUsername.Text)
                    && !string.IsNullOrEmpty(txtPhone.Text) && !string.IsNullOrEmpty(txtFullname.Text)
                    && !string.IsNullOrEmpty(txtPassword.Password) && dpkBirthDate.SelectedDate.HasValue
                    && !string.IsNullOrEmpty(txtAddress.Text) && Regex.IsMatch(txtPhone.Text, pattern)
                    && Regex.IsMatch(txtEmail.Text, patternEmail) )
                    {
                        // Thông tin không trùng lặp với thông tin trong cơ sở dữ liệu
                        // User user = new User();
                        user.Username = txtUsername.Text;
                        user.Passwork = txtPassword.Password;
                        user.Address = txtAddress.Text;
                        user.FullName = txtFullname.Text;
                        user.BirthDate = dpkBirthDate.SelectedDate;
                        if (rdoFemale.IsChecked == true)
                        {
                            user.Gender = "Female";
                        }
                        else
                        {
                            user.Gender = "Male";
                        }
                        user.Phone = txtPhone.Text;
                        user.Email = txtEmail.Text;
                        user.RoleId = 1;

                        context.Users.Add(user);

                        MessageBox.Show("Register account success: " + user.Username);

                        context.SaveChanges();
                        Login login = new Login();
                        login.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please. Check again information!!!");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert error: " + ex.Message);
            }
        }
    }
}
