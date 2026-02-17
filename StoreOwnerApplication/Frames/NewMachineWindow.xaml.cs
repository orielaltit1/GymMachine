using Microsoft.Win32;
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
using System.Windows.Shapes;
using System.IO;
using WebApiClient;

namespace StoreOwnerApplication.Frames
{
    /// <summary>
    /// Interaction logic for NewMachineWindow.xaml
    /// </summary>
    public partial class NewMachineWindow : Window
    {
        List<GymMachineBrand> brands;
        MacinesPage macines;
        string imagePath;
        public NewMachineWindow()
        {
            InitializeComponent();
            GetBrands();
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
            
        }

        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog .Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.gif;*.bmp|" +
                            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                            "Portable Network Graphic (*.png)|*.png|" +
                            "Graphics Interchange Format (*.gif)|*.gif|" +
                            "Bitmap (*.bmp)|*.bmp";
            bool? ok = openFileDialog.ShowDialog();
            if (ok == true)
            {
                Uri uri = new Uri(openFileDialog.FileName);
                this.MachineImagePreview.Source = new BitmapImage(uri);
                this.imagePath = openFileDialog.FileName;//שמירת סיומת של השם של התמונה png or jpj and more
            }
        }

        private void goBack_btn_Click(object sender, RoutedEventArgs e)
        {             
             this.Close();
        }

        private async void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            GymMachine gymMachine = new GymMachine();
            gymMachine.MachineName = MachineNameTextBox.Text;
            gymMachine.MachinePrice = MachinePriceTextBox.Text;
            gymMachine.MachineDescription = MachineDescriptionTextBox.Text;
            gymMachine.MachineImage = System.IO.Path.GetExtension(this.imagePath);
            Stream stream = new FileStream(imagePath,FileMode.Open, FileAccess.Read);
            gymMachine.BrandId = this.BrandComboBox.SelectedValue.ToString();
            WebClient<GymMachine> webClient = new WebClient<GymMachine>();
            webClient.Schema = "http";
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5138;
            webClient.Path = "api/Admin/AddMachine";
            bool ok = await webClient.PostAsync(gymMachine);
            if(ok)
            {
                MessageBox.Show("The Machine Add Successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Faild To Add a Machine","",MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
