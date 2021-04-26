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
    /// Interaction logic for SecretarEditAppointment.xaml
    /// </summary>
    public partial class SecretarEditAppointment : Window
    {
        private Appointment currentAppointment;
        ListCollectionView collectionView = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
        List<string> terms = new List<string> { "07:00", "07:30", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30",
                                                "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30" };


        public SecretarEditAppointment(Appointment selectedApp)
        {
            currentAppointment = selectedApp;
            InitializeComponent();
            
            cbDoctor.ItemsSource = DoctorStorage.getInstance().GetAll();

            txtPatient.Text = selectedApp.Patient.ToString();
            cbDoctor.SelectedItem = selectedApp.Doctor;
            dpDate.SelectedDate = selectedApp.Date;
            cbTime.Items.Add(selectedApp.Time.ToString("HH:mm"));
            cbTime.SelectedItem = selectedApp.Time.ToString("HH:mm");
        }

        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            double differenceInDays = Math.Abs(currentAppointment.Date.Subtract((DateTime)dpDate.SelectedDate).TotalDays);

            if (differenceInDays <= 2)
            {
                currentAppointment.Doctor = (Doctor)cbDoctor.SelectedItem;
                currentAppointment.Room = RoomStorage.getInstance().GetOne(8);
                currentAppointment.Date = (DateTime)dpDate.SelectedDate;
                currentAppointment.Time = Convert.ToDateTime((string)cbTime.SelectedItem);
                AppointmentStorage.getInstance().Edit(currentAppointment);
                this.Close();
            }
            else
            {
                dpDate.SelectedDate = currentAppointment.Date;
                InvalidEdit iv = new InvalidEdit();
                iv.Show();
            }
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
            if (cbDoctor.SelectedItem != null && dpDate.SelectedDate != null)
            {
                collectionView.Filter = (e) =>
                {
                    Appointment temp = e as Appointment;
                    if (temp.Doctor == (Doctor)cbDoctor.SelectedItem && temp.Date == (DateTime)dpDate.SelectedDate)
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
