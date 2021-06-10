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
                if (doctorsWithDifferentSpecialization.Count <= 0)
                    doctorsWithDifferentSpecialization.Add(doc);

                bool specializationAlreadyInList = false;
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

        private bool checkRefferal(string spec)
        {
            if (spec == "General medicine")
                return true;

            foreach (Refferal r in RefferalStorage.getInstance().GetAll())
            {
                if (r.PatientId == patient.Id)
                    if (r.Specialization == spec && r.Status == Refferal.STATUS.Active)
                    {
                        r.Status = Refferal.STATUS.Used;
                        return true;
                    }
            }

            return false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (cbDoctor.SelectedItem == null)
                return;
            Doctor doc = (Doctor)cbDoctor.SelectedItem;

            if (!checkRefferal(doc.Specialization))
            {
                MessageBox.Show("You must have refferal for doctor specializing in " + doc.Specialization + ".");
                return;
            }

            if (dp1.SelectedDate == null)
                return;
       
            if (cbTime.SelectedItem == null)
                return;

            string time = (string)cbTime.SelectedItem;
            Appointment appt = new Appointment(AppointmentStorage.getInstance().GenerateNewID(), doc, patient, RoomStorage.getInstance().GetOne(1), (DateTime)dp1.SelectedDate, DateTime.Parse(time), false, 0, DateTime.Now);
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
            if (d == null)
                return;

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

        private DateTime findFirstAvailableTerm(Doctor doctor)
        {
            ListCollectionView allAppointments = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
            List<string> occupiedTerms = new List<string>();
            DateTime currentDate = DateTime.Now.Date;

            filterAppointments(doctor, allAppointments, occupiedTerms);

            return findFirstAvailable(occupiedTerms, currentDate);
        }

        private DateTime findFirstAvailable(List<string> occupiedTerms, DateTime currentDate)
        {
            while (true)
            {
                foreach (String term in terms)
                {
                    String temp = currentDate.Date.ToString("dd-MMM-yyyy") + " " + term;

                    if (!occupiedTerms.Contains(temp))
                    {
                        return DateTime.ParseExact(temp, "dd-MMM-yyyy HH:mm", CultureInfo.InvariantCulture);
                    }
                }

                currentDate.AddDays(1);
            }
        }

        private static void filterAppointments(Doctor doctor, ListCollectionView allAppointments, List<string> occupiedTerms)
        {
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
        }

        private void specializationChanged(object sender, System.EventArgs  e)
        {
            if (cbSpecialization.SelectedItem == null)
                return;
            
            doctorCollection.Filter = (e) =>
            {
                Doctor tempDoctor = e as Doctor;
                if (tempDoctor.Specialization == cbSpecialization.SelectedItem.ToString())
                    return true;
                return false;   
            };
                
            cbDoctor.ItemsSource = doctorCollection;
        }

        private void filter()
        {
            if (cbDoctor.SelectedItem == null || dp1.SelectedDate == null)
                return;
            
            appointmentCollection.Filter = (e) =>
            {
                Appointment temp = e as Appointment;
                if (temp.Doctor == (Doctor)cbDoctor.SelectedItem && temp.Date == (DateTime)dp1.SelectedDate)
                    return true;
                return false;
            };
          
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
