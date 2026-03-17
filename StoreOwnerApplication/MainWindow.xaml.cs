using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StoreOwnerApplication.Frames;

namespace StoreOwnerApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StartPage startPage;
        LoginPage loginPage;
        MacinesPage macinesPage;
        ClientPage clientPage;
        public MainWindow()
        {
            InitializeComponent();
            SidebarMenu.Visibility = Visibility.Collapsed;
            MenuColumn.Width = new GridLength(0);
            ContentFrame.Navigate(new LoginPage());
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ViewStartPage()
        {
            if(this.startPage == null)
                this.startPage = new StartPage();
            this.ContentFrame.Content = this.startPage;
        }
        private void ViewMachinesPage()
        {
            if (this.macinesPage == null)
                this.macinesPage = new MacinesPage();
            this.ContentFrame.Content = this.macinesPage;
        }
        private void ViewLoginPage()
        {
            if (this.loginPage == null)
                this.loginPage = new LoginPage();
            this.ContentFrame.Content = this.loginPage;//החלפת מסך
        }
        private void ViewClientPage()
        {
            if (this.clientPage == null)
                this.clientPage = new ClientPage();
            this.ContentFrame.Content = this.clientPage;//החלפת מסך
        }
        
        private bool _isExpanded = false;
        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            _isExpanded = !_isExpanded;

            MenuColumn.Width = _isExpanded
                ? new GridLength(200)
                : new GridLength(60);

            MenuTextVisibility = _isExpanded
                ? Visibility.Visible
                : Visibility.Collapsed;

            // רענון Binding
            DataContext = null;
            DataContext = this;
        }

        public Visibility MenuTextVisibility { get; set; } = Visibility.Collapsed;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewStartPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ViewLoginPage();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ViewMachinesPage();
        }

        private void Client_btn_Click(object sender, RoutedEventArgs e)
        {
            ViewClientPage();
        }

        private void Logout_btn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new LoginPage());
            SidebarMenu.Visibility = Visibility.Collapsed;
            MenuColumn.Width = new GridLength(0);
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}