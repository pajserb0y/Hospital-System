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
    /// Interaction logic for NewExam.xaml
    /// </summary>
    public partial class NewExam : Window
    {
        //Patient currentPatient;
        ListCollectionView collectionAppointments = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
        List<string> terms = new List<string> { "07:00", "07:30", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30",
                                                "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30" };
        public NewExam()
        {
            InitializeComponent();
            //currentPatient = selectedPatient;
            //txtPatient.Text = selectedPatient.ToString();
            cbPatient.ItemsSource = PatientsStorage.getInstance().GetAll();
            cbDoctor.ItemsSource = DoctorStorage.getInstance().GetAll();
        }


        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            Examination appt = new Examination();
            appt.Id = AppointmentStorage.getInstance().GenerateNewID();
            appt.Patient = (Patient) cbPatient.SelectedItem;
            appt.Doctor = (Doctor)cbDoctor.SelectedItem;
            appt.Room = RoomStorage.getInstance().GetOne(8);
            appt.Date = (DateTime)dpDate.SelectedDate;
            appt.Time = Convert.ToDateTime((string)cbTime.SelectedItem);
            Room selectedRoom = (Room)cbRoom.SelectedItem;
            _ = selectedRoom.Name == "Ordination" ? appt.IsOperation = false : appt.IsOperation = true;
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
            if (cbDoctor.SelectedItem != null && dpDate.SelectedDate != null)
            {
                collectionAppointments.Filter = (e) =>
                {
                    Appointment temp = e as Appointment;
                    if (temp.Doctor == (Doctor)cbDoctor.SelectedItem && temp.Date == (DateTime)dpDate.SelectedDate && temp.Room == (Room)cbRoom.SelectedItem)
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
                if (!occupiedTerms.Contains(s))
                    cbTime.Items.Add(s);
            }
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
