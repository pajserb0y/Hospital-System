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
        ListCollectionView appointmentCollection = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
        ListCollectionView doctorCollection = new ListCollectionView(DoctorStorage.getInstance().GetAll());
        List<string> terms = new List<string> { "07:00", "07:30", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30",
                                                "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30" };

        public NewAppointment(Patient selectedItem)
        {
            InitializeComponent();
            patient = selectedItem;
            initializeSpecializations();
        }

        

        private void initializeSpecializations()
        {
            List<Doctor> doctorsWithDifferentSpecialization = new List<Doctor>();            
            foreach (Doctor doc in DoctorStorage.getInstance().GetAll())
            {
                bool specializationAlreadyInList = false;
                if (doctorsWithDifferentSpecialization.Count <= 0)
                    doctorsWithDifferentSpecialization.Add(doc);

                foreach (Doctor dr in doctorsWithDifferentSpecialization)
                    if (doc.Specialization == dr.Specialization)
                    {
                        specializationAlreadyInList = true;
                        break;
                    }

                if (specializationAlreadyInList is false)
                    doctorsWithDifferentSpecialization.Add(doc);
            }

            cbSpecialization.ItemsSource = doctorsWithDifferentSpecialization;
            cbSpecialization.SelectedIndex = -1;
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Appointment appt = new Appointment();
            appt.Id = AppointmentStorage.getInstance().GenerateNewID();
            appt.Patient = patient;
            if (cbDoctor.SelectedItem != null)
                appt.Doctor = (Doctor)cbDoctor.SelectedItem;
            else
                return;

            appt.Room = RoomStorage.getInstance().GetOne(1);
            if (dp1.SelectedDate != null)
                appt.Date = (DateTime)dp1.SelectedDate;
            else
                return;

            if (cbTime.SelectedItem != null)
            {
                string time = (string)cbTime.SelectedItem;
                appt.Time = DateTime.Parse(time);
            }
            else
                return;
            appt.TimesChanged = 0;
            appt.TimeOfCreation = DateTime.Now;
            AppointmentStorage.getInstance().Add(appt);
            this.Close();           
        }

        private void doctorChanged(object sender, System.EventArgs e)
        {
            filter();
            displayTerms();
            if (OptionTime.IsChecked == true)
            {
                dp1.IsEnabled = true;
                cbTime.IsEnabled = true;

                DateTime firstAvailable = findFirstAvailableTerm((Doctor)cbDoctor.SelectedItem);
                dp1.SelectedDate = firstAvailable.Date;
                cbTime.SelectedItem = firstAvailable.ToString("HH:mm");

                dp1.IsEnabled = false;
                cbTime.IsEnabled = false;
            }
        }

        private void dateChanged(object sender, System.EventArgs e)
        {
            filter();
            displayTerms();
        }

        private void CheckBoxDoctor(object sender, RoutedEventArgs e)
        {
            if (OptionDoctor.IsChecked == true)
            {
                findFirstAvailableDoctor();

                cbDoctor.IsEnabled = false;
                OptionTime.IsEnabled = false;
                dp1.IsEnabled = false;
                cbTime.IsEnabled = false;
            }
            else
            {
                cbDoctor.IsEnabled = true;
                OptionTime.IsEnabled = true;
                dp1.IsEnabled = true;
                cbTime.IsEnabled = true;
            }
        }

        private void CheckBoxTerm(object sender, RoutedEventArgs e)
        {
            if (OptionTime.IsChecked == true)
            {
                dp1.IsEnabled = false;
                cbTime.IsEnabled = false;

                DateTime firstAvailable;
                if (cbDoctor.SelectedItem != null)
                {
                    firstAvailable = findFirstAvailableTerm((Doctor)cbDoctor.SelectedItem);
                    dp1.SelectedDate = firstAvailable.Date;
                    cbTime.SelectedItem = firstAvailable.ToString("HH:mm");
                }
            }
            else
            {
                dp1.IsEnabled = true;
                cbTime.IsEnabled = true;
            }

        }

       
        private void findFirstAvailableDoctor()
        {
            Doctor d = (Doctor)cbSpecialization.SelectedItem;

            if (d != null)
            { 
                String specialization = d.Specialization;

                Doctor firstAvailable = (Doctor)doctorCollection.GetItemAt(0);
                DateTime min = findFirstAvailableTerm(firstAvailable);
                DateTime temp;

                foreach(Doctor doc in doctorCollection)
                {
                    temp = findFirstAvailableTerm(doc);

                    if (temp < min)
                    {
                        firstAvailable = doc;
                        min = temp;
                    }
                }

                cbDoctor.SelectedItem = firstAvailable;
                filter();
                displayTerms();
                dp1.SelectedDate = min.Date;
                cbTime.SelectedItem = min.ToString("HH:mm");
            }
        }

        private DateTime findFirstAvailableTerm(Doctor doctor)
        {
            ListCollectionView allAppointments = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
            List<string> occupiedTerms = new List<string>();

            allAppointments.Filter = (e) =>
            {
                Appointment tempApp = e as Appointment;
                if (doctor == tempApp.Doctor)
                {
                    occupiedTerms.Add(tempApp.Date.ToString("dd-MMM-yyyy") + " " + tempApp.Time.ToString("HH:mm"));
                    return true;
                }

                return false;
            };

            DateTime currentDate = DateTime.Now.Date;
            String temp;

            while(true)
            {
                foreach(String term in terms)
                {
                    temp = currentDate.Date.ToString("dd-MMM-yyyy") + " " + term;

                    if (!occupiedTerms.Contains(temp))
                    {
                        return DateTime.ParseExact(temp, "dd-MMM-yyyy HH:mm", CultureInfo.InvariantCulture);
                    }
                }

                currentDate.AddDays(1);
            }    
        }

        private void specializationChanged(object sender, System.EventArgs  e)
        {
            if (cbSpecialization.SelectedItem != null)
            {
                doctorCollection.Filter = (e) =>
                {
                    Doctor tempDoctor = e as Doctor;
                    if (tempDoctor.Specialization == cbSpecialization.SelectedItem.ToString())
                        return true;

                    return false;
                };
                
            }
            cbDoctor.ItemsSource = doctorCollection;
        }

        private void filter()
        {
            if (cbDoctor.SelectedItem != null && dp1.SelectedDate != null)
            {
                appointmentCollection.Filter = (e) =>
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

            foreach (Appointment a in appointmentCollection)
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
