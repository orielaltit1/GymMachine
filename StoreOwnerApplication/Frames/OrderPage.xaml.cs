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
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class OrderPage : UserControl
    {
        List<Order> orders;
        public OrderPage()
        {
            InitializeComponent();
            GetOrders();
        }

        private async Task GetOrders()
        {
            try
            {
                WebClient<List<Order>> webClient = new WebClient<List<Order>>();

                webClient.Schema = "http";
                webClient.Host = "localhost";
                webClient.Port = 5138;
                webClient.Path = "api/Admin/GetOrders";

                this.orders = await webClient.GetAsync();

                OrdersListView.ItemsSource = this.orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
