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
                                                "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30" };
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

                if(specializationAlreadyInList is false)
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

        DateTime RoundUp(DateTime dt, TimeSpan d)   //zaokruzivanje datuma na prvi gore za 30min
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }

        private void specializationChanged(object sender, System.EventArgs e)
        {
            filterDoctors();
            //filterRooms();
            //displayTerms();
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
                            if (tempDoctor == nearTerms.Doctor && (DateTime)nearTerms.Date == (DateTime)dpTime.SelectedDate)   //appointmenti sa tim doktorom tog datuma
                                if (RoundUp(DateTime.Now, TimeSpan.FromMinutes(30)).ToString("HH:mm") == nearTerms.Time.ToString("HH:mm") || 
                                RoundUp(DateTime.Now.AddMinutes(30), TimeSpan.FromMinutes(30)).ToString("HH:mm") == nearTerms.Time.ToString("HH:mm"))
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
                            txtTime.Text = RoundUp(DateTime.Now, TimeSpan.FromMinutes(30)).ToString("HH:mm");
                            return true;
                        }
                    }
                    foreach (var occupiedDoctor in occupied)
                    {
                        int k = 0;
                        if (occupiedDoctor.Split(',')[1] == RoundUp(DateTime.Now.AddMinutes(30), TimeSpan.FromMinutes(30)).ToString("HH:mm"))     //gledam da li onda postoji neki doktor kome je zauzet samo drugi termin
                        {
                            foreach (var o in occupied)     //gledam da li postoji slobodan prvi termin
                                if (o.Split(',')[0] == occupiedDoctor.Split(',')[0] && o.Split(',')[1] == RoundUp(DateTime.Now, TimeSpan.FromMinutes(30)).ToString("HH:mm")) //ovde jos i moze ono
                                    k = 1;
                            if (k == 0 && occupiedDoctor.Split(',')[0] == tempDoctor.Id.ToString())
                            {
                                txtTime.Text = RoundUp(DateTime.Now, TimeSpan.FromMinutes(30)).ToString("HH:mm");
                                return true;
                            }
                        }

                        else if (occupiedDoctor.Split(',')[1] == RoundUp(DateTime.Now, TimeSpan.FromMinutes(30)).ToString("HH:mm"))    //gledam da li onda postoji neki doktor koji ima slobodan barem drugi termin
                        {
                            foreach (var o in occupied)     //gledam da li postoji slobodan drugi termin
                                if (o.Split(',')[0] == occupiedDoctor.Split(',')[0] && o.Split(',')[1] == RoundUp(DateTime.Now.AddMinutes(30), TimeSpan.FromMinutes(30)).ToString("HH:mm"))
                                    k = 2;
                            if (k == 0 && occupiedDoctor.Split(',')[0] == tempDoctor.Id.ToString())
                            {
                                txtTime.Text = RoundUp(DateTime.Now.AddMinutes(30), TimeSpan.FromMinutes(30)).ToString("HH:mm");
                                return true;
                            }
                        }

                        else
                        {
                            //TODO: slucaj kada ne postoji slobodan termin
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
            exam.Date = (DateTime)dpTime.SelectedDate;
            exam.Time = Convert.ToDateTime(txtTime.Text);
            ExaminationStorage.getInstance().checkExamination(exam);
            ExaminationStorage.getInstance().Add(exam);

            this.Close();
        }
    }
}
