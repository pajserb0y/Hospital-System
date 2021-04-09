using System;
using System.Collections.Generic;
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
    /// Interaction logic for NewExam.xaml
    /// </summary>
    public partial class NewExam : Window
    {
        public NewExam()
        {
            InitializeComponent();
            lv1.ItemsSource = PatientsStorage.getInstance().GetAll();
            lv2.ItemsSource = DoctorStorage.getInstance().GetAll();
            lv3.ItemsSource = RoomStorage.getInstance().GetAll();
            //lv1.DataContext = PatientsStorage.getInstance().GetAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Examination exam = new Examination();
            exam.Id = ExaminationStorage.getInstance().GenerateNewID();
            exam.Patient = (Patient)lv1.SelectedItem;
            exam.Doctor = (Doctor) lv2.SelectedItem;
            exam.Room = (Room)lv3.SelectedItem;
           // exam.Time = (DateTime) DatePicker.;
            ExaminationStorage.getInstance().Save(exam);
            //List<Examination> ExamList = ExaminationStorage.getInstance().GetAll();
          // foreach (Examination exam in ExamList)
           //     DataGridXAML.Items.Add(exam);
        }
    }
}
