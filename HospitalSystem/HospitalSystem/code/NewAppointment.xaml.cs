using System;
using System.Collections.Generic;
using System.Globalization;
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

        Patient patient;
        ListCollectionView collectionView = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
        List<string> terms = new List<string> { "07:00", "07:30", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30",
                                                "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30" };

        public NewAppointment(Patient selectedItem)
        {
            InitializeComponent();

            patient = selectedItem;
            cbDoctor.ItemsSource = DoctorStorage.getInstance().GetAll();
                    
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Appointment appt = new Appointment();
            appt.Id = AppointmentStorage.getInstance().GenerateNewID();
            appt.Patient = patient;
            appt.Doctor = (Doctor)cbDoctor.SelectedItem;
            appt.Room = RoomStorage.getInstance().GetOne(1);
            appt.Date = (DateTime)dp1.SelectedDate;
            string time = (string)cbTime.SelectedItem;
            appt.Time = DateTime.Parse(time);
            AppointmentStorage.getInstance().Add(appt);
            this.Close();           
        }

        private void doctorChanged(object sender, System.EventArgs e)
        {
            filter();
            displayTerms();
        }

        private void dateChanged(object sender, System.EventArgs e)
        {
            filter();
            displayTerms();
        }

        private void filter()
        {
            if (cbDoctor.SelectedItem != null && dp1.SelectedDate != null)
            {
                collectionView.Filter = (e) =>
                {
                    Appointment temp = e as Appointment;
                    if (temp.Doctor == (Doctor)cbDoctor.SelectedItem && temp.Date == (DateTime)dp1.SelectedDate)
                        return true;
                    return false;
                };
            }
        }

        private void displayTerms()
        {
            List<string> occupied = new List<string>();
            cbTime.Items.Clear();

            foreach (Appointment a in collectionView)
            {
                occupied.Add(a.Time.ToString("HH:mm"));
            }

            foreach (string s in terms)
            {
                if (!occupied.Contains(s))
                    cbTime.Items.Add(s);
            }
        }


    }
}
