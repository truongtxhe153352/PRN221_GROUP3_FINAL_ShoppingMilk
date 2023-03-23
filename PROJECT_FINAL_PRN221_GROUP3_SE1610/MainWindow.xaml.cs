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
                bindingUser(user1);
            }
           // bindingCart();
        }

        private void bindingUser(User user)
        {
            userLogin = user;
            btnLogin.Content = "Logout(" + user.Username + ")";
            //if (user.Role == 1)
            //{
            //    btnAlbums.IsEnabled = true;
            //}
            //if (Settings.CartId != null)
            //{
            //    List<Cart> listCarts = context.Carts.Where(x => x.CartId == Settings.CartId).ToList();
            //    for (int i = 0; i < listCarts.Count; i++)
            //    {
            //        Cart cart = context.Carts.Where(x => x.RecordId == listCarts[i].RecordId).FirstOrDefault();
            //        cart.CartId = userLogin.UserName;
            //        context.Carts.Update(cart);
            //        context.SaveChanges();
            //    }
            //}
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
    }
}
