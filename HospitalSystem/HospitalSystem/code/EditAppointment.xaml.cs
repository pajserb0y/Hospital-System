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
    /// Interaction logic for EditAppointment.xaml
    /// </summary>
    public partial class EditAppointment : Window
    {
        private Appointment appointment;

        public EditAppointment(Appointment selectedAppointment)
        {
            InitializeComponent();
            appointment = selectedAppointment;

            cbPatient.ItemsSource = PatientsStorage.getInstance().GetAll();
            cbDoctor.ItemsSource = DoctorStorage.getInstance().GetAll();

            cbPatient.SelectedItem = selectedAppointment.Patient;
            cbDoctor.SelectedItem = selectedAppointment.Doctor;
            dp1.SelectedDate = selectedAppointment.Date;
            txt1.Text = Convert.ToString(selectedAppointment.Time.TimeOfDay);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            double differenceInDays = Math.Abs(appointment.Date.Subtract((DateTime)dp1.SelectedDate).TotalDays);

            if (differenceInDays <= 2)
            {
                appointment.Patient = (Patient)cbPatient.SelectedItem;
                appointment.Doctor = (Doctor)cbDoctor.SelectedItem;
                appointment.Time = Convert.ToDateTime(txt1.Text);
                appointment.Date = (DateTime)dp1.SelectedDate;
                AppointmentStorage.getInstance().Edit(appointment);
                this.Close();
            }
            else
            {
                dp1.SelectedDate = appointment.Date;
                InvalidEdit iv = new InvalidEdit();
                iv.Show();
            }
        }
    }
}
