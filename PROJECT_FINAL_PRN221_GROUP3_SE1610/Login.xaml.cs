﻿using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        shoppingMilkPrn221Context context = new shoppingMilkPrn221Context();

        private User getUser()
        {

            User user = new User
            {
                Username = txtUser.Text.Trim(),
                Passwork = txtPassword.Password.Trim()
            };

            if (user.Username == null || user.Passwork == null)
            {
                MessageBox.Show("Enter username and password!");
            }
            return user;
        }

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if (!AllowLogin()) return;

            try
            {
                string userName = txtUser.Text.Trim();
                string password = txtPassword.Password.Trim();

                User userForm = getUser();
                User userLogin = context.Users.Where(x => x.Username.Equals(userForm.Username)
                && x.Passwork.Equals(userForm.Passwork))
                    .Include(r => r.Roles)
                .FirstOrDefault();

        //        var userLogin = context.Users
        //.Include(u => u.Roles) // include the Roles navigation property
        //.FirstOrDefault(x => x.Username.Equals(userForm.Username)
        //        && x.Passwork.Equals(userForm.Passwork));


                if (userLogin == null)
                {
                    MessageBox.Show("Username or Password is incorrect!!!");
                }
                else
                {
                    List<Role> roles = userLogin.Roles.ToList();
                    foreach (Role role in roles) {
                        if (role.RoleName.Equals("user"))
                        {
                            MessageBox.Show("Login User successfully!!!!");
                            //Settings.UserName = userLogin.Name;
                            // Settings.Role = userLogin.Roles.;
                            MainWindow mainWindow = new MainWindow(userLogin);
                            mainWindow.Show();
                            this.Close();
                        } else if (role.RoleName.Equals("admin"))
                        {
                            MessageBox.Show("Login Admin successfully!!!!");
                            //Settings.UserName = userLogin.Name;
                            // Settings.Role = userLogin.Roles.;
                            MainWindow mainWindow = new MainWindow(userLogin);
                            mainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Eo co gi o day");
                        }
                    }
                        

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Username or Password is incorrect!!!");
            }
        }   
        

        private bool AllowLogin()
        {
            if(txtUser.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập thông tin tải khoản", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUser.Focus();
                return false;
            }
            if(txtPassword.Password.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin mật khẩu", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Focus();
                return false;
            }
            return true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}