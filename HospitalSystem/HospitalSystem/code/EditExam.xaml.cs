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
    /// Interaction logic for EditExam.xaml
    /// </summary>
    public partial class EditExam : Window
    {
        private Examination exam;
        public EditExam(Examination selectedExam)
        {
            InitializeComponent();
            exam = selectedExam;

            cbPatient.ItemsSource = PatientsStorage.getInstance().GetAll();
            cbDoctor.ItemsSource = DoctorStorage.getInstance().GetAll();
            cbRoom.ItemsSource = RoomStorage.getInstance().GetAll();

            cbPatient.SelectedItem = selectedExam.Patient;
            cbDoctor.SelectedItem = selectedExam.Doctor;
            cbRoom.SelectedItem = selectedExam.Room;
            dp1.SelectedDate = selectedExam.Date;
            txt1.Text = Convert.ToString(selectedExam.Time.TimeOfDay);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            exam.Patient = (Patient)cbPatient.SelectedItem;
            exam.Doctor = (Doctor)cbDoctor.SelectedItem;
            exam.Room = (Room)cbRoom.SelectedItem;
            exam.Date = (DateTime)dp1.SelectedDate;
            exam.Time = Convert.ToDateTime(txt1.Text);
            ExaminationStorage.getInstance().Edit(exam);
            //List<Examination> ExamList = ExaminationStorage.getInstance().GetAll();
            //foreach (Examination exam in ExamList)
            //     DataGridXAML.Items.Add(exam);
            this.Close();
        }
    }
}
