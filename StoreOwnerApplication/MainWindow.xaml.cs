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
        public MainWindow()
        {
            InitializeComponent();
            ViewStartPage();
            MenuTextVisibility = Visibility.Visible;
            DataContext = this;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Exitbtn(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        private void StartPage_Click(object sender, RoutedEventArgs e)
        {
            ViewStartPage();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ViewLoginPage();
        }

        private void MachinesPage_Click(object sender, RoutedEventArgs e)
        {
            ViewMachinesPage(); 
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
    }
}