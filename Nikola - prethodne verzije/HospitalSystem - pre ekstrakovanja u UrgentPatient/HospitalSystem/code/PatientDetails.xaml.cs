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


            selectedPatient = currentPatient;
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


            if (selectedPatient.Alergens == null)
                selectedPatient.Alergens = new ObservableCollection<string>();
            listViewAlergens.ItemsSource = selectedPatient.Alergens;

            if (selectedPatient.WorkHistory == default)
                selectedPatient.WorkHistory = new ObservableCollection<Job>();
            dgJob.ItemsSource = selectedPatient.WorkHistory;
        }

        private void Button_View_Examination(object sender, RoutedEventArgs e)
        {
            Examination selectedExam = (Examination)dgPatientExams.SelectedItem;
            Anamnesis anamnesis = AnamnesisStorage.getInstance().GetOne(selectedExam.Id);

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
        private void ButtonAddAlergen_Click(object sender, RoutedEventArgs e)
        {
            NewAlergen newAlergen = new NewAlergen(currentPatient);
            newAlergen.Show();
        }

        private void ButtonEditAlergen_Click(object sender, RoutedEventArgs e)
        {
            EditAlergen editAlergen = new EditAlergen(currentPatient, listViewAlergens.SelectedItem.ToString());
            editAlergen.Show();
        }

        private void ButtonDeleteAlergen_Click(object sender, RoutedEventArgs e)
        {
            currentPatient.Alergens.Remove(listViewAlergens.SelectedItem.ToString());
        }

        private void Button_Save_Anamnesis(object sender, RoutedEventArgs e)
        {
            Examination currExam = (Examination) dgPatientExams.SelectedItem;
            Anamnesis anamnesis = new Anamnesis(currExam.Id, txtAnamnesis.Text, txtDiagnosis.Text);
            AnamnesisStorage.getInstance().Edit(anamnesis);
            AnamnesisStorage.getInstance().serialize();
        }

        private void Button_Save_Refferal(object sender, RoutedEventArgs e)
        {
            string Note = txtNoteRefferal.Text;
            string specialization = cbSpecializationRefferal.Text;
            Doctor doctor = (Doctor)cbDoctorRefferal.SelectedItem;
            int id = RefferalStorage.getInstance().GenerateNewID();
            Patient patient = currentPatient;
            Refferal newRefferal = new Refferal(id, Note, specialization, patient.Id,patient.FirstName,patient.LastName, Refferal.STATUS.Active, doctor.Id,doctor.FirstName,doctor.LastName);
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
            tRefferal.Visibility = Visibility.Visible;
            tRefferal.Focus();
        }

        private void Button_View_Refferal(object sender, RoutedEventArgs e)
        {
            Refferal selectedRefferal = (Refferal) dgPatientRefferals.SelectedItem;
            txtNoteRefferal.Text = selectedRefferal.Note;

            Doctor selectedDoctor = DoctorStorage.getInstance().GetOne(selectedRefferal.DoctorId);

            cbDoctorRefferal.ItemsSource = DoctorStorage.getInstance().GetAll();
            cbDoctorRefferal.SelectedItem = selectedDoctor;

            InitializeSpecializatonForRefferal(cbSpecializationRefferal);
            cbSpecializationRefferal.SelectedItem = selectedDoctor;

            cbDoctorRefferal.IsEnabled = false;
            cbSpecializationRefferal.IsEnabled = false;

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

        }
    }
}
