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

                Role role = new Role();
                role.RoleName = "user";

                context.Roles.Add(role);
                context.Users.Add(user);
                MessageBox.Show("Register account success: " + user.Username);

                context.SaveChanges();
                Login login = new Login();
                login.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert error: " + ex.Message);
            }
        }
    }
}
