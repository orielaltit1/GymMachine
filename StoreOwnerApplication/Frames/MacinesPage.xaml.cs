using Models;
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
        EditMachine editMachine;
        // רשימה מסוג ObservableCollection היא מיוחדת ל-WPF.
        // ברגע שמוסיפים או מסירים ממנה פריט, המסך מתעדכן אוטומטית בלי צורך לרענן ידנית.
        public ObservableCollection<GymMachine> Machines { get; set; }

        // הבנאי (Constructor) - הפונקציה שרצה ראשונה כשיוצרים את הדף
        public MacinesPage()
        {
            InitializeComponent(); // טוען את רכיבי ה-XAML

            // אתחול הרשימה עם כמה נתונים לדוגמה
            Machines = new ObservableCollection<GymMachine>
            {
                
                new GymMachine { MachineName = "Treadmill 3000", MachinePrice = "5,000 NIS" },
                new GymMachine { MachineName = "Chest Press", MachinePrice = "3,200 NIS" },
                new GymMachine { MachineName = "Dumbbell Set", MachinePrice = "800 NIS" }
            };

            // חיבור מקור הנתונים של ה-ListView לרשימה שיצרנו
            // מעכשיו ה-ListView יציג את מה שיש בתוך Machines
            MachinesListView.ItemsSource = Machines;
        }
        private void ViewMachinePage()
        {
            if (this.editMachine == null)
                this.editMachine = new EditMachine();
           // this.ContentFrame.Content = this.editMachine;//החלפת מסך
        }
        // פונקציה המופעלת בלחיצה על כפתור "Add Machine"
        private void AddMachine_Click(object sender, RoutedEventArgs e)
        {
            // יצירת אובייקט חדש והוספתו לרשימה
            // ה-ObservableCollection יעדכן את המסך מיד
            //Machines.Add(new GymMachine { MachineName = "New Machine", MachinePrice = "0 NIS" });
            
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
