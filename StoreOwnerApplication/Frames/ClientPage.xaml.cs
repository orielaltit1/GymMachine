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
    public partial class ClientPage : UserControl
    {
        List<Client> clients;
        public ClientPage()
        {
            InitializeComponent();
            GetClients();
        }

        private async Task GetClients()
        {
            WebClient<List<Client>> webClient = new WebClient<List<Client>>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5138;
            webClient.Path = "api/Admin/GetClients";
            this.clients = await webClient.GetAsync(); // פעולה הזאת מביאה את הנתונים
            ClientsListView.ItemsSource = this.clients;
        }
    }
}
