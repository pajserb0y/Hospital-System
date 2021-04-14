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
    /// Interaction logic for NewAppointment.xaml
    /// </summary>
    public partial class NewAppointment : Window
    {
        public NewAppointment()
        {
            InitializeComponent();
            cbPatient.ItemsSource = PatientsStorage.getInstance().GetAll();
            cbDoctor.ItemsSource = DoctorStorage.getInstance().GetAll();            
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Appointment appt = new Appointment();
            appt.Id = AppointmentStorage.getInstance().GenerateNewID();
            appt.Patient = (Patient)cbPatient.SelectedItem;
            appt.Doctor = (Doctor)cbDoctor.SelectedItem;
            appt.Room = RoomStorage.getInstance().GetOne(8);
            appt.Date = (DateTime)dp1.SelectedDate;
            appt.Time = Convert.ToDateTime(txt1.Text);
            AppointmentStorage.getInstance().Add(appt);
            this.Close();           
        }
    }
}
