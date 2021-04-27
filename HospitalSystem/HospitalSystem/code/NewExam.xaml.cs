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
            cbPatient.ItemsSource = PatientsStorage.getInstance().GetAll();
            cbDoctor.ItemsSource = DoctorStorage.getInstance().GetAll();
            cbRoom.ItemsSource = RoomStorage.getInstance().GetAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = new Examination();
            appointment.Id = ExaminationStorage.getInstance().GenerateNewID();
            appointment.Patient = (Patient)cbPatient.SelectedItem;
            appointment.Doctor = (Doctor) cbDoctor.SelectedItem;
            appointment.Room = (Room)cbRoom.SelectedItem;
            appointment.Date = (DateTime)dp1.SelectedDate;
            appointment.Time = Convert.ToDateTime(txt1.Text);
            AppointmentStorage.getInstance().Add(appointment);
            this.Close();
        }
    }
}
