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
    public partial class HomeWindow : Window
    {
        shoppingMilkPrn221Context context = new shoppingMilkPrn221Context();
        int previous = 1;
        int next = 0;
        public HomeWindow()
        {
            InitializeComponent();
            List<Milk> milks = context.Milk.ToList();
            BindingSelection();
            next = previous+1;
            bindGridFilter(1, 0, 0, "");
        }

        public void BindingSelection()
        {
            List<Category> forFilterCate = context.Categories.ToList();
            Category category = new Category();
            category.Name = "All";
            forFilterCate.Insert(0, category);
            cbCategory.ItemsSource = forFilterCate;
            cbCategory.SelectedIndex = 0;


            List<Brand> brands = context.Brands.ToList();
            Brand brand = new Brand();
            brand.BrandName = "All";
            brands.Insert(0, brand);
            cbBrand.ItemsSource = brands;
            cbBrand.SelectedIndex = 0;
        }


        private async void bindGridFilter(int pageIndex, long cateId, long brandId, string keyword)
        {
            int i = 0;
            var query = context.Milk.OrderBy(milk => milk.MilkId);
            if (cateId != 0)
            {
                query = query.Where(milk => milk.Cate.CategoryId == cateId).OrderBy(milk => milk.MilkId);
            }
            if (brandId != 0)
            {
                query = query.Where(milk => milk.Cate.Brand.BrandId == brandId).OrderBy(milk => milk.MilkId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(milk => milk.Name.Contains(keyword)).OrderBy(milk => milk.MilkId);
            }
            List<Milk> list = await PaginatedList<Milk>.CreateAsync(query, pageIndex, 4);
            PaginatedList<Milk> pages = (PaginatedList<Milk>)list;
            foreach (var sp in listView.Children)
            {
                if (i < pages.Count)
                {
                    foreach (var obj in ((StackPanel)sp).Children)
                    {
                        if (obj is Label)
                        {
                            ((Label)obj).Content = list[i].Name.ToString() + ": " + pages[i].Price.ToString() + " USD";
                        }
                        if (obj is Image)
                        {
                            try
                            {
                                String path = pages[i].ImageUrl.Replace('/', '\\');
                                string workingDirectory = Environment.CurrentDirectory;
                                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                                ((Image)obj).Source = new BitmapImage(new Uri(projectDirectory + path));
                            }
                            catch
                            {
                            }
                        }
                        if (obj is Label label && label.Name.Equals("demo" + i))
                        {
                            ((Label)obj).Content = list[i].Cate.Name.ToString() + " - " + list[i].Cate.Brand.BrandName.ToString();
                        }
                        if (obj is Button)
                        {
                            Button btnAdd = (Button)obj;
                            btnAdd.Visibility = Visibility.Visible;
                            btnAdd.CommandParameter = pages[i].MilkId;
                            //btnAdd.Click += CartOnClick;
                        }
                    }
                    i++;
                }
                else
                {
                    foreach (var obj in ((StackPanel)sp).Children)
                    {
                        if (obj is Label)
                        {
                            ((Label)obj).Content = "";
                        }
                        if (obj is Image)
                        {
                            try
                            {
                                ((Image)obj).Source = null;
                            }
                            catch
                            {
                            }
                        }
                        if (obj is Label)
                        {
                            ((Label)obj).Content = "";
                        }
                        if (obj is Button)
                        {
                            ((Button)obj).Visibility = Visibility.Hidden;
                        }
                    }
                    i++;
                }

            }
            btnPreviousPage.IsEnabled = pages.HasPreviousPage;
            btnNextPage.IsEnabled = pages.HasNextPage;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Category cate = cbCategory.SelectedItem as Category;
            long cateId = (cate == null) ? 0 : cate.CategoryId;
            Brand brand = cbBrand.SelectedItem as Brand;
            long brandId = (brand == null) ? 0 : brand.BrandId;
            String keyword = txtSearch.Text;
            previous = 1;
            next = previous + 1;
            bindGridFilter(previous, cateId, brandId, keyword);
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            Category cate = cbCategory.SelectedItem as Category;
            long cateId = (cate == null) ? 0 : cate.CategoryId;
            Brand brand = cbBrand.SelectedItem as Brand;
            long brandId = (brand == null) ? 0 : brand.BrandId;
            String keyword = txtSearch.Text;
            next = previous;
            previous -= 1;
            bindGridFilter(previous, cateId, brandId, keyword);
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            Category cate = cbCategory.SelectedItem as Category;
            long cateId = (cate == null) ? 0 : cate.CategoryId;
            Brand brand = cbBrand.SelectedItem as Brand;
            long brandId = (brand == null) ? 0 : brand.BrandId;
            String keyword = txtSearch.Text;
            previous = next;
            next += 1;
            bindGridFilter(previous, cateId, brandId, keyword);
        }
    }
}
