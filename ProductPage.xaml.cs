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

namespace Tokarev41razmer
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage(User user)
        {
            InitializeComponent();
            ClientName.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            switch (user.UserRole)
            {
                case 1:
                    RoleName.Text = "Клиент";break;
                case 2:
                    RoleName.Text = "Менеджер";break;
                case 3:
                    RoleName.Text = "Администратор";break;


            }
            var currentProducts = Tokarev41Entities.GetContext().Product.ToList();
            ProductListView.ItemsSource = currentProducts;
            DiscountComboBox.SelectedIndex = 0;
            UpdateProductPage();
            int ProductMaxCount = 0;
            foreach (Product product in currentProducts)
            {
                ProductMaxCount++;
            }
            ProductMaxCountTextBlock.Text = ProductMaxCount.ToString();
        }
        private void UpdateProductPage()
        {
            var currentProducts = Tokarev41Entities.GetContext().Product.ToList();

            //поиск 
            currentProducts = currentProducts.Where(p => p.ProductName.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();

            //сортировка 
            if (RadioButtonUp.IsChecked.Value)
            {
                currentProducts = currentProducts.OrderBy(p => p.ProductCost).ToList();
            }
            if (RadioButtonDown.IsChecked.Value)
            {
                currentProducts = currentProducts.OrderByDescending(p => p.ProductCost).ToList();
            }

            //фильтрация 
            if (DiscountComboBox.SelectedIndex == 2)
            {
                currentProducts = currentProducts.ToList();
            }
            if (DiscountComboBox.SelectedIndex == 1)
            {
                currentProducts = currentProducts.Where(p => (p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount <= 9.99)).ToList();
            }
            if (DiscountComboBox.SelectedIndex == 2)
            {
                currentProducts = currentProducts.Where(p => (p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount <= 14.99)).ToList();
            }
            if (DiscountComboBox.SelectedIndex == 3)
            {
                currentProducts = currentProducts.Where(p => (p.ProductDiscountAmount >= 15)).ToList();
            }

            ProductListView.ItemsSource = currentProducts;

            int ProductCount = 0;
            foreach (Product product in currentProducts)
            {
                ProductCount++;
            }
            ProductCountTextBlock.Text = ProductCount.ToString();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProductPage();
        }

        private void RadioButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProductPage();
        }

        private void RadioButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProductPage();
        }

        private void DiscountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProductPage();
        }
    }
}