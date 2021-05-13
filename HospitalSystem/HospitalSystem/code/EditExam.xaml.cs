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
            dpDate.SelectedDate = selectedAppointment.Date;
            cbTime.SelectedItem = selectedAppointment.Time;
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            appointment.Patient = (Patient)cbPatient.SelectedItem;
            appointment.Doctor = (Doctor)cbDoctor.SelectedItem;
            appointment.Room = (Room)cbRoom.SelectedItem;
            appointment.Date = (DateTime)dpDate.SelectedDate;
            appointment.Time = Convert.ToDateTime((string)cbTime.SelectedItem);
            Room selectedRoom = (Room)cbRoom.SelectedItem;
            _ = selectedRoom.Name == "Ordination" ? appointment.IsOperation = false : appointment.IsOperation = true;
            AppointmentStorage.getInstance().Edit(appointment);
            this.Close();
        }
    }
}
