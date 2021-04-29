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


            //doctorCollection.Filter = (doc) =>
            //{
            //    Doctor tempDoc = doc as Doctor;
            //    foreach(Appointment app in AppointmentStorage.getInstance().GetAll())
            //        if(app.Doctor != tempDoc)
            //            return true;
            //    return false;
            //};
            //cbDoctor.ItemsSource = doctorCollection;
            //cbDoctor.SelectedIndex = -1;


            // pronalaze se sve sspecijalizacije ciji doktori rade u bolnici i ispisuju se samo jednom (distinct)
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
            
        }

        private void showAppointmentDetails()
        {
            cbDoctor.Visibility = Visibility.Visible;
            cbRoom.Visibility = Visibility.Visible;
            dpDate.Visibility = Visibility.Visible;
            txtTime.Visibility = Visibility.Visible;
            cbOperation.Visibility = Visibility.Visible;
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
            //filterRooms();
            //displayTerms();
            showAppointmentDetails();
        }
        private void filterDoctors()
        {
            List<string> occupied = new List<string>();
            List<string> wantedDoctorsID = new List<string>();
            if (cbSpecialization.SelectedItem != null)
            {
                doctorCollection.Filter = (e) =>
                {
                    int countOfNearTerms = 0;
                    Doctor tempDoctor = e as Doctor;
                    if (tempDoctor.Specialization == cbSpecialization.SelectedItem.ToString())    //svi doktori sa tom specijalizacijom
                    {
                        wantedDoctorsID.Add(tempDoctor.Id.ToString());
                        foreach (Appointment nearTerms in AppointmentStorage.getInstance().GetAll())
                            if (tempDoctor == nearTerms.Doctor && (DateTime)nearTerms.Date == (DateTime)dpDate.SelectedDate)   //appointmenti sa tim doktorom tog datuma
                                if (findFirstNearestTerm() == nearTerms.Time.ToString("HH:mm") || findSecondNearestTerm() == nearTerms.Time.ToString("HH:mm"))
                                    occupied.Add(tempDoctor.Id.ToString() + "," + nearTerms.Time.ToString("HH:mm"));
                        //else if (RoundUp(DateTime.Now.AddMinutes(30), TimeSpan.FromMinutes(30)).ToString("HH:mm") == nearTerms.Time.ToString("HH:mm"))  //da li su najbliza oba termina zauzeta
                        //    countOfNearTerms++;

                        //if (countOfNearTerms == 2)
                        //    return false;
                        //return true;
                    }
                    List<string> occupiedDoctorIDs = new List<string>();    //lista svi id-eva doktora koji imaju termine blizu
                    foreach (var a in occupied)
                        occupiedDoctorIDs.Add(a.Split(',')[0]);

                    foreach (var wantedDoctor in wantedDoctorsID)            //gledamo da li mozda postoji neki doktor koji nema ni jedan termin u blizini zauzet
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
                        if (occupiedDoctor.Split(',')[1] == findSecondNearestTerm())     //gledam da li onda postoji neki doktor kome je zauzet samo drugi termin
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

                        else if (occupiedDoctor.Split(',')[1] == findFirstNearestTerm())    //gledam da li onda postoji neki doktor koji ima slobodan barem drugi termin
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
            //foreach(Doctor suitableDoctor in doctorCollection)    //prolazim kroz listu doktora i gledam koji je termin najblizi
            //    foreach (Appointment nearTerms in AppointmentStorage.getInstance().GetAll())
            //        if (suitableDoctor == nearTerms.Doctor && (DateTime)nearTerms.Date == (DateTime)dpTime.SelectedDate)   //appointmenti sa tim doktorom tog datuma
            //            if (RoundUp(DateTime.Now, TimeSpan.FromMinutes(30)).ToString("HH:mm") == nearTerms.Time.ToString("HH:mm") ||
            //            RoundUp(DateTime.Now.AddMinutes(30), TimeSpan.FromMinutes(30)).ToString("HH:mm") == nearTerms.Time.ToString("HH:mm"))  //da li su najbliza oba termina zauzeta
            //                countOfNearTerms++;
            
            cbDoctor.ItemsSource = doctorCollection;

            //trazim one termine koje moram da pomerim, jer nema slobodnih
            if(occupied.Count == (wantedDoctorsID.Count * 2) && occupied.Count > 0)
            {
                dgApp.Visibility = Visibility.Visible;
                //TODO: slucaj kada ne postoji slobodan termin
                appointmentCollection.Filter = (e) =>
                {
                    Appointment tempApp = e as Appointment;
                    if (wantedDoctorsID.Contains(tempApp.Doctor.Id.ToString()) && (DateTime)tempApp.Date == (DateTime)dpDate.SelectedDate &&
                        (tempApp.Time.ToString("HH:mm") == findFirstNearestTerm() || tempApp.Time.ToString("HH:mm") == findSecondNearestTerm()))    //svi termini tih doktora tog datuma koji vaze za naredna dva termina
                        return true;
                    return false;
                };

                //sortiram listu tih zauzetih appointmenta po tome koji se mogu odloziti za ranije
                List<Appointment> sortedAppointmentCollection = new List<Appointment>();
                //Appointment appointmentInNewList = null;
                Appointment bestI = (Appointment)appointmentCollection.GetItemAt(0);
                Appointment bestJ = (Appointment)appointmentCollection.GetItemAt(0);
                for (int i = 0; i < appointmentCollection.Count ; i++)
                {
                    //bestI = (Appointment)appointmentCollection.GetItemAt(i);
                    //Appointment appointmentInNewList = bestI;
                    //for (int j = i + 1; j < appointmentCollection.Count; j++)
                    //{     
                    //    bestJ = (Appointment)appointmentCollection.GetItemAt(j);
                    //    if (DateTime.Compare(findNearestAvailableTermForDoctor(bestJ.Doctor), findNearestAvailableTermForDoctor(bestI.Doctor)) < 1)      //da li doktora od J app ima slobodan termin pre doktora od I app
                    //        appointmentInNewList = bestJ;
                    //}
                    //if (appointmentInNewList != null)   //treba da prodjem kroz sve termine koji su best i da ih sortiram po najblizim
                    //    sortedAppointmentCollection.Add(appointmentInNewList);
                    bestI = (Appointment)appointmentCollection.GetItemAt(i);
                    Appointment appointmentInNewList = bestI;
                    for (int j = i + 1; j < appointmentCollection.Count; j++)
                    {
                        bestJ = (Appointment)appointmentCollection.GetItemAt(j);
                        if (DateTime.Compare(findNearestAvailableTermForDoctor(bestJ.Doctor), findNearestAvailableTermForDoctor(appointmentInNewList.Doctor)) == -1)      //da li doktora od J app ima slobodan termin pre doktora od I app
                        {
                            appointmentInNewList = bestJ;
                            appointmentCollection.Remove(appointmentInNewList);  //rotiranje u appointment collection
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
            //foreach (Appointment tempApp in newAppointmentCollection)
            //{
            //    if ((doctor == tempApp.Doctor) && (DateTime.Compare((DateTime)tempApp.Date, (DateTime)dpDate.SelectedDate) >= 0))
            //    {
            //        foreach (string s in terms)
            //        {
            //            if (!occupied.Contains(s))
            //                cbTime.Items.Add(s);
            //        }
            //    }
                    
            //}
            DateTime dateTemp = (DateTime)dpDate.SelectedDate;
            while (true)        //idem redom po datumima pa ce mi s toga prvi dobar na koji naidjem ujedno biti i najblizi slobodan
            {
                List<string> allTermsOnThisDate = new List<string>();
                List<string> availableTermsOnThisDate = new List<string>();
                foreach (string s in terms)
                {
                    if (DateTime.Compare(dateTemp, (DateTime)dpDate.SelectedDate) == 0)
                    {
                        if (DateTime.Compare((DateTime)Convert.ToDateTime(findSecondNearestTerm()), (DateTime)Convert.ToDateTime(s)) == -1)
                            allTermsOnThisDate.Add(dateTemp.ToString() + "," + s.ToString());
                    }
                    else
                        allTermsOnThisDate.Add(dateTemp.ToString() + "," + s.ToString());   //dobijem za jedan datum sve termine                    
                }
                    

                foreach (string s in allTermsOnThisDate)
                    if (!newOccupied.Contains(s))
                        availableTermsOnThisDate.Add(s);

                if(availableTermsOnThisDate.Count != 0)
                {
                    DateTime min = Convert.ToDateTime("18:30");

                    foreach (string term in availableTermsOnThisDate)
                        if (DateTime.Compare((DateTime)Convert.ToDateTime(term.Split(',')[1]), min) < 0)
                            min = (DateTime)Convert.ToDateTime(term.Split(',')[1]);

                    return min;
                    // var min = 18:30
                    // prodjem kroz celu availableTermsOnThisDate i pronadjem najmanje vreme
                    // return to vreme
                }

                dateTemp.AddDays(1);
            }

            //return default;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Appointment a = (Appointment)dgApp.SelectedItem;
            txtNext.Text = findNearestAvailableTermForDoctor(a.Doctor).ToString();
        }

        private void filterRooms()
        {
            if (cbSpecialization.SelectedItem != null)
            {
                roomCollection.Filter = (e) =>
                {
                    Doctor temp = e as Doctor;
                    if (temp.Specialization == cbSpecialization.SelectedItem.ToString())
                        return true;
                    return false;
                };
            }
            cbRoom.ItemsSource = roomCollection;
        }
        //private void displayTerms()
        //{
        //    List<string> occupied = new List<string>();
        //    cbTime.Items.Clear();

        //    foreach (Appointment a in appointmentCollection)
        //    {
        //        occupied.Add(a.Time.ToString("HH:mm"));
        //    }

        //    foreach (string s in terms)
        //    {
        //        if (!occupied.Contains(s))
        //            cbTime.Items.Add(s);
        //    }
        //}


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

            Examination exam = new Examination();
            exam.Id = ExaminationStorage.getInstance().GenerateNewID();
            exam.Patient = patient;
            exam.Doctor = (Doctor)cbDoctor.SelectedItem;
            exam.Room = (Room)cbRoom.SelectedItem;
            exam.Date = (DateTime)dpDate.SelectedDate;
            exam.Time = Convert.ToDateTime(txtTime.Text);
            ExaminationStorage.getInstance().checkExamination(exam);
            ExaminationStorage.getInstance().Add(exam);

            this.Close();
        }
    }
}
