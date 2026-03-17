using Models;
using Models.ViewModel;
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
using WebApiClient;
using StoreOwnerApplication.Frames;

namespace StoreOwnerApplication.Frames
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        LoginViewModel loginViewModel;
        public LoginPage()
        {
            InitializeComponent();

            MainWindow main = (MainWindow)Application.Current.MainWindow;

            main.SidebarMenu.Visibility = Visibility.Visible;

            main.ContentFrame.Navigate(new StartPage());
        }


        private async void button_Login_Click(object sender, RoutedEventArgs e)
        {
            string email = "david.levi@mail.com";
            string password = "Dav123!";
            //string email = EmailTextBox.Text;
            //string password = PasswordBox.Password;

            bool isValid = true;

            // איפוס הודעות
            EmailError.Visibility = Visibility.Collapsed;
            PasswordError.Visibility = Visibility.Collapsed;

            EmailTextBox.BorderBrush = Brushes.Gray;
            PasswordBox.BorderBrush = Brushes.Gray;

            // בדיקת אימייל
            if (string.IsNullOrWhiteSpace(email))
            {
                EmailError.Text = "Please insert email";
                EmailError.Visibility = Visibility.Visible;
                EmailTextBox.BorderBrush = Brushes.Red;
                isValid = false;
            }
            else if (!email.Contains("@"))
            {
                EmailError.Text = "The email is not vaild";
                EmailError.Visibility = Visibility.Visible;
                EmailTextBox.BorderBrush = Brushes.Red;
                isValid = false;
            }

            // בדיקת סיסמה
            if (string.IsNullOrWhiteSpace(password))
            {
                PasswordError.Text = "Please insert password";
                PasswordError.Visibility = Visibility.Visible;
                PasswordBox.BorderBrush = Brushes.Red;
                isValid = false;
            }

            if (!isValid)
                return;

            // אם הכל תקין → שלח לשרת
            WebClient<string> client = new WebClient<string>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5138;
            client.Path = "Api/Guest/Login";

            client.AddParameter("email", email);
            client.AddParameter("password", password);

            try
            {
                string result = await client.GetAsync();

                if (result == "fail")
                {
                    SubmitError.Text = "The email or the password is not valid";
                    SubmitError.Visibility = Visibility.Visible;
                }
                else
                {
                    MainWindow main = (MainWindow)Application.Current.MainWindow;
                    main.SidebarMenu.Visibility = Visibility.Visible;
                    main.MenuColumn.Width = new GridLength(60);
                    main.ContentFrame.Navigate(new StartPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }
    }
}
