using Models;
using WebApiClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using StoreOwnerApplication.Frames;




namespace StoreOwnerApplication.Frames
{
    /// <summary>
    /// Interaction logic for MacinesPage.xaml
    /// </summary>
    public partial class MacinesPage : UserControl
    {
        List<AdminMachineViewModel> machines;
        public MacinesPage()
        {
            InitializeComponent(); // טוען את רכיבי ה-XAML
            GetMachines();
            

            // חיבור מקור הנתונים של ה-ListView לרשימה שיצרנו
            // מעכשיו ה-ListView יציג את מה שיש בתוך Machines
            
        }
        // רשימה מסוג ObservableCollection היא מיוחדת ל-WPF.
        // ברגע שמוסיפים או מסירים ממנה פריט, המסך מתעדכן אוטומטית בלי צורך לרענן ידנית.
        public ObservableCollection<GymMachine> Machines { get; set; }

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
        private void DeleteMachine_Click(object sender, RoutedEventArgs e)
        {
            // ה-sender הוא האלמנט שלחצו עליו. אנחנו ממירים אותו ל-Button
            Button btn = sender as Button;

            // בדיקה: האם ההמרה הצליחה והאם יש בתוך ה-Tag אובייקט מסוג GymMachine?
            // (זוכר שב-XAML שמנו Tag="{Binding}"? זה בדיוק בשביל השורה הזו)
            if (btn != null && btn.Tag is GymMachine machineToDelete)
            {
                // מחיקת המכונה הספציפית מהרשימה
                Machines.Remove(machineToDelete);
            }
        }
    }
}
