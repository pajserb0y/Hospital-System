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
            ObservableCollection<Patient> patients = PatientCRUDMenager.getInstance().GetAll();
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

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
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
            tabAnamnesis.Focus();

            txtNotes.Text = selectedExam.Notes;
            dgMedication.ItemsSource = prescriptions;
            
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
            if (cbPatient.SelectedItem != null)
            {
                currentPatient = (Patient)cbPatient.SelectedItem;

                if (!newApptsMade.ContainsKey(currentPatient.Id))
                    newApptsMade.Add(currentPatient.Id, 0);

                initializeSelectedPatientDetails();
                hideExaminationDetails();
                fillAnnouncements();
                filterAppointments();
                filterExaminations();

                if (allExaminations.Count > 0 && allExaminations.Count % 3 == 0)
                    provideGeneralFeedback(currentPatient.Id);
            }
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

        #region Home

        private void initializeSelectedPatientDetails()
        {
            firstName.Text = currentPatient.FirstName;
            lastName.Text = currentPatient.LastName;
            jmbg.Text = currentPatient.Jmbg.ToString();
            gender.Text = currentPatient.Gender == 'M' ? "Male" : "Female";
            adress.Text = currentPatient.Adress;
            telephone.Text = currentPatient.Telephone.ToString();
            email.Text = currentPatient.Email;
        }

        private void Button_Chart_Details(object sender, RoutedEventArgs e)
        {
            ChartDetails chartDetails = new ChartDetails(currentPatient);
            chartDetails.ChangeTextEvent += new ChangeTextHandler(initializeSelectedPatientDetails);
            chartDetails.ShowDialog();
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
