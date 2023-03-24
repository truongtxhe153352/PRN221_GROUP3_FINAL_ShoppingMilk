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

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
