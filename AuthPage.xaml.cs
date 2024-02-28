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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void guestBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProductPage(guestBtn));
        }

        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTB.Text;
            string password = passwordTB.Text;
            if (login =="" || password == "")
            {
                MessageBox.Show("Есть пустые поля");
                return;
            }
            User user = Tokarev41Entities.GetContext().User.ToList().Find(p => p.UserLogin == login && p.UserPassword == password);
            if (user != null)
            {
                Manager.MainFrame.Navigate(new ProductPage(user));
                loginTB.Text = "";
                passwordTB.Text = "";
            }
            else
            {
                MessageBox.Show("Введены неверные данные");
                enterBtn.IsEnabled = false;
                enterBtn.IsEnabled = true;
            }
        }
    }
}
