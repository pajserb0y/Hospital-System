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
        private Appointment appointment;
        public EditExam(Appointment selectedAppointment)
        {
            InitializeComponent();
            appointment = selectedAppointment;

            cbPatient.ItemsSource = PatientsStorage.getInstance().GetAll();
            cbDoctor.ItemsSource = DoctorStorage.getInstance().GetAll();
            cbRoom.ItemsSource = RoomStorage.getInstance().GetAll();

            cbPatient.SelectedItem = selectedAppointment.Patient;
            cbDoctor.SelectedItem = selectedAppointment.Doctor;
            cbRoom.SelectedItem = selectedAppointment.Room;
            dp1.SelectedDate = selectedAppointment.Date;
            txt1.Text = Convert.ToString(selectedAppointment.Time.TimeOfDay);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            appointment.Patient = (Patient)cbPatient.SelectedItem;
            appointment.Doctor = (Doctor)cbDoctor.SelectedItem;
            appointment.Room = (Room)cbRoom.SelectedItem;
            appointment.Date = (DateTime)dp1.SelectedDate;
            appointment.Time = Convert.ToDateTime(txt1.Text);
            AppointmentStorage.getInstance().Edit(appointment);
            //List<Examination> ExamList = ExaminationStorage.getInstance().GetAll();
            //foreach (Examination exam in ExamList)
            //     DataGridXAML.Items.Add(exam);
            this.Close();
        }
    }
}
