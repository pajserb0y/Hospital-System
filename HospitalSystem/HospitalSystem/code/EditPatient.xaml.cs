using NHibernate.Hql.Ast.ANTLR.Util;
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
    /// Interaction logic for EditPatient.xaml
    /// </summary>
    public partial class EditPatient : Window
    {
        private Patient currentPatient;
        ListCollectionView appointmentCollection = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
        ListCollectionView examCollection = new ListCollectionView(ExaminationStorage.getInstance().GetAll());


        public EditPatient(Patient selectedPatient)
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();

            currentPatient = selectedPatient;
            initializeSelectedPatientDetails(selectedPatient);
            fillAppointments();
            fillExaminations();
            hideExaminationDetails();
            fillAnnouncements();
        }
      
        private void fillAnnouncements()
        {
            List<Announcement> selectedPatientAnnouncements = new List<Announcement>();
            foreach (Announcement ann in AnnouncementStorage.getInstance().GetAll())
                if (ann.PatientIDs.Contains(currentPatient.Id))
                    selectedPatientAnnouncements.Add(ann);
            announcementList.ItemsSource = selectedPatientAnnouncements;
        }

        private void fillExaminations()
        {
            examCollection.Filter = (e) =>
            {
                Examination temp = e as Examination;
                if (temp.Patient == currentPatient)
                    return true;
                return false;
            };
            dgExam.ItemsSource = examCollection;
        }

        private void fillAppointments()
        {
            appointmentCollection.Filter = (e) =>
            {
                Appointment temp = e as Appointment;
                if (temp.Patient == currentPatient)
                    return true;
                return false;
            };
            dgApp.ItemsSource = appointmentCollection;
        }

        private void hideExaminationDetails()
        {
            tabAnamnesis.Visibility = Visibility.Collapsed;
            tabPrescription.Visibility = Visibility.Collapsed;
            t5.Visibility = Visibility.Collapsed;
            t6.Visibility = Visibility.Collapsed;
            t7.Visibility = Visibility.Collapsed;
            t8.Visibility = Visibility.Collapsed;
        }

        private void initializeSelectedPatientDetails(Patient selectedPatient)
        {
            txtID.Text = selectedPatient.Id.ToString();
            txtIme.Text = selectedPatient.FirstName;
            txtPrezime.Text = selectedPatient.LastName;
            txtJmbg.Text = selectedPatient.Jmbg.ToString();
            txtAdress.Text = selectedPatient.Adress;
            txtTel.Text = selectedPatient.Telephone.ToString();
            txtEmail.Text = selectedPatient.Email;
            _ = selectedPatient.Gender == 'M' ? rbM.IsChecked = true : rbF.IsChecked = true;
            _ = selectedPatient.Guest == true ? cbGuest.IsChecked = true : cbGuest.IsChecked = false;
            txtUsername.Text = selectedPatient.Username;
            txtPassword.Text = selectedPatient.Password;
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
            listViewAlergens.ItemsSource = selectedPatient.Alergens;
            dgJob.ItemsSource = selectedPatient.WorkHistory;
        }

        #region Chart + Account + Inbox
        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PatientsStorage.getInstance().Edit(new Patient(currentPatient.Id, txtIme.Text, txtPrezime.Text, Convert.ToInt64(txtJmbg.Text),
                (char)((bool)rbF.IsChecked ? Convert.ToChar(rbF.Content) : Convert.ToChar(rbM.Content)), txtAdress.Text, Convert.ToInt64(txtTel.Text), txtEmail.Text, cbGuest.IsChecked == true,
                txtUsername.Text, txtPassword.Text, (DateTime)dpBirth.SelectedDate, cbMarriage.SelectedIndex == -1 ? "" : cbMarriage.SelectedValue.ToString(), Convert.ToInt64(txtSoc.Text),
                txtCity.Text, txtCountry.Text, currentPatient.Alergens, currentPatient.WorkHistory));
            PatientsStorage.getInstance().serialize();
            this.Close();
        }
        private void txbAddJob_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewJob newJob = new NewJob(currentPatient);
            newJob.Show();
        }
        private void txbEditJob_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            EditJob editJob = new EditJob(currentPatient, (Job)dgJob.SelectedItem);
            editJob.Show();
            (dgJob.ItemContainerGenerator.ContainerFromItem(dgJob.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan job
        }
        private void txbDeleteJob_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            currentPatient.WorkHistory.Remove((Job)dgJob.SelectedItem);
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

        private void txbRead_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AnnouncementWindow announcementWindow = new AnnouncementWindow((Announcement)announcementList.SelectedItem);
            announcementWindow.Show();
        }
        #endregion


        #region Appointment
        private void txbAddApp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SecretarNewAppointment secretarNewAppointment = new SecretarNewAppointment(currentPatient);
            secretarNewAppointment.Show();
        }
        private void txbEditApp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var selectedApp = dgApp.SelectedItem;
            if (selectedApp != null)
            {
                SecretarEditAppointment secretarEditAppointment = new SecretarEditAppointment((Appointment)selectedApp);
                secretarEditAppointment.Show();
                (dgApp.ItemContainerGenerator.ContainerFromItem(dgApp.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan app      
            }  
        }
        private void txbDeleteApp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var selectedApp = dgApp.SelectedItem;
            if (selectedApp != null)
            {
                AppointmentStorage.getInstance().Delete((Appointment)selectedApp);
            }
        }
        #endregion


        #region Medical history
        private void txbView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            showExaminationDetails();

            Examination selectedExam = (Examination)dgExam.SelectedItem;
            Anamnesis anamnesis = AnamnesisStorage.getInstance().GetOne(selectedExam.Id);
            Prescription prescription = PrescriptionStorage.getInstance().GetOne(selectedExam.Id);

            txtAnamnesis.Clear();
            txtDiagnosis.Clear();
            if (anamnesis != null)
            {
                txtAnamnesis.Text = anamnesis.AnamnesisInfo;
                txtDiagnosis.Text = anamnesis.Diagnosis;
            }
            tabAnamnesis.Focus();

            cbDrug.SelectedIndex = -1;
            txtTaking.Clear();
            txtDate.Clear();
            if (prescription != null)
            {
                cbDrug.SelectedItem = prescription.Drug;
                txtTaking.Text = prescription.Taking;
                txtDate.Text = prescription.DateOfPrescription.ToString();
            }
            tabAnamnesis.Focus();
        }

        private void showExaminationDetails()
        {
            tabAnamnesis.Visibility = Visibility.Visible;
            tabPrescription.Visibility = Visibility.Visible;
            t5.Visibility = Visibility.Visible;
            t6.Visibility = Visibility.Visible;
            t7.Visibility = Visibility.Visible;
            t8.Visibility = Visibility.Visible;
        }
        #endregion


        private void Window_Closed(object sender, EventArgs e)
        {
            //JobStorage.getInstance().serialize();
            this.Close();
        }        
    }
}
