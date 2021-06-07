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
    /// Interaction logic for PatientDetails.xaml
    /// </summary>
    public partial class PatientDetails : Window
    {
        ListCollectionView collectionViewExam = new ListCollectionView(ExaminationStorage.getInstance().GetAll());
        ListCollectionView collectionViewRefferals = new ListCollectionView(RefferalStorage.getInstance().GetAll());
        ListCollectionView collectionViewOperation = new ListCollectionView(AppointmentStorage.getInstance().GetAll());

        private Patient currentPatient;

        public PatientDetails(Patient selectedPatient)
        {
            InitializeComponent();
            currentPatient = selectedPatient;

            txtID.Text = currentPatient.Id.ToString();
            txtIme.Text = currentPatient.FirstName;
            txtPrezime.Text = currentPatient.LastName;
            txtJmbg.Text = currentPatient.Jmbg.ToString();
            txtAdress.Text = currentPatient.Adress;
            txtTel.Text = currentPatient.Telephone.ToString();
            txtEmail.Text = currentPatient.Email;
            _ = currentPatient.Gender == 'M' ? rbM.IsChecked = true : rbF.IsChecked = true;
            _ = currentPatient.Guest == true ? cbGuest.IsChecked = true : cbGuest.IsChecked = false;

            dpBirth.SelectedDate = currentPatient.BirthDate;

            if (currentPatient.MarriageStatus == "Married")
                cbMarriage.SelectedIndex = 0;
            if (currentPatient.MarriageStatus == "Unmarried")
                cbMarriage.SelectedIndex = 1;
            if (currentPatient.MarriageStatus == "Divorced")
                cbMarriage.SelectedIndex = 2;
            if (currentPatient.MarriageStatus == "Widow(er)")
                cbMarriage.SelectedIndex = 3;

            txtSoc.Text = currentPatient.SocNumber.ToString();
            txtCity.Text = currentPatient.City;
            txtCountry.Text = currentPatient.Country;

            //collectionViewJob.Filter = (e) =>
            //{
            //    Job temp = e as Job;
            //    if (temp.PID == currentPatient.Id)
            //        return true;
            //    return false;
            //};
            //dgJob.ItemsSource = collectionViewJob;

            InitializeCollections();

            txtID.IsReadOnly = true; //PREBACI U XAML
            txtIme.IsReadOnly = true;
            txtPrezime.IsReadOnly = true;
            txtJmbg.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
            txtTel.IsReadOnly = true;
            txtAdress.IsReadOnly = true;
            txtSoc.IsReadOnly = true;
            txtCity.IsReadOnly = true;
            txtCountry.IsReadOnly = true;
            cbMarriage.IsEnabled = false;
            dpBirth.IsEnabled = false;
            rbF.IsEnabled = false;
            rbM.IsEnabled = false;
            cbGuest.IsEnabled = false;
            tExam.Visibility = Visibility.Collapsed;
            tRefferal.Visibility = Visibility.Collapsed;


        }

        private void InitializeCollections()
        {
            if (currentPatient != null)
            {
                collectionViewExam.Filter = (e) =>
                {
                    Examination temp = e as Examination;
                    if (temp.Patient == currentPatient)
                        return true;
                    return false;
                };
                dgPatientExams.ItemsSource = collectionViewExam;
            }

            if (currentPatient != null)
            {
                collectionViewOperation.Filter = (e) =>
                {
                    Appointment temp = e as Appointment;
                    if (temp.Patient == currentPatient && temp.IsOperation == true)
                        return true;
                    return false;
                };
                dgPatientOperations.ItemsSource = collectionViewOperation;
            }

            if (currentPatient != null)
            {
                collectionViewRefferals.Filter = (e) =>
                {
                    Refferal temp = e as Refferal;
                    if (temp.PatientId == currentPatient.Id)
                        return true;
                    return false;
                };
                dgPatientRefferals.ItemsSource = collectionViewRefferals;
            }
            collectionViewExam = new ListCollectionView(ExaminationStorage.getInstance().GetAll());
            if (currentPatient != null)
            {
                collectionViewExam.Filter = (e) =>
                {
                    Examination temp = e as Examination;
                    if (temp.Patient == currentPatient && temp.Prescriptions != null)
                        return true;
                    return false;
                };
                ObservableCollection<PrescriptionDTO> PrescriptionList = new ObservableCollection<PrescriptionDTO>();
                foreach (Examination e in collectionViewExam)
                {
                    foreach (Prescription p in e.Prescriptions)
                    {
                        PrescriptionDTO temp = new PrescriptionDTO(p, e);
                        PrescriptionList.Add(temp);
                    }
                }
                dgPatientPrescriptions.ItemsSource = PrescriptionList;
            }
            if (currentPatient.Alergens == null)
                currentPatient.Alergens = new ObservableCollection<string>();
            listViewAlergens.ItemsSource = currentPatient.Alergens;

            if (currentPatient.WorkHistory == null)
                currentPatient.WorkHistory = new ObservableCollection<Job>();
            dgJob.ItemsSource = currentPatient.WorkHistory;
            //HOspitalization

            ObservableCollection<HospitalizationDTO> HospitalizationList = new ObservableCollection<HospitalizationDTO>();
            HospitalizationDTO hos;
            foreach (Room room in RoomStorage.getInstance().GetAll())
            {
                if (room.Beds != null)
                {
                    foreach (Bed bed in room.Beds)
                        foreach (Tuple<DateTime, DateTime, int> t in bed.Interval)
                        {
                            if (t.Item3 == currentPatient.Id)
                            {
                                hos = new HospitalizationDTO(bed.Id, t.Item1, t.Item2, currentPatient, room);
                                HospitalizationList.Add(hos);
                            }
                        }
                }
            }
            dgPatientHospitalizations.ItemsSource = HospitalizationList;

            txtID.IsReadOnly = true; //PREBACI U XAML
            txtIme.IsReadOnly = true;
            txtPrezime.IsReadOnly = true;
            txtJmbg.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
            txtTel.IsReadOnly = true;
            txtAdress.IsReadOnly = true;
            txtSoc.IsReadOnly = true;
            txtCity.IsReadOnly = true;
            txtCountry.IsReadOnly = true;
            cbMarriage.IsEnabled = false;
            dpBirth.IsEnabled = false;
            rbF.IsEnabled = false;
            rbM.IsEnabled = false;
            cbGuest.IsEnabled = false;
            tExam.Visibility = Visibility.Collapsed;
            tRefferal.Visibility = Visibility.Collapsed;
            tPersc.Visibility = Visibility.Collapsed;
            tHospitalization.Visibility = Visibility.Collapsed;
        }

        private void Button_View_Examination(object sender, RoutedEventArgs e)
        {
            View_Examination_Or_Operation(dgPatientExams);
        }
        private void ButtonAddAlergen_Click(object sender, RoutedEventArgs e)
        {
            if (txtSubstance.Text != "")
                currentPatient.Alergens.Add(txtSubstance.Text);
        }

        private void ButtonDeleteAlergen_Click(object sender, RoutedEventArgs e)
        {
            List<string> tempList = new List<string>();
            foreach (string selectedAlergen in listViewAlergens.SelectedItems)
                tempList.Add(selectedAlergen);

            foreach (string alergen in tempList)
                currentPatient.Alergens.Remove(alergen);
        }

        private void Button_Save_Anamnesis(object sender, RoutedEventArgs e)
        {
            Examination currExam = (Examination)dgPatientExams.SelectedItem;
            Anamnesis anamnesis = new Anamnesis(txtAnamnesis.Text, txtDiagnosis.Text);
            currExam.Anamnesis = anamnesis;
            ExaminationStorage.getInstance().Edit(currExam);
        }

        private void Button_Save_Refferal(object sender, RoutedEventArgs e)
        {
            string Note = txtNoteRefferal.Text;
            string specialization = cbSpecializationRefferal.Text;
            Doctor doctor = (Doctor)cbDoctorRefferal.SelectedItem;
            int id = RefferalStorage.getInstance().GenerateNewID();
            Patient patient = currentPatient;
            Refferal newRefferal = new Refferal(id, Note, specialization, patient.Id, patient.FirstName, patient.LastName, Refferal.STATUS.Active, doctor.Id, doctor.FirstName, doctor.LastName);
            RefferalStorage.getInstance().Add(newRefferal);
            RefferalStorage.getInstance().serialize();
            tRefferal.Visibility = Visibility.Collapsed;
            tRefferals.Focus();
        }

        private void Button_New_Refferal(object sender, RoutedEventArgs e)
        {
            txtNoteRefferal.Clear();
            InitializeSpecializatonForRefferal(cbSpecializationRefferal);
            cbDoctorRefferal.SelectedIndex = -1;
            cbSpecializationRefferal.SelectedIndex = -1;
            cbDoctorRefferal.IsEnabled = true;
            cbSpecializationRefferal.IsEnabled = true;
            btnSavePrescription.Visibility = Visibility.Visible;
            tRefferal.Visibility = Visibility.Visible;
            tRefferal.Focus();
        }

        private void Button_View_Refferal(object sender, RoutedEventArgs e)
        {
            Refferal selectedRefferal = (Refferal)dgPatientRefferals.SelectedItem;
            txtNoteRefferal.Text = selectedRefferal.Note;

            Doctor selectedDoctor = DoctorStorage.getInstance().GetOne(selectedRefferal.DoctorId);

            cbDoctorRefferal.ItemsSource = DoctorStorage.getInstance().GetAll();
            cbDoctorRefferal.SelectedItem = selectedDoctor;

            InitializeSpecializatonForRefferal(cbSpecializationRefferal);
            cbSpecializationRefferal.SelectedItem = selectedDoctor;

            cbDoctorRefferal.IsEnabled = false;
            cbSpecializationRefferal.IsEnabled = false;
            txtNoteRefferal.IsEnabled = false;

            btnSavePrescription.Visibility = Visibility.Collapsed;
            tRefferal.Visibility = Visibility.Visible;
            tRefferal.Focus();
        }

        private void cbSpecializationRefferal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSpecializationRefferal.SelectedIndex != -1)
                InitializeDoctorsForRefferal(cbSpecializationRefferal.SelectedItem.ToString(), cbDoctorRefferal);
        }

        private void InitializeSpecializatonForRefferal(ComboBox cb)
        {
            ListCollectionView specializationCollection = new ListCollectionView(DoctorStorage.getInstance().GetAll());
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
            cb.ItemsSource = specializationCollection;
        }

        private void InitializeDoctorsForRefferal(string Specializaton, ComboBox cb)
        {
            ListCollectionView doctorsCollection = new ListCollectionView(DoctorStorage.getInstance().GetAll());
            doctorsCollection.Filter = (e) =>
            {
                Doctor temp = e as Doctor;
                if (temp.Specialization == Specializaton)
                    return true;
                return false;
            };
            cb.ItemsSource = doctorsCollection;
        }

        private void Button_View_Operation(object sender, RoutedEventArgs e)
        {
            View_Examination_Or_Operation(dgPatientOperations);
        }

        private void View_Examination_Or_Operation(DataGrid dg)
        {
            Examination selectedExam = (Examination)dgPatientExams.SelectedItem;
            Anamnesis anamnesis = selectedExam.Anamnesis;

            txtAnamnesis.Clear();
            txtDiagnosis.Clear();

            if (anamnesis != null)
            {
                txtAnamnesis.Text = anamnesis.AnamnesisInfo;
                txtDiagnosis.Text = anamnesis.Diagnosis;
            }

            tExam.Visibility = Visibility.Visible;
            tExam.Focus();
        }

        private void Button_Cancel_Refferal(object sender, RoutedEventArgs e)
        {
            tRefferal.Visibility = Visibility.Collapsed;
            tRefferals.Focus();
        }

        private void Button_Cancel_Anamnesis(object sender, RoutedEventArgs e)
        {
            tExam.Visibility = Visibility.Collapsed;
            tMedHis.Focus();
        }

        private void Button_View_Prescription(object sender, RoutedEventArgs e)
        {
            PrescriptionDTO selectedPrescription = (PrescriptionDTO)dgPatientPrescriptions.SelectedItem;
            Examination correspondingExam = new Examination();
            correspondingExam = ExaminationStorage.getInstance().GetOne(selectedPrescription.ExamId);
            txtDate.Text = correspondingExam.Date.ToString("dd/MM/yyyy");
            txtTime.Text = correspondingExam.Time.ToString("HH/mm");
            cbDrug.ItemsSource = DrugStorage.getInstance().GetAllVerifiedDrugs();
            cbDrug.SelectedItem = selectedPrescription.Drug;
            txtTaking.Text = selectedPrescription.Taking;
            txtInterval.Text = Convert.ToString(selectedPrescription.Interval);

            txtDate.IsEnabled = false;
            txtTime.IsEnabled = false;
            cbDrug.IsEnabled = false;
            txtTaking.IsEnabled = false;
            txtInterval.IsEnabled = false;

            tPersc.Visibility = Visibility.Visible;
            tPersc.Focus();
        }

        //private void Button_Save_Prescription(object sender, RoutedEventArgs e)
        //{
        //    PrescriptionDTO selectedPrescription = (PrescriptionDTO)dgPatientPrescriptions.SelectedItem;
        //    Examination correspondingExam = new Examination();
        //    correspondingExam = ExaminationStorage.getInstance().GetOne(selectedPrescription.ExamId);
        //    for(int i= 0; i < correspondingExam.Prescriptions.Count - 1; ++i)
        //    {
        //        if(correspondingExam.Prescriptions[i].Id == selectedPrescription.PrescriptionId)
        //        {
        //            correspondingExam.Prescriptions[i].Taking = selectedPrescription.Taking;
        //            correspondingExam.Prescriptions[i].Interval = selectedPrescription.Interval;
        //            correspondingExam.Prescriptions[i].Drug = selectedPrescription.Drug;
        //            break;
        //        }
        //    }
        //    ExaminationStorage.getInstance().Edit(correspondingExam);
        //}

        private void Button_Cancel_Prescription(object sender, RoutedEventArgs e)
        {
            tPersc.Visibility = Visibility.Collapsed;
            tMedHis.Focus();
        }

        private void Button_View_Hospitalization(object sender, RoutedEventArgs e)
        {
            HospitalizationDTO selectedHospitalization = (HospitalizationDTO)dgPatientHospitalizations.SelectedItem;
            Room selectedRoom = RoomStorage.getInstance().GetOne(selectedHospitalization.RoomId);
            Bed selectedBed = new Bed();
            cbHospitalizationRoom.ItemsSource = RoomStorage.getInstance().GetAll();
            cbHospitalizatonBed.ItemsSource = selectedRoom.Beds;
            foreach (Bed bed in selectedRoom.Beds)
                if (bed.Id == selectedHospitalization.BedId)
                {
                    selectedBed = bed;
                    break;
                }
            txtHospitalizationPatient.Text = selectedHospitalization.FirstName + " " + selectedHospitalization.LastName;
            txtHospitalizationPatient.IsEnabled = false;
            dpHospitalizationIN.SelectedDate = selectedHospitalization.DateIn;
            dpHospitalizationOUT.SelectedDate = selectedHospitalization.DateOut;
            cbHospitalizationRoom.SelectedItem = selectedRoom;
            cbHospitalizatonBed.SelectedItem = selectedBed;

            tHospitalization.Visibility = Visibility.Visible;
            tHospitalization.Focus();
        }

        private void Button_Save_Hospitalization(object sender, RoutedEventArgs e)
        {
            HospitalizationDTO selectedHospitalization = (HospitalizationDTO)dgPatientHospitalizations.SelectedItem;
            ObservableCollection<Room> rooms = RoomStorage.getInstance().GetAll();

            DateTime inTime = (DateTime)dpHospitalizationIN.SelectedDate;
            DateTime outTime = (DateTime)dpHospitalizationOUT.SelectedDate;
            Patient selectedPatient = currentPatient;
            foreach (Room room in rooms)
            {
                if(room.Beds != null)
                {
                    foreach (Bed bed in room.Beds)
                    {
                        if(bed.Interval != null)
                        {
                            foreach (Tuple<DateTime, DateTime, int> t in bed.Interval)
                            {
                                if (t.Item1 == selectedHospitalization.DateIn && t.Item2 == selectedHospitalization.DateOut && t.Item3 == currentPatient.Id && room.Id == selectedHospitalization.RoomId && bed.Id == selectedHospitalization.BedId)
                                {
                                    bed.Interval.Remove(t);
                                    bed.Interval.Add(Tuple.Create(inTime, outTime, selectedPatient.Id));
                                    RoomStorage.getInstance().Edit(room);
                                    return;
                                }
                            }
                        }             
                    }
                }
            }
            tHospitalization.Visibility = Visibility.Collapsed;
            tMedHis.Focus();
        }

        private void cbHospitalizationRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fillListOfAvailableBeds();
        }

        private void dpHospitalizationTimeOut_Changed(object sender, SelectionChangedEventArgs e)
        {
            fillListOfAvailableRooms(new List<Bed>());
        }
        private void fillListOfAvailableRooms(List<Bed> beds)
        {
            DateTime inTime = (DateTime)dpHospitalizationIN.SelectedDate;
            DateTime outTime = (DateTime)dpHospitalizationOUT.SelectedDate;
            ListCollectionView roomCollectionView = new ListCollectionView(RoomStorage.getInstance().GetAll());

            roomCollectionView.Filter = (room) =>
            {
                Room tempRoom = room as Room;
                if (tempRoom.Name == "Room")
                {
                    foreach (Bed tempBed in tempRoom.Beds)
                    {
                        foreach (Tuple<DateTime, DateTime, int> val in tempBed.Interval)
                        {
                            if (val.Item1 <= inTime && val.Item2 >= inTime)
                                return false;
                            if (val.Item1 <= outTime && val.Item2 >= outTime)
                                return false;
                            if (val.Item1 >= inTime && val.Item2 <= outTime)
                                return false;
                            return true;
                        }
                    }
                }
                return false;
            };
            cbHospitalizationRoom.ItemsSource = roomCollectionView;
        }
        private void fillListOfAvailableBeds()
        {
            if (cbHospitalizationRoom.SelectedIndex != -1)
            {

                DateTime inTime = (DateTime)dpHospitalizationIN.SelectedDate;
                DateTime outTime = (DateTime)dpHospitalizationOUT.SelectedDate;
                Room selectedRoom = (Room)cbHospitalizationRoom.SelectedItem;
                ListCollectionView bedCollectionView = new ListCollectionView(selectedRoom.Beds);

                bedCollectionView.Filter = (bed) =>
                {
                    Bed tempBed = bed as Bed;

                    foreach (Tuple<DateTime, DateTime, int> val in tempBed.Interval)
                    {
                        if (val.Item1 <= inTime && val.Item2 >= inTime)
                            return false;
                        if (val.Item1 <= outTime && val.Item2 >= outTime)
                            return false;
                        if (val.Item1 >= inTime && val.Item2 <= outTime)
                            return false;
                        return true;
                    }
                    return false;
                };
                cbHospitalizatonBed.ItemsSource = bedCollectionView;
            }
        }

        private void Button_Cancel_Hospitalization(object sender, RoutedEventArgs e)
        {
            tHospitalization.Visibility = Visibility.Collapsed;
            tMedHis.Focus();
        }
    }
}
