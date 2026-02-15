using Models;
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

namespace StoreOwnerApplication.Frames
{
    /// <summary>
    /// Interaction logic for NewMachine.xaml
    /// </summary>
    public partial class NewMachine : Page
    {
        List<GymMachineBrand> brands;
        public NewMachine()
        {
            InitializeComponent();
            GetBrands();
        }

        private void goBack_btn_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private async Task GetBrands()
        {
            WebClient<List<GymMachineBrand>> webClient = new WebClient<List<GymMachineBrand>>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5138;
            webClient.Path = "api/Admin/GetBrands";
            this.brands = await webClient.GetAsync(); // פעולה הזאת מביאה את הנתונים
            BrandComboBox.ItemsSource = brands;
            BrandComboBox.DisplayMemberPath = "BrandName";
        }

        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
