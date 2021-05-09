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
    /// Interaction logic for PatientDetails.xaml
    /// </summary>
    public partial class PatientDetails : Window
    {
        ListCollectionView collectionViewJob = new ListCollectionView(JobStorage.getInstance().GetAll());
        ListCollectionView collectionViewExam = new ListCollectionView(ExaminationStorage.getInstance().GetAll());
        ListCollectionView alergensCollection = new ListCollectionView(AlergenStorage.getInstance().GetAll());
        private Patient currentPatient;

        public PatientDetails(Patient selectedPatient)
        {
            InitializeComponent();
            currentPatient = selectedPatient;


            txtID.Text = selectedPatient.Id.ToString();
            txtIme.Text = selectedPatient.FirstName;
            txtPrezime.Text = selectedPatient.LastName;
            txtJmbg.Text = selectedPatient.Jmbg.ToString();
            txtAdress.Text = selectedPatient.Adress;
            txtTel.Text = selectedPatient.Telephone.ToString();
            txtEmail.Text = selectedPatient.Email;
            _ = selectedPatient.Gender == 'M' ? rbM.IsChecked = true : rbF.IsChecked = true;
            _ = selectedPatient.Guest == true ? cbGuest.IsChecked = true : cbGuest.IsChecked = false;

            dpBirth.SelectedDate = selectedPatient.BirthDate;

            if (selectedPatient.MarriageStatus == "Married")
                cbMarriage.SelectedIndex = 0;
            if (selectedPatient.MarriageStatus == "Unmarried")
                cbMarriage.SelectedIndex = 1;
            if (selectedPatient.MarriageStatus == "Divorced")
                cbMarriage.SelectedIndex = 2;
            if (selectedPatient.MarriageStatus == "Widow(er)")
                cbMarriage.SelectedIndex = 3;

            txtSoc.Text = selectedPatient.SocNumber.ToString();
            txtCity.Text = selectedPatient.City;
            txtCountry.Text = selectedPatient.Country;

            collectionViewJob.Filter = (e) =>
            {
                Job temp = e as Job;
                if (temp.PID == selectedPatient.Id)
                    return true;
                return false;
            };
            dgJob.ItemsSource = collectionViewJob;

            if (selectedPatient != null)
            {
                collectionViewExam.Filter = (e) =>
                {
                    Examination temp = e as Examination;
                    if (temp.Patient == selectedPatient)
                        return true;
                    return false;
                };
                dgPatientExams.ItemsSource = collectionViewExam;
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
            cbMarriage.IsEditable = false;
            dpBirth.IsEnabled = false;
            rbF.IsEnabled = false;
            rbM.IsEnabled = false;
            cbGuest.IsEnabled = false;
            tExam.Visibility = Visibility.Collapsed;


            alergensCollection.Filter = (e) =>
            {
                Alergen temp = e as Alergen;
                if (temp.PatientID == selectedPatient.Id)
                    return true;
                return false;
            };
            listViewAlergens.ItemsSource = alergensCollection;
        }

        private void Button_View(object sender, RoutedEventArgs e)
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
            NewAlergen newAlergen = new NewAlergen(currentPatient.Id);
            newAlergen.Show();
        }

        private void ButtonEditAlergen_Click(object sender, RoutedEventArgs e)
        {
            EditAlergen editAlergen = new EditAlergen((Alergen)listViewAlergens.SelectedItem);
            editAlergen.Show();
        }

        private void ButtonDeleteAlergen_Click(object sender, RoutedEventArgs e)
        {
            AlergenStorage.getInstance().Delete((Alergen)listViewAlergens.SelectedItem);
        }

        private void Button_Save_Anamnesis(object sender, RoutedEventArgs e)
        {
            Examination currExam = (Examination) dgPatientExams.SelectedItem;
            Anamnesis anamnesis = new Anamnesis(currExam.Id, txtAnamnesis.Text, txtDiagnosis.Text);
            AnamnesisStorage.getInstance().Edit(anamnesis);
            AnamnesisStorage.getInstance().serialize();
        }
    }
}
