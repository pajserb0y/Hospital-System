﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HospitalSystem.code
{
    /// <summary>
    /// Interaction logic for DoctorInitialWindow.xaml
    /// </summary>
    public partial class DoctorInitialWindow : Window
    {
        ListCollectionView collectionViewAppointment = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
        private Examination currExam = new Examination();
        private Doctor selectedDoctor = new Doctor();
        public DoctorInitialWindow(Doctor currentDoctor)
        {
            selectedDoctor = currentDoctor;
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();

            InitializeCollection();
            
            tExam.Visibility = Visibility.Collapsed;
            tPersc.Visibility = Visibility.Collapsed;
            tDrugDetails.Visibility = Visibility.Collapsed;
            tReport.Visibility = Visibility.Collapsed;
            tHospitalization.Visibility = Visibility.Collapsed;
            tRefferal.Visibility = Visibility.Collapsed;
        }
        
        private void InitializeCollection()
        {
            checkbox_tooltip.IsChecked = null;
            ObservableCollection<Appointment> appointments = AppointmentStorage.getInstance().GetAll();
            ObservableCollection<Doctor> doctors = DoctorStorage.getInstance().GetAll();
            ObservableCollection<Patient> patients = PatientsStorage.getInstance().GetAll();
            ObservableCollection<Drug> verifiedDrugs = DrugStorage.getInstance().GetAllVerifiedDrugs();
            ObservableCollection<Drug> unverifiedDrugs = DrugStorage.getInstance().GetAllUnverifiedDrugs();
            ObservableCollection<Announcement> announcements = AnnouncementStorage.getInstance().GetAll();

            dgVerifiedDrugs.ItemsSource = verifiedDrugs;
            dgUnverifiedDrugs.ItemsSource = unverifiedDrugs;
            cbDrug.ItemsSource = DrugStorage.getInstance().GetAllVerifiedDrugs();

            //Home
            //cbDoctorHome.ItemsSource = doctors;
            //cbDoctor.ItemsSource = doctors;

            //na home page je bio cbDoctorHome a na exaimnationu je bio cbDoctor
            dgPatients.ItemsSource = PatientsStorage.getInstance().GetAll();
            //dgDoctorAppointments.ItemsSource = appointments;
            fillAnnouncement();
            fillAppointment();
            fillDoctorAccount();
            InitializeSpecializatonForRefferal(cbSpecializationRefferal);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            AppointmentStorage.getInstance().serialize();
            this.Close();
        }
        private void fillAnnouncement()
        {
            ListCollectionView collectionViewAnnouncement = new ListCollectionView(AnnouncementStorage.getInstance().GetAll());
            if (selectedDoctor == null) //PREIMENOVATI cb u comboBox
            {
                return;
            }
            collectionViewAnnouncement.Filter = (announcement) =>
            {
                Announcement tempAnnouncement = announcement as Announcement;
                Doctor tempDoctor = selectedDoctor;
                if (tempAnnouncement.DoctorIDs.Contains(tempDoctor.Id))
                    return true;
                return false;
            };
            dgDoctorAnnouncements.ItemsSource = collectionViewAnnouncement;
            dgDoctorAnnouncements.SelectedIndex = -1;
        }
        private void Button_View_Announcement(object sender, RoutedEventArgs e)
        {
            if(dgDoctorAnnouncements.SelectedIndex != -1)
            {
                Announcement announcement = (Announcement)dgDoctorAnnouncements.SelectedItem;
                MessageBox.Show(announcement.Content);
            }
            else
            {
                MessageBox.Show("You have to select announcement");
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            NewExam newExamWindow = new NewExam();
            newExamWindow.ShowDialog();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;
            var selectedItem = dgDoctorAppointments.SelectedItem;
            if (selectedItem != null)
            {
                MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure that you want to delete appointement?", "Permanently deleting", btnMessageBox, icnMessageBox);
                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        {
                            AppointmentStorage.getInstance().Delete((Appointment)selectedItem);
                            break;
                        }

                    case MessageBoxResult.No:
                        /* ... */
                        break;

                    case MessageBoxResult.Cancel:
                        /* ... */
                        break;
                } 
           }
           else
           { 
                MessageBox.Show("You have to select appointment!");
           }
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            if (dgDoctorAppointments.SelectedItem != null)
            {
                var selectedItem = dgDoctorAppointments.SelectedItem;
                if (selectedItem == null)
                {
                    return;
                }
                EditExam editExamWindow = new EditExam((Appointment)selectedItem);
                editExamWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("You have to select appointment!");
            }
        }

        private void Button_LogOut(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            AppointmentStorage.getInstance().serialize();
            mainWindow.Show();
            this.Close();
        }

        private void fillAppointment()
        {
            if (selectedDoctor == null) //PREIMENOVATI cb u comboBox
            {
                return;
            }
            collectionViewAppointment.Filter = (appointment) =>
            {
                Appointment tempAppointment = appointment as Appointment;
                if (tempAppointment.Doctor == selectedDoctor)
                    return true;
                return false;
            };
            dgDoctorAppointments.ItemsSource = collectionViewAppointment;
            dgDoctorAppointments.SelectedIndex = -1;
        }
        private void fillDoctorAccount()
        {
            txtID.Text = selectedDoctor.Id.ToString();
            txtIme.Text = selectedDoctor.FirstName;
            txtPrezime.Text = selectedDoctor.LastName;
            txtJmbg.Text = selectedDoctor.Jmbg.ToString();
            txtAdress.Text = selectedDoctor.Adress;
            txtTel.Text = selectedDoctor.Telephone.ToString();
        }
        private void Button_Save_Anamnesis(object sender, RoutedEventArgs e)
        {
            if(!txtAnamnesis.Text.Equals("") && !txtDiagnosis.Text.Equals(""))
            {
                Appointment currApp = (Appointment)dgDoctorAppointments.SelectedItem;

                currExam.Anamnesis.AnamnesisInfo = txtAnamnesis.Text;
                currExam.Anamnesis.Diagnosis = txtDiagnosis.Text;

                ExaminationStorage.getInstance().Edit(currExam);

                AppointmentStorage.getInstance().Delete(currApp);

                tAppointments.Focus();
                tExam.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("You have not filled all necessary information!");
            }
        }

        private void Button_Begin(object sender, RoutedEventArgs e)
        {
            if(dgDoctorAppointments.SelectedItem != null)
            {
                Appointment currApp = (Appointment)dgDoctorAppointments.SelectedItem;

                currExam.Id = ExaminationStorage.getInstance().GenerateNewID();
                currExam.Time = currApp.Time;
                currExam.Date = currApp.Date;
                currExam.Patient = currApp.Patient;
                currExam.Room = currApp.Room;
                currExam.Doctor = currApp.Doctor;
                currExam.IsOperation = currApp.IsOperation;
                currExam.TimesChanged = currApp.TimesChanged;
                currExam.TimeOfCreation = currApp.TimeOfCreation;

                ExaminationStorage.getInstance().Add(currExam);
                tExam.Visibility = Visibility.Visible;
                txtAnamnesis.Clear();
                txtDiagnosis.Clear();
                tExam.Focus();
            }
            else
            {
                MessageBox.Show("You have to select appointment to start new exam!");
            }
        }

        private void Button_Save_Prescription(object sender, RoutedEventArgs e)
        {
            if(cbDrug.SelectedItem != null && !txtInterval.Text.Equals("") && !txtTaking.Text.Equals(""))
            {
                Drug selectedDrug = (Drug)cbDrug.SelectedItem;
                if (currExam.Patient.Alergens != null)
                {
                    foreach (Ingridient ingridient in selectedDrug.Ingridients)
                        if (currExam.Patient.Alergens.Contains(ingridient.Name))
                        {
                            MessageBox.Show("Patient is ALERGIC on some ingredint of this medicine!");
                            return;
                        }
                }
                if (currExam.Prescriptions == null)
                    currExam.Prescriptions = new ObservableCollection<Prescription>();

                int prescriptionId = ExaminationStorage.getInstance().GenerateNewPrescriptionID(currExam);
                Prescription newPrescription = new Prescription(prescriptionId, selectedDrug, txtTaking.Text, Convert.ToInt32(txtInterval.Text), currExam.Date, currExam.Time);

                currExam.Prescriptions.Add(newPrescription);

                ExaminationStorage.getInstance().Edit(currExam);
                tExam.Focus();
                tPersc.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("You have not filled all necessary information!");
            }
        }

        private void Button_Prescription(object sender, RoutedEventArgs e)
        {
            txtDate.Text = currExam.Date.ToString("dd/MM/yyyy");
            txtTime.Text = currExam.Time.ToString("HH/mm");
            cbDrug.SelectedIndex = -1;
            txtTaking.Clear();
            txtInterval.Clear();
            tPersc.Visibility = Visibility.Visible;
        }

        Drug selectedDrug;

        private void Button_View_Verified_Drug(object sender, RoutedEventArgs e)
        {
            selectedDrug = (Drug)dgVerifiedDrugs.SelectedItem;
            if (selectedDrug != null)
                View_Drug();
            else
            {
                MessageBox.Show("You have not selected any drug!");
            }
        }
        private void Button_View_Unverified_Drug(object sender, RoutedEventArgs e)
        {
            selectedDrug = (Drug)dgUnverifiedDrugs.SelectedItem;
            if (selectedDrug != null)
                View_Drug();
            else
            {
                MessageBox.Show("You have not selected any drug!");
            }
        }

        private void View_Drug()
        {
            if (selectedDrug != null)
            {
                tDrugDetails.Visibility = Visibility.Visible;
                if (selectedDrug.Ingridients == null)
                    selectedDrug.Ingridients = new ObservableCollection<Ingridient>();
                dgDrugDetails.ItemsSource = selectedDrug.Ingridients;
                txtDrugName.Text = selectedDrug.Name;
                tDrugDetails.Focus();

            }
            else
            {
                MessageBox.Show("You have not selected any drug!");
            }
        }

        private void Button_Add_Ingridient(object sender, RoutedEventArgs e)
        {
            if(txtNewIngridients.Text != string.Empty && txtNewAmount.Text != string.Empty)
            {
                Ingridient ingridient = new Ingridient();
                ingridient.Name = txtNewIngridients.Text;
                ingridient.Amount = Convert.ToInt32(txtNewAmount.Text);
                selectedDrug.Ingridients.Add(ingridient);
                foreach (Ingridient i in selectedDrug.Ingridients)
                    selectedDrug.Amount += i.Amount;
                txtNewIngridients.Clear();
                txtNewAmount.Clear();
            }
            else
            {
                MessageBox.Show("You have not filled all information!");
            }
        }

        private void Button_Delete_Ingridient(object sender, RoutedEventArgs e)
        {
            Ingridient selectedIngridient = (Ingridient)dgDrugDetails.SelectedItem;
            if (selectedIngridient != null)
            {
                selectedDrug.Ingridients.Remove(selectedIngridient);
            }
            else
            {
                MessageBox.Show("You have not selected any ingridient!");
            }
        }

        private void Button_Save_Drug_Details(object sender, RoutedEventArgs e)
        {
            int newAmount = 0;
            foreach (Ingridient i in selectedDrug.Ingridients)
                newAmount += i.Amount;
            selectedDrug.Amount = newAmount;
            selectedDrug.Name = txtDrugName.Text;
            DrugStorage.getInstance().EditDrug(selectedDrug);
            DrugStorage.getInstance().serialize();
            tDrugDetails.Visibility = Visibility.Collapsed;
            tDrugRecord.Focus();
        }

        private void Button_Verify_Drug(object sender, RoutedEventArgs e)
        {
            selectedDrug = (Drug)dgUnverifiedDrugs.SelectedItem;
            if (selectedDrug != null)
            {
                selectedDrug.Status = Drug.STATUS.Verified;
                selectedDrug.Report = null;
                DrugStorage.getInstance().EditDrug(selectedDrug); //ovde se i vrsi serijalizacija
                InitializeCollection();
            }
            else
            {
                MessageBox.Show("You have not selected any drug!");
            }
        }

        private void Button_Report_Drug(object sender, RoutedEventArgs e)
        {
            selectedDrug = (Drug)dgUnverifiedDrugs.SelectedItem;
            if (selectedDrug != null)
            {
                tReport.Visibility = Visibility.Visible;
                tReport.Focus();
                txtReport.Clear();
            }
            else
            {
                MessageBox.Show("You have not selected any drug!");
            }
        }

        private void Button_Send_Report(object sender, RoutedEventArgs e)
        {
            if(txtReport.Text != "")
            {
                selectedDrug = (Drug)dgUnverifiedDrugs.SelectedItem;
                selectedDrug.Status = Drug.STATUS.InProgress;
                selectedDrug.Report = txtReport.Text;
                DrugStorage.getInstance().EditDrug(selectedDrug);
                InitializeCollection();
                tReport.Visibility = Visibility.Collapsed;
                tDrugRecord.Focus();
            }
            else
            {
                MessageBox.Show("You have not filled neccesary information!");
            }
           
        }

        private void Button_Save_Refferal(object sender, RoutedEventArgs e)
        {
            if(cbSpecializationRefferal.SelectedItem != null)
            {
                string Note = txtNoteRefferal.Text;
                string specialization = cbSpecializationRefferal.Text;
                Doctor doctor = selectedDoctor;
                int id = RefferalStorage.getInstance().GenerateNewID();
                //Examination currExam = (Examination)dgDoctorExams.SelectedItem; //treba da bude currExam izabran kad je dugme begin klinkuto i da se sve vreme radi s tim currExam-om
                Patient patient = currExam.Patient;
                Refferal newRefferal = new Refferal(id, Note, specialization, patient.Id, patient.FirstName, patient.LastName, Refferal.STATUS.Active, doctor.Id, doctor.FirstName, doctor.LastName);
                RefferalStorage.getInstance().Add(newRefferal);
                tRefferal.Visibility = Visibility.Collapsed;
                tExam.Focus();
            }
            else
            {
                MessageBox.Show("You have not filled all necessary information!");
            }
           
        }

        private void Button_Refferal(object sender, RoutedEventArgs e)
        {
            txtNoteRefferal.Clear();
            cbDoctorRefferal.SelectedIndex = -1;
            cbSpecializationRefferal.SelectedIndex = -1;
            tRefferal.Visibility = Visibility.Visible;
            tRefferal.Focus();
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

        private void cbSpecializationRefferal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSpecializationRefferal.SelectedIndex != -1)
                InitializeDoctorsForRefferal(cbSpecializationRefferal.SelectedItem.ToString(), cbDoctorRefferal);
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
        private void Button_Hospitalization(object sender, RoutedEventArgs e)
        {
            txtHospitalizationPatient.Text = Convert.ToString(currExam.Patient);
            txtHospitalizationPatient.IsEnabled = false;
            dpHospitalizationOUT.SelectedDate = null;
            cbHospitalizationRoom.SelectedIndex = -1;
            cbHospitalizatonBed.SelectedIndex = -1;
            dpHospitalizationIN.SelectedDate = null;
            tHospitalization.Visibility = Visibility.Visible;
            tHospitalization.Focus();
        }

        private void Button_Save_Hospitalization(object sender, RoutedEventArgs e)
        {
            if(cbHospitalizationRoom.SelectedItem != null && cbHospitalizatonBed.SelectedItem != null && dpHospitalizationIN.SelectedDate != null && dpHospitalizationOUT.SelectedDate != null)
            {
                Room selectedRoom = (Room)cbHospitalizationRoom.SelectedItem;
                Bed selectedBed = (Bed)cbHospitalizatonBed.SelectedItem;
                DateTime inTime = (DateTime)dpHospitalizationIN.SelectedDate;
                DateTime outTime = (DateTime)dpHospitalizationOUT.SelectedDate;
                Patient selectedPatient = (Patient)currExam.Patient;
                foreach (Bed b in selectedRoom.Beds)
                {
                    if (b == selectedBed)
                    {

                        b.Interval.Add(Tuple.Create(inTime, outTime, selectedPatient.Id));
                        break;
                    }
                }
                RoomStorage.getInstance().Edit(selectedRoom);
                tHospitalization.Visibility = Visibility.Collapsed;
                tExam.Focus();
            }
            else
            {
                MessageBox.Show("You have not filled all necessary information!");
            }
        }

        private void cbHospitalizationRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           //if (dpHospitalizationIN.SelectedDate != null && dpHospitalizationOUT != null)
                fillListOfAvailableBeds();

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

        private void dpHospitalizationTimeOut_Changed(object sender, SelectionChangedEventArgs e)
        {
            fillListOfAvailableRooms(new List<Bed>());
        }

        private void fillListOfAvailableBeds()
        {
            if(cbHospitalizationRoom.SelectedIndex != -1)
            {

                DateTime inTime = (DateTime)dpHospitalizationIN.SelectedDate;
                DateTime outTime = (DateTime)dpHospitalizationOUT.SelectedDate;
                Room selectedRoom = (Room)cbHospitalizationRoom.SelectedItem;
                ListCollectionView bedCollectionView = new ListCollectionView(selectedRoom.Beds);

                bedCollectionView.Filter = (bed) =>
                {
                    Bed tempBed = bed as Bed;

                    foreach (Tuple<DateTime,DateTime,int> val in tempBed.Interval)
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

        private void Button_Wizard(object sender, RoutedEventArgs e)
        {
            HelpWizard hw = new HelpWizard(selectedDoctor);
            hw.ShowDialog();
            this.Close();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ButtonAddAppointment.ToolTip = null;
            ButtonEditAppointment.ToolTip = null;
            ButtonDeleteAppointment.ToolTip = null;
            ButtonBeginExam.ToolTip = null;
            Button_Verify.ToolTip = null;
            Button_Report.ToolTip = null;
            Button_View_Unverified.ToolTip = null;
            Button_View_Verified.ToolTip = null;
            Button_wizard.ToolTip = null;
            button_announcement.ToolTip = null;
            txtPatient.ToolTip = null;
            button_save_perscription.ToolTip = null;
        }

        private void checkbox_tooltip_Checked(object sender, RoutedEventArgs e)
        {
            ButtonAddAppointment.ToolTip = "Add new appointment or operation";
            ButtonEditAppointment.ToolTip = "Edit appointment";
            ButtonDeleteAppointment.ToolTip = "Delete appointment";
            ButtonBeginExam.ToolTip = "Start new examination";
            Button_Verify.ToolTip = "Verify new drug";
            Button_Report.ToolTip = "Send report for drug";
            Button_View_Unverified.ToolTip = "View drug details";
            Button_View_Verified.ToolTip = "View drug details";
            Button_wizard.ToolTip = "Instuction wizard";
            button_announcement.ToolTip = "View selected announcement";
            txtPatient.ToolTip = "Search by patient";
            button_save_perscription.ToolTip = "Save prescription";
        }

        private void Button_View_Patient_Details(object sender, RoutedEventArgs e)
        {
            PatientDetails patientDetails = new PatientDetails((Patient)dgPatients.SelectedItem);
            patientDetails.ShowDialog();
        }

        private void Button_Save_Doctor_Info(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedDoctor.Jmbg = Convert.ToInt32(txtJmbg.Text);
                selectedDoctor.Telephone = Convert.ToInt32(txtTel.Text);
                if (txtIme.Text != "" && txtPrezime.Text != "" && txtAdress.Text != "" && selectedDoctor.Jmbg != 0 && selectedDoctor.Telephone != 0)
                {
                    selectedDoctor.FirstName = txtIme.Text;
                    selectedDoctor.LastName = txtPrezime.Text;
                    selectedDoctor.Adress = txtAdress.Text;
                }
                else
                {
                    MessageBox.Show("You have not filled all necessary information!");
                }
            }
            catch
            {
                MessageBox.Show("You have not filled all necessary information corectly!");
            }          
        }
    }
}

