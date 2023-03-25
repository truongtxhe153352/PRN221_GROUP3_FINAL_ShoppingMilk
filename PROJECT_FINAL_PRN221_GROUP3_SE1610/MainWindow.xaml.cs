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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User userLogin;

        bool isLoggedIn = false;


        shoppingMilkPrn221Context context = new shoppingMilkPrn221Context();

        public MainWindow()
        {
            InitializeComponent();
            if (Settings.UserName != null)
            {
                User user = context.Users.Where(X => X.Username == Settings.UserName).FirstOrDefault();
                bindingUser(user);
            }
        }


        public MainWindow(User user)
        {
            InitializeComponent();
            if (Settings.UserName != null)
            {
                User user1 = context.Users.Where(X => X.Username == Settings.UserName).FirstOrDefault();
                isLoggedIn = true;
                btnMyProfile.Visibility = Visibility.Visible;
                btnManagerUser.Visibility = Visibility.Visible;
                btnManagerOrder.Visibility = Visibility.Visible;
                btnManagerMilk.Visibility = Visibility.Visible;
                bindingUser(user1);
            }
            // bindingCart();
        }

        private void bindingUser(User user)
        {
            userLogin = user;
            btnLogin.Content = "Logout(" + user.Username + "))";
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (userLogin == null)
            {
                Login loginWindow = new Login();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Settings.UserName = null;
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
            }
        }


        private void btnMyProfile_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.UserName != null)
            {
                User user1 = context.Users.Where(X => X.Username == Settings.UserName).FirstOrDefault();
                isLoggedIn = true;
                btnMyProfile.Visibility = Visibility.Visible;
                bindingUser(user1);
                MyProfile myProfile = new MyProfile(user1);
                myProfile.Show();
            }
        }
            
        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindows = new CartWindow();
            cartWindows.ShowDialog();
            this.Close();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow home = new HomeWindow();
            home.Show();
            this.Close();
        }

        private void btnManagerUser_Click(object sender, RoutedEventArgs e)
        {
            ManagerUser mU = new ManagerUser();
            mU.ShowDialog();
            this.Close();
        }

        private void btnManagerOrder_Click(object sender, RoutedEventArgs e)
        {
            ManageOrder manageOrder = new ManageOrder();
            manageOrder.ShowDialog();
        }

        private void btnManagerMilk_Click(object sender, RoutedEventArgs e)
        {
            ManagerMilk managerMilk = new ManagerMilk();
            managerMilk.ShowDialog();
        }
    }
}
