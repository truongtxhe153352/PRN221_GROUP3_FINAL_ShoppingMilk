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
    /// Interaction logic for MyProfile.xaml
    /// </summary>
    public partial class MyProfile : Window
    {
        shoppingMilkPrn221Context context = new shoppingMilkPrn221Context();

        private User userLogin;

        public MyProfile()
        {
            InitializeComponent();
        }

        public MyProfile(User user)
        {
            InitializeComponent();
            if (Settings.UserName != null)
            {
                User user1 = context.Users.Where(X => X.Username == Settings.UserName).FirstOrDefault();
                bindingUser(user1);
                txtUserName.Text = userLogin.Username;
                txtEmail.Text = userLogin.Email;
                txtFullName.Text = userLogin.FullName;
                txtPhone.Text = userLogin.Phone;
                txtAddress.Text = userLogin.Address;
                dpkDateBirth.SelectedDate = userLogin.BirthDate;
                if(rdoMale.IsChecked == true)
                {
                    rdoMale.IsChecked = true;
                }
                else
                {
                    rdoFemale.IsChecked = true;
                }
            }
            // bindingCart();
        }

        private void bindingUser(User user)
        {
            userLogin = user;
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Settings.UserName != null)
                {

                         User user = context.Users.Where(X => X.Username == Settings.UserName).FirstOrDefault();
                        // bindingUser(user);
                        //User user = new User();

                        user.Username = txtUserName.Text;
                        user.FullName = txtFullName.Text;
                        if (rdoFemale.IsChecked == true)
                        {
                            user.Gender = "Female";
                        }
                        else
                        {
                            user.Gender = "Male";
                        }
                        user.BirthDate = dpkDateBirth.SelectedDate;
                        user.Phone = txtPhone.Text;
                        user.Email = txtEmail.Text;
                        user.Address = txtAddress.Text;

                        context.Users.Update(user);
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show($"{user.Username} Update success");
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
