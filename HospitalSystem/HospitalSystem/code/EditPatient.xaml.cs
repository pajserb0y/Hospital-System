using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            setButtonImages();
            initializeSelectedPatientDetails(selectedPatient);
            fillAppointments();
            fillExaminations();
            hideExaminationDetails();
            fillAnnouncements();
        }

        private void setButtonImages()
        {
            buttonAddJob.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/add.jpg"))));
            buttonEditJob.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/edit.jpg"))));
            buttonDeleteJob.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/delete.jpg"))));
            buttonAddAlergen.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/add.jpg"))));
            buttonDeleteAlergen.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/delete.jpg"))));
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
            tabRefferal.Visibility = Visibility.Collapsed;            
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

            if (selectedPatient.Alergens == null)
                selectedPatient.Alergens = new ObservableCollection<string>();
            listViewAlergens.ItemsSource = selectedPatient.Alergens;

            if (selectedPatient.WorkHistory == default)
                selectedPatient.WorkHistory = new ObservableCollection<Job>();
            dgJob.ItemsSource = selectedPatient.WorkHistory;
        }

        #region Chart + Account + Inbox
        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtIme.Text == "" || txtPrezime.Text == "" || txtJmbg.Text == "" || txtTel.Text == "")
                MessageBox.Show("First name, Last name, JMBG and Telephone fields are required.");
            else
                try
                {
                    if (Convert.ToInt64(txtJmbg.Text) < 100000000000 || Convert.ToInt64(txtJmbg.Text) > 9999999999999 || txtJmbg.Text.Length != 13)
                        errorJmbg.Content = "JMBG must contain 13 digits";
                    else
                    {
                        PatientsStorage.getInstance().Edit(new Patient(currentPatient.Id, txtIme.Text, txtPrezime.Text, Convert.ToInt64(txtJmbg.Text),
                (char)((bool)rbF.IsChecked ? Convert.ToChar(rbF.Content) : Convert.ToChar(rbM.Content)), txtAdress.Text, Convert.ToInt64(txtTel.Text), txtEmail.Text, cbGuest.IsChecked == true,
                txtUsername.Text, txtPassword.Text, (DateTime)dpBirth.SelectedDate, cbMarriage.SelectedIndex == -1 ? "" : cbMarriage.SelectedValue.ToString(), Convert.ToInt64(txtSoc.Text),
                txtCity.Text, txtCountry.Text, currentPatient.Alergens, currentPatient.WorkHistory));
                        PatientsStorage.getInstance().serialize();
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("JMBG and Telephone field must contain only digits!");
                }
        }
        private void txbAddJob_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            NewJob newJob = new NewJob(currentPatient);
            newJob.Show();
        }
        private void txbEditJob_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            if (dgJob.SelectedItem != null)
            {
                NewJob editJob = new NewJob(currentPatient, (Job)dgJob.SelectedItem);
                editJob.Show();
                (dgJob.ItemContainerGenerator.ContainerFromItem(dgJob.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan job
            }
            else
                MessageBox.Show("Before editing some job you need to select some firsst.");
        }
        private void txbDeleteJob_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            if (dgJob.SelectedItem != null)
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure that you want to delete this job?", "Permanently deleting", btnMessageBox, icnMessageBox);
                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        {
                            currentPatient.WorkHistory.Remove((Job)dgJob.SelectedItem);
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
                MessageBox.Show("You have to select job first.");
        }
        private void ButtonAddAlergen_Click(object sender, RoutedEventArgs e)
        {
            if (txtSubstance.Text != "")
                currentPatient.Alergens.Add(txtSubstance.Text);
        }

        private void ButtonDeleteAlergen_Click(object sender, RoutedEventArgs e)
        {
            if (dgJob.SelectedItems != null)
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure that you want to delete this alergens?", "Permanently deleting", btnMessageBox, icnMessageBox);
                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        {
                            List<string> tempList = new List<string>();
                            foreach (string selectedAlergen in listViewAlergens.SelectedItems)
                                tempList.Add(selectedAlergen);

                            foreach (string alergen in tempList)
                                currentPatient.Alergens.Remove(alergen);
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
                MessageBox.Show("You have to select alergens first.");            
        }

        private void txbRead_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (announcementList.SelectedItem != null)
            {
                AnnouncementWindow announcementWindow = new AnnouncementWindow((Announcement)announcementList.SelectedItem);
                announcementWindow.Show();
            }
            else
                MessageBox.Show("You have to select announcement first.");
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
            if (dgApp.SelectedItem != null)
            {
                var selectedApp = dgApp.SelectedItem;
                if (selectedApp != null)
                {
                    SecretarEditAppointment secretarEditAppointment = new SecretarEditAppointment((Appointment)selectedApp);
                    secretarEditAppointment.Show();
                    (dgApp.ItemContainerGenerator.ContainerFromItem(dgApp.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan app      
                }
            }
            else
                MessageBox.Show("You have to select appointment first.");
        }
        private void txbDeleteApp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dgApp.SelectedItem != null)
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure that you want to delete this appointment?", "Permanently deleting", btnMessageBox, icnMessageBox);
                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        {
                            AppointmentStorage.getInstance().Delete((Appointment)dgApp.SelectedItem);
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
                MessageBox.Show("You have to select appointment first.");            
        }
        #endregion


        #region Medical history
        private void txbView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dgExam.SelectedItem != null)
            {
                showExaminationDetails();

                Examination selectedExam = (Examination)dgExam.SelectedItem;
                Anamnesis anamnesis = selectedExam.Anamnesis;
                Prescription prescription = selectedExam.Prescriptions[0];
                //List<Refferal> refferals = 

                txtAnamnesis.Clear();
                txtDiagnosis.Clear();
                if (anamnesis != null)
                {
                    txtAnamnesis.Text = anamnesis.AnamnesisInfo;
                    txtDiagnosis.Text = anamnesis.Diagnosis;
                }

                cbDrug.SelectedIndex = -1;
                txtTaking.Clear();
                txtDate.Clear();
                if (prescription != null)
                {
                    cbDrug.SelectedItem = prescription.Drug;
                    txtTaking.Text = prescription.Taking;
                    txtDate.Text = prescription.DateOfPrescription.ToString();
                }
            }
            else
                MessageBox.Show("You have to select examination first.");
        }

        private void showExaminationDetails()
        {
            tabAnamnesis.Visibility = Visibility.Visible;
            tabPrescription.Visibility = Visibility.Visible;
            tabRefferal.Visibility = Visibility.Visible;
        }
        #endregion


        private void Window_Closed(object sender, EventArgs e)
        {
            //JobStorage.getInstance().serialize();
            this.Close();
        }
    }
}
