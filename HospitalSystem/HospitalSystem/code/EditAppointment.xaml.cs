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
    public partial class EditAppointment : Window
    {
        private Appointment appointment;
        ListCollectionView collectionView = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
        List<string> terms = new List<string> { "07:00", "07:30", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30",
                                                "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30" };

        public EditAppointment(Appointment selectedAppointment)
        {
            InitializeComponent();
            appointment = selectedAppointment;
            cbDoctor.ItemsSource = DoctorStorage.getInstance().GetAll();           
            cbDoctor.SelectedItem = selectedAppointment.Doctor;
            cbDoctor.IsEnabled = false;
            dp1.SelectedDate = selectedAppointment.Date;
            cbTime.Items.Add(selectedAppointment.Time.ToString("HH:mm"));
            cbTime.SelectedItem = selectedAppointment.Time.ToString("HH:mm");
        }

        private bool checkTime(DateTime date)
        {
            double differenceInDays = Math.Abs(date.Subtract((DateTime)dp1.SelectedDate).TotalDays);

            if (differenceInDays > 2)
            {
                dp1.SelectedDate = appointment.Date;
                MessageBox.Show("Invalid date! Must be within 2 days of the selected appointment.");
                return false;
            }
            return true;
        }

        private void Save_Click_Edit(object sender, RoutedEventArgs e)
        {

            if (!checkTime(appointment.Date))
                return;
            if (appointment.TimesChanged > 2)
            {
                MessageBox.Show("Cannot change details of appointment more than 3 times!");
                return;
            }
            if (cbDoctor.SelectedItem != null)
                appointment.Doctor = (Doctor)cbDoctor.SelectedItem;
            if (cbTime.SelectedItem != null)
            {
                string time = (string)cbTime.SelectedItem;
                appointment.Time = DateTime.Parse(time);
            }
            if (dp1.SelectedDate != null)
                appointment.Date = (DateTime)dp1.SelectedDate;
            AppointmentStorage.getInstance().Edit(appointment);
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

        private void displayErrorApptChanged()
        {
            return;
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
