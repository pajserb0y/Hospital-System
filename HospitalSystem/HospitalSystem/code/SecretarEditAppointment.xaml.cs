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
    /// Interaction logic for SecretarEditAppointment.xaml
    /// </summary>
    public partial class SecretarEditAppointment : Window
    {
        bool fromConstructor;
        private Appointment currentAppointment;
        ObservableCollection<Refferal> refferals = RefferalStorage.getInstance().GetAll();
        ListCollectionView collectionAppointments = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
        List<string> terms = new List<string> { "07:00", "07:30", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30",
                                                "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30" };

        public SecretarEditAppointment(Appointment selectedApp)
        {
            currentAppointment = selectedApp;
            InitializeComponent();
            fromConstructor = true;
            cbDoctor.ItemsSource = filterDoctors(DoctorStorage.getInstance().GetAll());
            initializeSelectedAppointmentDetails(selectedApp);
            dpDate.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddYears(-1), DateTime.Now.AddDays(-1)));
        }

        public ObservableCollection<Doctor> filterDoctors(ObservableCollection<Doctor> doctors)
        {
            ObservableCollection<Doctor> finalDoctors = new ObservableCollection<Doctor>();
            finalDoctors.Add(currentAppointment.Doctor);
            findDoctorsWithRefferal(doctors, finalDoctors);
            findGeneralMedicineDoctors(doctors, finalDoctors);

            return finalDoctors;
        }

        private static void findGeneralMedicineDoctors(ObservableCollection<Doctor> doctors, ObservableCollection<Doctor> finalDoctors)
        {
            foreach (Doctor doctor in doctors)
                if (doctor.Specialization.Equals("General medicine"))
                    finalDoctors.Add(doctor);
        }

        private void findDoctorsWithRefferal(ObservableCollection<Doctor> doctors, ObservableCollection<Doctor> finalDoctors)
        {
            foreach (Refferal refferal in refferals)
                if (currentAppointment.Patient.Id == refferal.PatientId && refferal.Status == Refferal.STATUS.Active)
                    foreach (Doctor doctor in doctors)
                        if (doctor.Id == refferal.DoctorId && !finalDoctors.Contains(doctor))
                            finalDoctors.Add(doctor);
        }

        private void initializeSelectedAppointmentDetails(Appointment selectedApp)
        {
            txtPatient.Text = selectedApp.Patient.ToString();
            cbDoctor.SelectedItem = selectedApp.Doctor;
            dpDate.SelectedDate = selectedApp.Date;
            cbTime.Items.Add(selectedApp.Time.ToString("HH:mm"));
            cbTime.SelectedItem = selectedApp.Time.ToString("HH:mm");
            cbRoom.Items.Add(selectedApp.Room);
            cbRoom.SelectedItem = selectedApp.Room;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (cbDoctor.SelectedItem != null && cbRoom.SelectedItem != null && dpDate.SelectedDate != null && cbTime.SelectedItem != null)
            {
                double differenceInDays = Math.Abs(currentAppointment.Date.Subtract((DateTime)dpDate.SelectedDate).TotalDays);
                if (differenceInDays <= 2)
                {
                    currentAppointment.Doctor = (Doctor)cbDoctor.SelectedItem;
                    currentAppointment.Room = (Room)cbRoom.SelectedItem;
                    currentAppointment.Date = (DateTime)dpDate.SelectedDate;
                    currentAppointment.Time = Convert.ToDateTime((string)cbTime.SelectedItem);
                    Room selectedRoom = (Room)cbRoom.SelectedItem;
                    _ = selectedRoom.Name == "Ordination" ? currentAppointment.IsOperation = false : currentAppointment.IsOperation = true;
                    AppointmentStorage.getInstance().Edit(currentAppointment);
                    this.Close();
                }
                else
                {
                    dpDate.SelectedDate = currentAppointment.Date;
                    MessageBox.Show("Invalid date! Must be within 2 days of the selected appointment.");
                }
            }
            else
            {
                MessageBox.Show("You need first to fill all input fields.");
            }
        }

        private void Window_Closed()
        {
            //JobStorage.getInstance().serialize();
            this.Close();
        }

        private void doctorChanged(object sender, System.EventArgs e)
        {
            if (!fromConstructor)
            {
                filter();
                if (displayTerms() == 1)
                    Window_Closed();
            }
        }

        private void dateChanged(object sender, System.EventArgs e)
        {
            if (!fromConstructor)
            {
                filter();
                if (displayTerms() == 1)
                    Window_Closed();
                fromConstructor = false;
            }
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

        private int displayTerms()
        {
            if (cbDoctor.SelectedItem != null && dpDate.SelectedDate != null)
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
                    if (dpDate.SelectedDate <= DateTime.Now.Date)
                    {
                        MessageBox.Show("Changing past appointment is not allowed!");
                        //this.Close();
                        return 1;
                    }
                    if (!occupiedTerms.Contains(s) && !selectedDoctor.FreeDays.Contains((DateTime)dpDate.SelectedDate) && doctorIsInHospital(selectedDoctor, s))
                        cbTime.Items.Add(s);
                }
                return 0;
            }
            return 0;
        }

        private bool doctorIsInHospital(Doctor selectedDoctor, string term)
        {
            ListCollectionView shiftsCollection = new ListCollectionView(WorkingShiftStorage.getInstance().GetAll());
            shiftsCollection.Filter = (shift) =>
            {
                WorkingShift workingShift = shift as WorkingShift;
                if (workingShift.DoctorId == selectedDoctor.Id && (workingShift.StartDate <= dpDate.SelectedDate && workingShift.EndDate >= dpDate.SelectedDate) &&
                    (((DateTime)Convert.ToDateTime(term) >= workingShift.StartTime && (DateTime)Convert.ToDateTime(term) <= workingShift.EndTime) ||
                    ((DateTime)Convert.ToDateTime(term) >= workingShift.StartTime && (DateTime)Convert.ToDateTime(term) <= workingShift.EndTime)))
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
