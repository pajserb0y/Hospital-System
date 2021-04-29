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
    /// Interaction logic for UrgentPatient.xaml
    /// </summary>
    public partial class UrgentPatient : Window
    {
        List<string> terms = new List<string> { "07:00", "07:30", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30",
                                                "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30"};
        ListCollectionView doctorCollection = new ListCollectionView(DoctorStorage.getInstance().GetAll());
        ListCollectionView specializationCollection = new ListCollectionView(DoctorStorage.getInstance().GetAll());
        ListCollectionView appointmentCollection = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
        ListCollectionView roomCollection = new ListCollectionView(RoomStorage.getInstance().GetAll());
        public UrgentPatient()
        {
            InitializeComponent();
            cbPatient.ItemsSource = PatientsStorage.getInstance().GetAll();
            //cbRoom.ItemsSource = RoomStorage.getInstance().GetAll();
            txtTime.Text = DateTime.Now.ToString("HH:mm");

            // pronalaze se sve specijalizacije ciji doktori rade u bolnici i ispisuju se samo jednom (distinct)
            List<Doctor> doctorsWithDifferentSpecialization = new List<Doctor>();
            specializationCollection.Filter = (doc) =>
            {
                bool specializationAlreadyInList = false;
                Doctor tempDoc = doc as Doctor;
                if (doctorsWithDifferentSpecialization.Count <= 0)
                {
                    doctorsWithDifferentSpecialization.Add(tempDoc);
                    return true;
                }
                foreach (Doctor dr in doctorsWithDifferentSpecialization)
                    if (tempDoc.Specialization == dr.Specialization)
                        specializationAlreadyInList = true;

                if (specializationAlreadyInList is false)
                {
                    doctorsWithDifferentSpecialization.Add(tempDoc);
                    return true;
                }
                return false;
            };
            cbSpecialization.ItemsSource = specializationCollection;
            cbSpecialization.SelectedIndex = -1;

            cbDoctor.SelectedIndex = -1;
            hideAppointmentDetails();
        }

        private void showAppointmentDetails()
        {
            cbDoctor.Visibility = Visibility.Visible;
            cbRoom.Visibility = Visibility.Visible;
            dpDate.Visibility = Visibility.Visible;
            txtTime.Visibility = Visibility.Visible;
            cbOperation.Visibility = Visibility.Visible;
            labelDoctor.Visibility = Visibility.Visible;
            labelRoom.Visibility = Visibility.Visible;
            labelDate.Visibility = Visibility.Visible;
            labelTime.Visibility = Visibility.Visible;
        }
        private void hideAppointmentDetails()
        {
            cbDoctor.Visibility = Visibility.Hidden;
            cbRoom.Visibility = Visibility.Hidden;
            dpDate.Visibility = Visibility.Hidden;
            txtTime.Visibility = Visibility.Hidden;
            cbOperation.Visibility = Visibility.Hidden;
            labelDoctor.Visibility = Visibility.Hidden;
            labelRoom.Visibility = Visibility.Hidden;
            labelDate.Visibility = Visibility.Hidden;
            labelTime.Visibility = Visibility.Hidden;
        }

        DateTime RoundUp(DateTime dt, TimeSpan d)   //zaokruzivanje datuma na prvi gore za 30min
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }
        private string findSecondNearestTerm()
        {
            return RoundUp(DateTime.Now.AddMinutes(30), TimeSpan.FromMinutes(30)).ToString("HH:mm");
        }

        private string findFirstNearestTerm()
        {
            return RoundUp(DateTime.Now, TimeSpan.FromMinutes(30)).ToString("HH:mm");
        }

        private void specializationChanged(object sender, System.EventArgs e)
        {
            filterDoctors();
            filterRooms();
            cbDoctor.SelectedIndex = 0;
            cbRoom.SelectedIndex = 0;
            showAppointmentDetails();            
        }
        private void filterDoctors()
        {
            dgApp.Visibility = Visibility.Hidden;
            List<string> occupied = new List<string>();     //zauzeti doktorID + , + termin ukoliko postoji u narednih pola sata/sat
            List<string> wantedDoctorsID = new List<string>();  //doktori sa trazenom specijalizacijom
            if (cbSpecialization.SelectedItem != null)
            {
                doctorCollection.Filter = (e) =>
                {
                    Doctor tempDoctor = e as Doctor;
                    if (tempDoctor.Specialization == cbSpecialization.SelectedItem.ToString())    //svi doktori sa tom specijalizacijom
                    {
                        wantedDoctorsID.Add(tempDoctor.Id.ToString());
                        foreach (Appointment nearTerms in AppointmentStorage.getInstance().GetAll())    //trazim appointmente za taj dan i ukoliko postoje zakazani appointmenti u roku od pola sata/sat onda njih dodajem u listu occupied
                            if (tempDoctor == nearTerms.Doctor && (DateTime)nearTerms.Date == (DateTime)dpDate.SelectedDate)   //appointmenti sa tim doktorom tog datuma
                                if (findFirstNearestTerm() == nearTerms.Time.ToString("HH:mm") || findSecondNearestTerm() == nearTerms.Time.ToString("HH:mm"))
                                    occupied.Add(tempDoctor.Id.ToString() + "," + nearTerms.Time.ToString("HH:mm"));
                    }
                    List<string> occupiedDoctorIDs = new List<string>();    //lista svih id-eva doktora koji imaju termine blizu
                    foreach (var a in occupied)
                        occupiedDoctorIDs.Add(a.Split(',')[0]);

                    //gledamo da li mozda postoji neki doktor koji nema ni jedan termin u blizini zauzet
                    foreach (var wantedDoctor in wantedDoctorsID)
                    {
                        if (!occupiedDoctorIDs.Contains(wantedDoctor) && wantedDoctor == tempDoctor.Id.ToString())
                        {
                            txtTime.Text = findFirstNearestTerm();
                            return true;
                        }
                    }


                    foreach (var occupiedDoctor in occupied)
                    {
                        int k = 0;
                        //gledam da li onda postoji neki doktor kome je zauzet samo drugi termin
                        if (occupiedDoctor.Split(',')[1] == findSecondNearestTerm())     
                        {
                            foreach (var o in occupied)     //gledam da li postoji slobodan prvi termin
                                if (o.Split(',')[0] == occupiedDoctor.Split(',')[0] && o.Split(',')[1] == findFirstNearestTerm()) //ovde jos i moze ono
                                    k = 1;
                            if (k == 0 && occupiedDoctor.Split(',')[0] == tempDoctor.Id.ToString())
                            {
                                txtTime.Text = findFirstNearestTerm();
                                return true;
                            }
                        }

                        //gledam da li onda postoji neki doktor koji ima slobodan barem drugi termin
                        else if (occupiedDoctor.Split(',')[1] == findFirstNearestTerm())    
                        {
                            foreach (var o in occupied)     //gledam da li postoji slobodan drugi termin
                                if (o.Split(',')[0] == occupiedDoctor.Split(',')[0] && o.Split(',')[1] == findSecondNearestTerm())
                                    k = 2;
                            if (k == 0 && occupiedDoctor.Split(',')[0] == tempDoctor.Id.ToString())
                            {
                                txtTime.Text = findSecondNearestTerm();
                                return true;
                            }
                        }                               
                    }
                    return false;
                };

            }          
            cbDoctor.ItemsSource = doctorCollection;


            //trazim one termine koje moram da pomerim, jer nema slobodnih
            if(occupied.Count == (wantedDoctorsID.Count * 2) && occupied.Count > 0)
            {
                hideAppointmentDetails();
                dgApp.Visibility = Visibility.Visible;
                appointmentCollection.Filter = (e) =>
                {
                    Appointment tempApp = e as Appointment;
                    if (wantedDoctorsID.Contains(tempApp.Doctor.Id.ToString()) && (DateTime)tempApp.Date == (DateTime)dpDate.SelectedDate &&
                        (tempApp.Time.ToString("HH:mm") == findFirstNearestTerm() || tempApp.Time.ToString("HH:mm") == findSecondNearestTerm()))    //svi termini tih doktora tog datuma koji vaze za naredna dva termina
                        return true;
                    return false;
                };

                List<Appointment> sortedAppointmentCollection = new List<Appointment>();    //sortiram listu tih zauzetih appointmenta po tome koji se mogu odloziti za ranije
                //Appointment appointmentInNewList = null;
                Appointment bestI = (Appointment)appointmentCollection.GetItemAt(0);    //nisam siguran da li mi treba inicijalizacija za ove bestove
                Appointment bestJ = (Appointment)appointmentCollection.GetItemAt(0);
                for (int i = 0; i < appointmentCollection.Count ; i++)
                {
                    bestI = (Appointment)appointmentCollection.GetItemAt(i);
                    Appointment appointmentInNewList = bestI;
                    for (int j = i + 1; j < appointmentCollection.Count; j++)
                    {
                        bestJ = (Appointment)appointmentCollection.GetItemAt(j);
                        if (DateTime.Compare(findNearestAvailableTermForDoctor(bestJ.Doctor), findNearestAvailableTermForDoctor(appointmentInNewList.Doctor)) == -1)      //da li doktora od J app ima slobodan termin pre doktora od I app
                        {
                            appointmentInNewList = bestJ;
                            appointmentCollection.Remove(appointmentInNewList);  //nema sanse da ovo radi, treba da se pomeri izvan ovog for-a (u if za sorted)
                            i = -1;
                        }                            
                    }
                    if (appointmentInNewList != null)   //treba da prodjem kroz sve termine koji su best i da ih sortiram po najblizim (svakako kad sekretar vidi istog doktora moze izabrati taj najblizi)
                        sortedAppointmentCollection.Add(appointmentInNewList);
                }

                //List<Appointment> sortedAppointmentCollectionAgain = new List<Appointment>();
                //for (int i = 0; i < sortedAppointmentCollection.Count; i += 2)      //sortiranje termina po najblizim koje treba da pomeri
                //{
                //    Appointment app1 = (Appointment)appointmentCollection.GetItemAt(i);
                //    Appointment app2 = (Appointment)appointmentCollection.GetItemAt(i+1);
                //    if (DateTime.Compare(app1.Time, app2.Time) == -1)
                //    {
                //        sortedAppointmentCollectionAgain.Add(app1);
                //        sortedAppointmentCollectionAgain.Add(app2);
                //    }
                //    else
                //    {
                //        sortedAppointmentCollectionAgain.Add(app2);
                //        sortedAppointmentCollectionAgain.Add(app1);
                //    }
                //}

                dgApp.ItemsSource = sortedAppointmentCollection;
            }
        }

        private DateTime findNearestAvailableTermForDoctor(Doctor doctor)
        {
            ListCollectionView newAppointmentCollection = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
            List<string> newOccupied = new List<string>();
            newAppointmentCollection.Filter = (e) =>
            {
                Appointment tempApp = e as Appointment;
                if ((doctor == tempApp.Doctor) && (DateTime.Compare((DateTime)tempApp.Date, (DateTime)dpDate.SelectedDate) >= 0))   //svi termini tog doktora nakon trenutnog datuma
                {
                    newOccupied.Add(tempApp.Date.ToString() + "," + tempApp.Time.ToString("HH:mm"));       //lista datuma i vremena zauzetih termina tog doktora u terminima nakon trenutnnog
                    return true;
                }

                return false;
            };

            DateTime dateTemp = (DateTime)dpDate.SelectedDate;
            while (true)        //idem redom po datumima pa ce mi s toga prvi dobar na koji naidjem ujedno biti i najblizi slobodan
            {
                List<string> allTermsOnThisDate = new List<string>();
                foreach (string s in terms)     //dobijem za svaki datum sve termine  
                {
                    if (DateTime.Compare(dateTemp, (DateTime)dpDate.SelectedDate) == 0)     //ovu logiku mogu skroz preneti gore na 242 liniju koda gde pravim ovu listu newOccupied
                    {
                        if (DateTime.Compare((DateTime)Convert.ToDateTime(findSecondNearestTerm()), (DateTime)Convert.ToDateTime(s)) == -1)
                            allTermsOnThisDate.Add(dateTemp.ToString() + "," + s.ToString());
                    }
                    else
                        allTermsOnThisDate.Add(dateTemp.ToString() + "," + s.ToString());                     
                }


                List<string> availableTermsOnThisDate = new List<string>();
                foreach (string s in allTermsOnThisDate)
                    if (!newOccupied.Contains(s))
                        availableTermsOnThisDate.Add(s);    //pronasao najblizi i uradim break??? i onda mi ovo dole nista ne treba, jer mi je lista allTermsOnThisDate vec sortirana od najblizeg ka najdaljem

                if (availableTermsOnThisDate.Count != 0)
                {
                    DateTime min = Convert.ToDateTime("18:30");

                    foreach (string term in availableTermsOnThisDate)
                        if (DateTime.Compare((DateTime)Convert.ToDateTime(term.Split(',')[1]), min) < 0)
                            min = (DateTime)Convert.ToDateTime(term.Split(',')[1]);

                    return min;
                }

                dateTemp.AddDays(1);
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Appointment a = (Appointment)dgApp.SelectedItem;
            txtNext.Text = findNearestAvailableTermForDoctor(a.Doctor).ToString();
        }
        
        private void CheckBoxOperationChanged(object sender, RoutedEventArgs e)
        {
            filterRooms();
        }

        private void filterRooms()
        {
            ListCollectionView appointmentCollection = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
            List<Room> occupiedRooms = new List<Room>();
            string roomName = "Ordination";
            if (cbOperation.IsChecked == true)
                roomName = "Operation room";              

            
            foreach (Appointment tempApp in AppointmentStorage.getInstance().GetAll())
                if (tempApp.Date == dpDate.SelectedDate && tempApp.Time.ToString("HH:mm") == txtTime.Text && tempApp.Room.Name == roomName)
                    occupiedRooms.Add(tempApp.Room);

            roomCollection.Filter = (e) =>
            {
                Room tempRoom = e as Room;
                if (!occupiedRooms.Contains(tempRoom) && tempRoom.Name == roomName)
                    return true;
                return false;
            };

            if (roomCollection == null)
            {
                hideAppointmentDetails();
                dgApp.Visibility = Visibility.Visible;
                appointmentCollection.Filter = (e) =>
                {
                    Appointment tempApp = e as Appointment;
                    if (occupiedRooms.Contains(tempApp.Room))
                        return true;
                    return false;
                };
            }

            cbRoom.ItemsSource = roomCollection;
        }


        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Patient patient = new Patient();
            if (cbPatient.SelectedItem == null)
            {
                patient = new Patient(PatientsStorage.getInstance().GenerateNewID(), "", "", 0, default, "", 0, "", true, "", "", default(DateTime), "", 0, "", "");
                PatientsStorage.getInstance().Add(patient);
            }
            else
                patient = (Patient)cbPatient.SelectedItem;

            Appointment app = new Appointment();
            app.Id = AppointmentStorage.getInstance().GenerateNewID();
            app.Patient = patient;
            app.Doctor = (Doctor)cbDoctor.SelectedItem;
            app.Room = (Room)cbRoom.SelectedItem;
            app.Date = (DateTime)dpDate.SelectedDate;
            app.Time = Convert.ToDateTime(txtTime.Text);
            app.IsOperation = (bool)cbOperation.IsChecked;
            AppointmentStorage.getInstance().Add(app);

            this.Close();
        }
    }
}
