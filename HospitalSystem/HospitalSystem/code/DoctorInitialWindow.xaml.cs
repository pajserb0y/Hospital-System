using System;
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

        public DoctorInitialWindow()
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();

            InitializeCollection();
            
            tExam.Visibility = Visibility.Collapsed;
            tPersc.Visibility = Visibility.Collapsed;
            tDrugDetails.Visibility = Visibility.Collapsed;
            tReport.Visibility = Visibility.Collapsed;
            tOperation.Visibility = Visibility.Collapsed;
            tRefferal.Visibility = Visibility.Collapsed;
        }
        
        private void InitializeCollection()
        {
            ObservableCollection<Appointment> appointments = AppointmentStorage.getInstance().GetAll();
            ObservableCollection<Doctor> doctors = DoctorStorage.getInstance().GetAll();
            ObservableCollection<Patient> patients = PatientsStorage.getInstance().GetAll();
            ObservableCollection<Drug> verifiedDrugs = DrugStorage.getInstance().GetAllVerifiedDrugs();
            ObservableCollection<Drug> unverifiedDrugs = DrugStorage.getInstance().GetAllUnverifiedDrugs();

            dgVerifiedDrugs.ItemsSource = verifiedDrugs;
            dgUnverifiedDrugs.ItemsSource = unverifiedDrugs;
            cbDrug.ItemsSource = DrugStorage.getInstance().GetAllVerifiedDrugs();

            cbDoctor.ItemsSource = doctors;
            cbPatient.ItemsSource = patients;
            dgDoctorExams.ItemsSource = appointments;

            InitializeSpecializatonForRefferal(cbSpecializationRefferal);
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

        private void Window_Closed(object sender, EventArgs e)
        {
            AppointmentStorage.getInstance().serialize();
            this.Close();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            NewExam newExamWindow = new NewExam();
            newExamWindow.Show();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgDoctorExams.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            AppointmentStorage.getInstance().Delete((Appointment)selectedItem);
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Appointment> ExamList = AppointmentStorage.getInstance().GetAll();
            var selectedItem = dgDoctorExams.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            EditExam editExamWindow = new EditExam((Appointment)selectedItem);
            editExamWindow.Show();
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            AppointmentStorage.getInstance().serialize();
            mainWindow.Show();
            this.Close();
        }

        private void doctorChanged(object sender, System.EventArgs e)
        {
            if (cbDoctor.SelectedItem == null) //PREIMENOVATI cb u comboBox
            {
                return;
            }
            collectionViewAppointment.Filter = (appointment) =>
            {
                Appointment tempAppointment = appointment as Appointment;
                if (tempAppointment.Doctor == cbDoctor.SelectedItem)
                    return true;
                return false;
            };
            dgDoctorExams.ItemsSource = collectionViewAppointment;
        }

        private void patientChanged(object sender, SelectionChangedEventArgs e)
        {
            PatientDetails patientDetails = new PatientDetails((Patient)cbPatient.SelectedItem);
            patientDetails.Show();
        }

        private void Button_Save_Anamnesis(object sender, RoutedEventArgs e)
        {
            Examination currExam = (Examination)dgDoctorExams.SelectedItem;
            currExam.Id = ExaminationStorage.getInstance().GenerateNewID();
            Anamnesis newAnamnesis = new Anamnesis(currExam.Id, txtAnamnesis.Text, txtDiagnosis.Text);
            AnamnesisStorage.getInstance().Add(newAnamnesis);
            AnamnesisStorage.getInstance().serialize();

            ExaminationStorage.getInstance().Add(currExam);
            ExaminationStorage.getInstance().serialize();

            Appointment currApp = (Appointment)dgDoctorExams.SelectedItem;
            AppointmentStorage.getInstance().Delete(currApp);
            AppointmentStorage.getInstance().serialize();
            t0.Focus();
            tExam.Visibility = Visibility.Collapsed;
           // dgDoctorExams.Items.Remove(dgDoctorExams.SelectedItem);
        }

        private void Button_Begin(object sender, RoutedEventArgs e)
        {
            tExam.Visibility = Visibility.Visible;
            txtAnamnesis.Clear();
            txtDiagnosis.Clear();
            tExam.Focus();
        }

        private void Button_Save_Prescription(object sender, RoutedEventArgs e)
        {
            Examination currExam = (Examination)dgDoctorExams.SelectedItem;
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

            int patientId = currExam.Patient.Id;

            int prescID = PrescriptionStorage.getInstance().GenerateNewID();
            Prescription newPrescription = new Prescription(prescID, patientId, currExam.Id, selectedDrug, txtTaking.Text, currExam.Date);
            
            PrescriptionStorage.getInstance().Add(newPrescription);
            PrescriptionStorage.getInstance().serialize();
            tExam.Focus();
            tPersc.Visibility = Visibility.Collapsed;
        }

        private void Button_Prescription(object sender, RoutedEventArgs e)
        {
            Examination currExam = (Examination)dgDoctorExams.SelectedItem;
            txtDate.Text = currExam.Date.ToString("dd/MM/yyyy");
            cbDrug.SelectedIndex = -1;
            txtTaking.Clear();
            tPersc.Visibility = Visibility.Visible;
        }

        private Drug selectedDrug;

        private void Button_View_Verified_Drug(object sender, RoutedEventArgs e)
        {
            selectedDrug = (Drug)dgVerifiedDrugs.SelectedItem;
            if (selectedDrug != null)
                View_Drug(dgVerifiedDrugs);
        }
        private void Button_View_Unverified_Drug(object sender, RoutedEventArgs e)
        {
            selectedDrug = (Drug)dgUnverifiedDrugs.SelectedItem;
            if (selectedDrug != null)
                View_Drug(dgUnverifiedDrugs);
        }

        private void View_Drug(DataGrid dgDrugs)
        {
            if (dgDrugs.SelectedItem != null)
            {
                tDrugDetails.Visibility = Visibility.Visible;
                if (selectedDrug.Ingridients == null)
                    selectedDrug.Ingridients = new ObservableCollection<Ingridient>();
                dgDrugDetails.ItemsSource = selectedDrug.Ingridients;
                txtName.Text = selectedDrug.Name;
                tDrugDetails.Focus();

            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //    string input = txtPatient.Text;

            //    string[] tokens = input.Split(" ");
            //    if(tokens.Length > 3)
            //    {
            //        txtPatient.Foreground.Freeze;

            //    }
        }

        private void Button_Add_Ingridient(object sender, RoutedEventArgs e)
        {
            Ingridient ingridient = new Ingridient();
            ingridient.Name = txtNewIngridients.Text;
            ingridient.Amount = Convert.ToInt32(txtNewAmount.Text);
            selectedDrug.Ingridients.Add(ingridient);
            foreach (Ingridient i in selectedDrug.Ingridients)
                selectedDrug.Amount += i.Amount;
            txtNewIngridients.Text = "";
            txtNewAmount.Text = "";
        }

        private void Button_Delete_Ingridient(object sender, RoutedEventArgs e)
        {
            Ingridient selectedIngridient = (Ingridient)dgDrugDetails.SelectedItem;
            if (selectedIngridient != null)
            {
                selectedDrug.Ingridients.Remove(selectedIngridient);
            }
        }

        private void Button_Save_Drug_Details(object sender, RoutedEventArgs e)
        {
            int newAmount = 0;
            foreach (Ingridient i in selectedDrug.Ingridients)
                newAmount += i.Amount;
            selectedDrug.Amount = newAmount;
            DrugStorage.getInstance().GetOneDrug(selectedDrug.Id).Name = txtName.Text;
            DrugStorage.getInstance().serialize();
            tDrugDetails.Visibility = Visibility.Collapsed;
            tDrugRecord.Focus();
        }

        private void Button_Verify_Drug(object sender, RoutedEventArgs e)
        {
            selectedDrug = (Drug)dgUnverifiedDrugs.SelectedItem;
            DrugStorage.getInstance().GetOneDrug(selectedDrug.Id).Status = Drug.STATUS.Verified;
            DrugStorage.getInstance().GetOneDrug(selectedDrug.Id).Report = null;
            InitializeCollection();
            DrugStorage.getInstance().serialize();
        }

        private void Button_Report(object sender, RoutedEventArgs e)
        {
            selectedDrug = (Drug)dgUnverifiedDrugs.SelectedItem;
            if (selectedDrug != null)
            {
                tReport.Visibility = Visibility.Visible;
                tReport.Focus();
                txtReport.Text = "";
            }
        }

        private void Button_Send_Report(object sender, RoutedEventArgs e)
        {
            selectedDrug = (Drug)dgUnverifiedDrugs.SelectedItem;
            DrugStorage.getInstance().GetOneDrug(selectedDrug.Id).Status = Drug.STATUS.InProgress;
            InitializeCollection();
            DrugStorage.getInstance().GetOneDrug(selectedDrug.Id).Report = txtReport.Text;
            tReport.Visibility = Visibility.Collapsed;
            tDrugRecord.Focus();
        }

        private void Button_Save_Refferal(object sender, RoutedEventArgs e)
        {
            string Note = txtNoteRefferal.Text;
            string specialization = cbSpecializationRefferal.Text;
            Doctor doctor = (Doctor)cbDoctorRefferal.SelectedItem;
            int id = RefferalStorage.getInstance().GenerateNewID();
            Examination currExam = (Examination)dgDoctorExams.SelectedItem;
            Patient patient = currExam.Patient;
            Refferal newRefferal = new Refferal(id, Note, specialization, patient.Id, patient.FirstName, patient.LastName, Refferal.STATUS.Active, doctor.Id, doctor.FirstName, doctor.LastName);
            RefferalStorage.getInstance().Add(newRefferal);
            RefferalStorage.getInstance().serialize();
            tRefferal.Visibility = Visibility.Collapsed;
            tExam.Focus();
        }

        private void Button_Refferal(object sender, RoutedEventArgs e)
        {
            txtNoteRefferal.Text = "";
            cbDoctorRefferal.SelectedIndex = -1;
            cbSpecializationRefferal.SelectedIndex = -1;
            tRefferal.Visibility = Visibility.Visible;
            tRefferal.Focus();
        }

        private void Button_Operation(object sender, RoutedEventArgs e)
        {

        }
    }
}
