using Microsoft.Win32;
using Models;
using StoreOwnerApplication.Frames;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
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
using WebApiClient;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            MachineNameError.Visibility = Visibility.Collapsed;
            MachineNameTextBox.BorderBrush = Brushes.Gray;
            MachinePriceError.Visibility = Visibility.Collapsed;
            MachinePriceTextBox.BorderBrush = Brushes.Gray;
            MachineDescriptionError.Visibility = Visibility.Collapsed;
            MachineDescriptionTextBox.BorderBrush = Brushes.Gray;
            MachineImageError.Visibility = Visibility.Collapsed;
            MachineBrandError.Visibility = Visibility.Collapsed;



            //Name
            if (string.IsNullOrWhiteSpace(MachineNameTextBox.Text))
            {
                MachineNameError.Text = "Please fill in a name";
                MachineNameError.Visibility = Visibility.Visible;
                MachineNameTextBox.BorderBrush = Brushes.Red;
                return;
            }
            if (MachineNameTextBox.Text.Trim().Length < 3)
            {
                MachineNameError.Text = "Name must be at least 3 characters"; 
                MachineNameError.Visibility = Visibility.Visible;
                MachineNameTextBox.BorderBrush = Brushes.Red;
                return;
            }
            gymMachine.MachineName = MachineNameTextBox.Text;

            //Price
            if (MachinePriceTextBox.Text == "")
            {
                MachinePriceError.Text = "Please fill in the price section";
                MachinePriceError.Visibility = Visibility.Visible;
                MachinePriceTextBox.BorderBrush = Brushes.Red;
                return;
            }
            if (!double.TryParse(MachinePriceTextBox.Text, out double price))
            {
                MachinePriceError.Text = "Please use only numbers digits";
                MachinePriceError.Visibility = Visibility.Visible;
                MachinePriceTextBox.BorderBrush = Brushes.Red;
                return;
            }
            gymMachine.MachinePrice = MachinePriceTextBox.Text;

            //Description
            if (string.IsNullOrWhiteSpace(MachineDescriptionTextBox.Text))
            {
                MachineDescriptionError.Text = "Please fill in a description";
                MachineDescriptionError.Visibility = Visibility.Visible;
                MachineDescriptionTextBox.BorderBrush = Brushes.Red;
                return;
            }
            if (MachineDescriptionTextBox.Text.Trim().Length < 10)
            {
                MachineDescriptionError.Text = "Name must be at least 10 characters";
                MachineDescriptionError.Visibility = Visibility.Visible;
                MachineDescriptionTextBox.BorderBrush = Brushes.Red;
                return;
            }
            gymMachine.MachineDescription = MachineDescriptionTextBox.Text;
            
            //Brand
            if (BrandComboBox.SelectedValue == null)
            {
                MachineBrandError.Text = "Please select a brand";
                MachineBrandError.Visibility = Visibility.Visible;
                return;
            }
            gymMachine.BrandId = this.BrandComboBox.SelectedValue.ToString();

            //Image
            if (string.IsNullOrEmpty(imagePath))
            {
                MachineImageError.Text = "Please choose an image";
                MachineImageError.Visibility = Visibility.Visible;
                return;
            }
            gymMachine.MachineImage = System.IO.Path.GetExtension(this.imagePath);

            gymMachine.IsActive = true;
            
            
            
            WebClient<GymMachine> webClient = new WebClient<GymMachine>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5138;
            webClient.Path = "api/Admin/AddNewMachine";
            
            using (Stream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                bool ok = await webClient.PostAsync(gymMachine, stream);

                if (ok)
                {
                    MessageBox.Show("The Machine Add Successfully!");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Faild To Add a Machine", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
