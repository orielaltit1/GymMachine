using Models;
using StoreOwnerApplication.Frames;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebApiClient;




namespace StoreOwnerApplication.Frames
{
    /// <summary>
    /// Interaction logic for MacinesPage.xaml
    /// </summary>
    public partial class MacinesPage : UserControl
    {
        List<AdminMachineViewModel> machines;
        public ObservableCollection<GymMachine> MachinesList { get; set; }
        public MacinesPage()
        {
            InitializeComponent(); // טוען את רכיבי ה-XAML
            GetMachines();
            MachinesList = new ObservableCollection<GymMachine>();
        }

        private async Task GetMachines()
        {
            WebClient<List<AdminMachineViewModel>> webClient = new WebClient<List<AdminMachineViewModel>>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5138;
            webClient.Path = "api/Admin/GetMachines";
            this.machines = await webClient.GetAsync(); // פעולה הזאת מביאה את הנתונים
            MachinesListView.ItemsSource = this.machines;
            this.DataContext = this.machines;
        }

       
        // פונקציה המופעלת בלחיצה על כפתור "Add Machine"
        private void AddMachine_Click(object sender, RoutedEventArgs e)
        {
            // יצירת אובייקט חדש והוספתו לרשימה
            // ה-ObservableCollection יעדכן את המסך מיד
            //Machines.Add(new GymMachine { MachineName = "New Machine", MachinePrice = "0 NIS" });
            NewMachineWindow newMachineWindow = new NewMachineWindow();
            bool? result = newMachineWindow.ShowDialog();

        }

        // פונקציה המופעלת בלחיצה על כפתור "Delete" בתוך אחת השורות
        private async void Delete_btn_Click_1(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.DataContext is GymMachine selectedMachine)
            {
                WebClient<bool> webClient = new WebClient<bool>();
                webClient.Schema = "http";
                webClient.Host = "localhost";
                webClient.Port = 5138;
                webClient.Path = $"api/Admin/DeleteMachine/{selectedMachine.MachineId}";

                bool ok = await webClient.GetAsync();
                if (ok)
                {
                    MachinesList.Remove(selectedMachine); // אין צורך ב-FirstOrDefault
                }
                else
                {
                    // מומלץ תמיד להוסיף הודעה במקרה של כישלון כדי שהמשתמש ידע מה קרה
                    MessageBox.Show("The system failed");
                }
            }
        }
    }
}
