using HospitalSystem.code.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SecretarNewAppointment.xaml
    /// </summary>
    public partial class SecretarNewAppointment : Window
    {
        Patient currentPatient;
        ObservableCollection<Refferal> refferals = RefferalStorage.getInstance().GetAll();
        ListCollectionView collectionAppointments = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
        List<string> terms = new List<string> { "07:00", "07:30", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30",
                                                "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30" };
        public SecretarNewAppointment(Patient selectedPatient)
        {
            InitializeComponent();
            currentPatient = selectedPatient;
            txtPatient.Text = selectedPatient.ToString();
            cbDoctor.ItemsSource = filterDoctors(DoctorStorage.getInstance().GetAll());
            dpDate.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddYears(-1), DateTime.Now.AddDays(-1)));
        }

        public SecretarNewAppointment()
        {
            InitializeComponent();
            txtPatient.Visibility = Visibility.Collapsed;
            cbPatient.Visibility = Visibility.Visible;
            List<Patient> patients = new List<Patient>(PatientsStorage.getInstance().GetAll());
            patients.Add(new Patient("---None---"));
            cbPatient.ItemsSource = patients;
            disableOtherInputFields();
            dpDate.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddYears(-1), DateTime.Now.AddDays(-1)));
        }

        private void disableOtherInputFields()
        {
            cbDoctor.IsEnabled = false;
            dpDate.IsEnabled = false;
            cbRoom.IsEnabled = false;
            cbTime.IsEnabled = false;
            cbDoctor.SelectedIndex = -1;
            dpDate.SelectedDate = null;
            cbRoom.SelectedIndex = -1;
            cbTime.SelectedIndex = -1;
        }

        public ObservableCollection<Doctor> filterDoctors(ObservableCollection<Doctor> doctors)
        {
            ObservableCollection<Doctor> finalDoctors = new ObservableCollection<Doctor>();

            if(currentPatient != null)
            {
                foreach (Refferal refferal in refferals)
                    if (currentPatient.Id == refferal.PatientId && refferal.Status == Refferal.STATUS.Active)
                        foreach (Doctor doctor in doctors)
                            if (doctor.Id == refferal.DoctorId)
                                finalDoctors.Add(doctor);

                foreach (Doctor doctor in doctors)
                    if (doctor.Specialization.Equals("General medicine"))
                        finalDoctors.Add(doctor);
            }
            
            return finalDoctors;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (cbDoctor.SelectedItem != null && cbRoom.SelectedItem != null && dpDate.SelectedDate != null && cbTime.SelectedItem != null)
            {
                Appointment appt = new Appointment();
                appt.Id = AppointmentStorage.getInstance().GenerateNewID();
                appt.Patient = currentPatient;
                appt.Doctor = (Doctor)cbDoctor.SelectedItem;
                appt.Room = (Room)cbRoom.SelectedItem;
                appt.Date = (DateTime)dpDate.SelectedDate;
                appt.Time = Convert.ToDateTime((string)cbTime.SelectedItem);
                Room selectedRoom = (Room)cbRoom.SelectedItem;
                _ = selectedRoom.Name == "Ordination" ? appt.IsOperation = false : appt.IsOperation = true;
                AppointmentStorage.getInstance().Add(appt);
                this.Close();
            }
            else
                MessageBox.Show("You need first to fill all input fields.");
        }

        private void patientChanged(object sender, System.EventArgs e)
        {
            Patient selectedPatient = (Patient)cbPatient.SelectedItem;
            if (selectedPatient != null)
            {
                if (selectedPatient.FirstName == "---None---")
                    cbPatient.SelectedIndex = -1;
                else
                    currentPatient = selectedPatient;

                cbDoctor.ItemsSource = filterDoctors(DoctorStorage.getInstance().GetAll());
            }
            disableOtherInputFields();
            cbDoctor.IsEnabled = true;
        }

        private void doctorChanged(object sender, System.EventArgs e)
        {
            filter();
            displayTerms();
            dpDate.IsEnabled = true;
            cbTime.IsEnabled = false;
            cbRoom.IsEnabled = false;
            dpDate.SelectedDate = null;
            cbRoom.SelectedIndex = -1;
            cbTime.SelectedIndex = -1;
        }

        private void dateChanged(object sender, System.EventArgs e)
        {
            filter();
            displayTerms();
            cbTime.IsEnabled = true;
            cbRoom.IsEnabled = false;
            cbRoom.SelectedIndex = -1;
            cbTime.SelectedIndex = -1;
            if (cbTime.Items.Count == 0 && dpDate.SelectedDate != null)
                MessageBox.Show("There is not any available term for this doctor on this day.");
        }

        private void filter()
        {
            if (cbDoctor.SelectedItem != null && dpDate.SelectedDate != null)
            {
                collectionAppointments.Filter = (e) =>
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
            List<string> occupiedTerms = new List<string>();
            cbTime.Items.Clear();

            foreach (Appointment a in collectionAppointments)
            {
                occupiedTerms.Add(a.Time.ToString("HH:mm"));
            }

            foreach (string s in terms)
            {
                Doctor selectedDoctor = (Doctor)cbDoctor.SelectedItem;
                if(dpDate.SelectedDate != null && cbDoctor.SelectedItem != null)
                    if (!occupiedTerms.Contains(s) && !selectedDoctor.FreeDays.Contains((DateTime)dpDate.SelectedDate) && doctorIsInHospital(selectedDoctor, s))
                        cbTime.Items.Add(s);
            }
        }

        private bool doctorIsInHospital(Doctor selectedDoctor, string term)
        {
            ListCollectionView shiftsCollection = new ListCollectionView(WorkingShiftStorage.getInstance().GetAll());
            shiftsCollection.Filter = (shift) =>
            {
                WorkingShift workingShift = shift as WorkingShift;
                if (workingShift.DoctorId == selectedDoctor.Id && (workingShift.StartDate <= dpDate.SelectedDate && workingShift.EndDate >= dpDate.SelectedDate) &&
                    ((((DateTime)Convert.ToDateTime(term)).TimeOfDay >= workingShift.StartTime.TimeOfDay && ((DateTime)Convert.ToDateTime(term)).TimeOfDay <= workingShift.EndTime.TimeOfDay) ||
                    (((DateTime)Convert.ToDateTime(term)).TimeOfDay >= workingShift.StartTime.TimeOfDay && ((DateTime)Convert.ToDateTime(term)).TimeOfDay <= workingShift.EndTime.TimeOfDay)))
                    return true;
                return false;
            };

            if (shiftsCollection.Count == 0)
                return false;
            else
                return true;
        }

        private void timeChanged(object sender, System.EventArgs e)
        {
            displayRooms();
            cbRoom.IsEnabled = true;
            cbRoom.SelectedIndex = -1;
            if (cbRoom.Items.Count == 0 && cbTime.SelectedItem != null)
                MessageBox.Show("There is not any available room for this doctor on this day in this room.");
        }

        private void displayRooms()
        {
            List<Room> occupiedRooms = new List<Room>();
            cbRoom.Items.Clear();

            if (cbTime.SelectedItem != null)
            {
                fillListOfOccupiedRooms(occupiedRooms);
                fillCbRoomsWithAvailableRooms(occupiedRooms);
            }
        }

        private void fillCbRoomsWithAvailableRooms(List<Room> occupiedRooms)
        {
            foreach (Room room in RoomStorage.getInstance().GetAll())
                if (!occupiedRooms.Contains(room))
                    cbRoom.Items.Add(room);
        }

        private void fillListOfOccupiedRooms(List<Room> occupiedRooms)
        {
            foreach (Appointment a in AppointmentStorage.getInstance().GetAll())
                if (a.Date == (DateTime)dpDate.SelectedDate && a.Time.ToString("HH:mm") == cbTime.SelectedItem.ToString())
                    occupiedRooms.Add(a.Room);
        }
    }
}
