using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for PatientInitialWindow.xaml
    /// </summary>
    public partial class PatientInitialWindow : Window
    {
        ListCollectionView allAppointments = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
        ListCollectionView allExaminations = new ListCollectionView(ExaminationStorage.getInstance().GetAll());
        Dictionary<int, int> newApptsMade = new Dictionary<int, int>();
        Patient currentPatient;

        #region Appointments and examinations
        public PatientInitialWindow()
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();

            hideExaminationDetails();
            ObservableCollection<Patient> patients = PatientsStorage.getInstance().GetAll();
            cbPatient.ItemsSource = patients;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            AppointmentStorage.getInstance().serialize();
            this.Close();
        }

        private void hideExaminationDetails()
        {
            tabAnamnesis.Visibility = Visibility.Collapsed;
            tabPrescription.Visibility = Visibility.Collapsed;
            tabNotes.Visibility = Visibility.Collapsed;
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (cbPatient.SelectedItem == null)
                return;
            
            int pid = ((Patient)cbPatient.SelectedItem).Id;
            foreach (Appointment a in AppointmentStorage.getInstance().GetAll())
            {
                if (a.Patient == cbPatient.SelectedItem && a.TimeOfCreation.Date == DateTime.Now.Date)
                    newApptsMade[pid] += 1;
                else if (a.Patient == cbPatient.SelectedItem && a.TimeOfCreation.Date < DateTime.Now.Date)
                    newApptsMade[pid] = 0;
            }

            if (newApptsMade[pid] >= 3)
            {
                MessageBox.Show("Cannot make more than 3 appointments in a day! Contact secretary for more details.");
                return;
            }            
            NewAppointment newAppt = new NewAppointment((Patient)cbPatient.SelectedItem);
            newAppt.Show();         
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (dgAppointment.SelectedItem == null)
                return;

            if (cbPatient.SelectedItem == null)
                return;

            AppointmentStorage.getInstance().Delete((Appointment)dgAppointment.SelectedItem);          
           
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            if (dgAppointment.SelectedItem != null && cbPatient.SelectedItem != null)
            {
                EditAppointment editAppt = new EditAppointment((Appointment)dgAppointment.SelectedItem);
                editAppt.Show();             
            }
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            AppointmentStorage.getInstance().serialize();           
            mw.Show();
            this.Close();
        }

        private void Button_View(object sender, RoutedEventArgs e)
        {
            Examination selectedExam = (Examination)dgExamination.SelectedItem;
            Anamnesis anamnesis = selectedExam.Anamnesis;
            ObservableCollection<Prescription> prescriptions = selectedExam.Prescriptions;
            showExaminationDetails();
            txtAnamnesis.Clear();
            txtDiagnosis.Clear();
            txtNotes.Clear();
            if (anamnesis != null)
            {
                txtAnamnesis.Text = anamnesis.AnamnesisInfo;
                txtDiagnosis.Text = anamnesis.Diagnosis;
            }

            txtNotes.Text = selectedExam.Notes;
            dgMedication.ItemsSource = prescriptions;
            tabAnamnesis.Focus();
        }

        private void showExaminationDetails()
        {
            tabAnamnesis.Visibility = Visibility.Visible;
            tabPrescription.Visibility = Visibility.Visible;
        }

        private void Button_Feedback(object sender, RoutedEventArgs e)
        {
            if (cbPatient.SelectedItem == null || dgExamination.SelectedItem == null)
                return;
            
            FeedbackForm rateExamination = new FeedbackForm((Examination)dgExamination.SelectedItem, ((Patient)cbPatient.SelectedItem).Id);
            rateExamination.Show();         
        }

        private void provideGeneralFeedback(int pid)
        {
            FeedbackForm rateExamination = new FeedbackForm(null, pid);
            rateExamination.Show();
        }

        private void patientChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPatient.SelectedItem == null)
                return;
            currentPatient = (Patient)cbPatient.SelectedItem;

            if (!newApptsMade.ContainsKey(currentPatient.Id))
                newApptsMade.Add(currentPatient.Id, 0);

            initializeSelectedPatientDetails(currentPatient);
            hideExaminationDetails();
            fillAnnouncements();
            filterAppointments();
            filterExaminations();

            if (allExaminations.Count > 0 && allExaminations.Count % 3 == 0)
                provideGeneralFeedback(currentPatient.Id);     
        }

        private void filterExaminations()
        {
            allExaminations.Filter = (e) =>
            {
                Examination temp = e as Examination;
                if (temp.Patient == cbPatient.SelectedItem)
                    return true;
                return false;
            };
            dgExamination.ItemsSource = allExaminations;
        }

        private void filterAppointments()
        {
            allAppointments.Filter = (e) =>
            {
                Appointment temp = e as Appointment;
                if (temp.Patient == cbPatient.SelectedItem)
                    return true;
                return false;
            };
            dgAppointment.ItemsSource = allAppointments;
        }
        #endregion

        #region Chart
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
            _ = selectedPatient.Guest = false;
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

            if (selectedPatient.Alergens == null)
                selectedPatient.Alergens = new ObservableCollection<string>();
            listViewAlergens.ItemsSource = selectedPatient.Alergens;

            if (selectedPatient.WorkHistory == default)
                selectedPatient.WorkHistory = new ObservableCollection<Job>();
            dgJob.ItemsSource = selectedPatient.WorkHistory;
        }

        private void Button_Save_Changes(object sender, RoutedEventArgs e)
        {
            PatientsStorage.getInstance().Edit(new Patient(currentPatient.Id, txtIme.Text, txtPrezime.Text, Convert.ToInt64(txtJmbg.Text),
                (char)((bool)rbF.IsChecked ? Convert.ToChar(rbF.Content) : Convert.ToChar(rbM.Content)), txtAdress.Text, Convert.ToInt64(txtTel.Text), txtEmail.Text, false,
                "", "", (DateTime)dpBirth.SelectedDate, cbMarriage.SelectedIndex == -1 ? "" : cbMarriage.SelectedValue.ToString(), Convert.ToInt64(txtSoc.Text),
                txtCity.Text, txtCountry.Text, currentPatient.Alergens, currentPatient.WorkHistory));
            PatientsStorage.getInstance().serialize();
            this.Close();
        }
        private void Button_Add_Job(object sender, RoutedEventArgs e)
        {
            NewJob newJob = new NewJob(currentPatient);
            newJob.Show();
        }
        private void Button_Edit_Job(object sender, RoutedEventArgs e)
        {
            //EditJob editJob = new EditJob(currentPatient, (Job)dgJob.SelectedItem);
            //editJob.Show();
            //(dgJob.ItemContainerGenerator.ContainerFromItem(dgJob.SelectedItem) as DataGridRow).IsSelected = false;
        }
        private void Button_Delete_Job(object sender, RoutedEventArgs e)
        {
            currentPatient.WorkHistory.Remove((Job)dgJob.SelectedItem);
        }

        private void Button_Add_Alergen(object sender, RoutedEventArgs e)
        {
            //NewAlergen newAlergen = new NewAlergen(currentPatient);
            //newAlergen.Show();
        }
        #endregion

        #region Inbox
        private void fillAnnouncements()
        {
            List<Announcement> selectedPatientAnnouncements = new List<Announcement>();
            foreach (Announcement ann in AnnouncementStorage.getInstance().GetAll())
                if (ann.PatientIDs.Contains(currentPatient.Id))
                    selectedPatientAnnouncements.Add(ann);
            announcementList.ItemsSource = selectedPatientAnnouncements;
        }

        private void Button_Read(object sender, RoutedEventArgs e)
        {
            AnnouncementWindow announcementWindow = new AnnouncementWindow((Announcement)announcementList.SelectedItem);
            announcementWindow.Show();
        }
        #endregion

        private void Button_Add_Notes(object sender, RoutedEventArgs e)
        {
            tabNotes.Visibility = Visibility.Visible;
            tabNotes.Focus();
        }

        private void Button_Save_Notes(object sender, RoutedEventArgs e)
        {
            Examination temp = (Examination)dgExamination.SelectedItem;
            temp.Notes = txtNotes.Text;
            ExaminationStorage.getInstance().Edit(temp);
        }
    }
}
