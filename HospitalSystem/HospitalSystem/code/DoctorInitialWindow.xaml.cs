using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalSystem.code
{
    /// <summary>
    /// Interaction logic for DoctorInitialWindow.xaml
    /// </summary>
    public partial class DoctorInitialWindow : Window
    {
        /*public DoctorInitialWindow()
        {
            InitializeComponent();

            Patient p1 = new Patient("pac1", "Marko", "Nikolic",12354, "Bulevar", 1234554);
            
            Patient p2 = new Patient("pac2", "Marko", "Nikic", 12654, "Bula", 1234554);

            Doctor d1 = new Doctor("doc1", "Ivan", "Ivanovic", 12686, "", 1234554);

            Doctor d2 = new Doctor("doc2", "Ivana", "Ivanic", 7654, "", 1234575);

            Room r1 = new Room(1 , "Control");

            Room r2 = new Room(2, "Operation");

            Examination exam1 = new Examination("exam1");
            exam1.Patient = p1;
            exam1.Doctor = d1;
            exam1.Room = r1;
            exam1.Time = new DateTime(2012, 12, 25, 10, 30, 50);

            Examination exam2 = new Examination("exam2");
            exam2.Patient = p2;
            exam2.Doctor = d2;
            exam2.Room = r2;
            exam1.Time = new DateTime(2013, 10, 25, 12, 30, 50);
            ExaminationStorage.getInstance().Save(exam1);
            ExaminationStorage.getInstance().Save(exam2);
            //DataGridXAML.Items.Add(exam1);
            //DataGridXAML.Items.Add(exam2);
            List<Examination> ExamList = ExaminationStorage.getInstance().GetAll();
            foreach (Examination exam in ExamList)
                DataGridXAML.Items.Add(exam);

            //           string path = @"C:\Users\Marko\Desktop\Hospital-System\HospitalSystem\HospitalSystem\HospitalSystem.json";
            //           using (var tr = new StreamReader(path, true))
            //           {
            //
            //              txtList.Text = tr.ReadToEnd().ToString();
            //
            //               tr.Close();
            //         }
            //           //  FileLocation = 
            //     }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewExam ne = new NewExam();
           
            ne.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedItem = DataGridXAML.SelectedItem;
            if (selectedItem != null)
            {
                DataGridXAML.Items.Remove(selectedItem);
                List<Examination> ExamList = ExaminationStorage.getInstance().GetAll();
                //Examination exam = ExamList[DataGridXAML.SelectedIndex];
                //ExaminationStorage.getInstance().Delete(exam);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<Examination> ExamList = ExaminationStorage.getInstance().GetAll();
            DataGridXAML.Items.Clear();
            foreach (Examination exam in ExamList)
            {
                DataGridXAML.Items.Add(exam);
            }
        }*/
    }
}
