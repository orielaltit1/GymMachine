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
        public ObservableCollection<AdminMachineViewModel> MachinesList { get; set; }
        public MacinesPage()
        {
            InitializeComponent(); // טוען את רכיבי ה-XAML
            GetMachines();
            MachinesList = new ObservableCollection<AdminMachineViewModel>();
            MachinesListView.ItemsSource = MachinesList;
        }

        private async Task GetMachines()
        {
            WebClient<List<AdminMachineViewModel>> webClient = new WebClient<List<AdminMachineViewModel>>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5138;
            webClient.Path = "api/Admin/GetMachines";
            var dataFromServer = await webClient.GetAsync(); // פעולה הזאת מביאה את הנתונים
            if (dataFromServer != null)
            {
                MachinesList.Clear(); // מרוקן את הרשימה הקיימת בלי לנתק אותה
                foreach (var item in dataFromServer)
                {
                    MachinesList.Add(item); // מוסיף לרשימה המחוברת
                }
            }
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
            // 1. קבלת הכפתור שנלחץ
            var button = sender as Button;

            // 2. ה-DataContext הוא מסוג AdminMachineViewModel (לא GymMachine!)
            if (button != null && button.DataContext is AdminMachineViewModel viewModel)
            {
                // 3. הוספת הודעת אישור (Confirmation)
                var result = MessageBox.Show(
                    $"Are you sure you want to delete {viewModel.Machine.MachineName}?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // 4. קריאה לשרת עם ה-ID הנכון
                    WebClient<bool> webClient = new WebClient<bool>();
                    webClient.Schema = "http";
                    webClient.Host = "localhost";
                    webClient.Port = 5138;
                    webClient.Path = $"api/Admin/DeleteMachine/{viewModel.Machine.MachineId}";

                    bool ok = await webClient.GetAsync();

                    if (ok)
                    {
                        // 5. הסרה מה-ObservableCollection שמוצג ברשימה
                        // חשוב: אנחנו מסירים את ה-ViewModel כולו, לא רק את המכונה
                        MachinesList.Remove(viewModel);
                    }
                    else
                    {
                        MessageBox.Show("The system failed to delete the machine.");
                    }
                }
            }
        }
    }
}
