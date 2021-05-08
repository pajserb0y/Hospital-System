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
    /// Interaction logic for DoctorInitialWindow.xaml
    /// </summary>
    public partial class DoctorInitialWindow : Window
    {
        ListCollectionView collectionViewExamination = new ListCollectionView(ExaminationStorage.getInstance().GetAll());

        public DoctorInitialWindow()
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();

            InitializeCollection();

            FillDrugList();

            tExam.Visibility = Visibility.Collapsed;
            tPersc.Visibility = Visibility.Collapsed;

        }

        private void FillDrugList()
        {
            Drug d1 = new Drug(1, "Bensedin");
            Drug d2 = new Drug(2, "Bromazepam");
            Drug d3 = new Drug(3, "Trodon");

            cbDrug.Items.Add(d1);
            cbDrug.Items.Add(d2);
            cbDrug.Items.Add(d3);
        }

        private void InitializeCollection()
        {
            ObservableCollection<Appointment> appointments = AppointmentStorage.getInstance().GetAll();
            ObservableCollection<Doctor> doctors = DoctorStorage.getInstance().GetAll();
            ObservableCollection<Patient> patients = PatientsStorage.getInstance().GetAll();

            cbDoctor.ItemsSource = doctors;
            cbPatient.ItemsSource = patients;
            dgDoctorExams.ItemsSource = appointments;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ExaminationStorage.getInstance().serialize();
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
            ExaminationStorage.getInstance().Delete((Examination)selectedItem);
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Examination> ExamList = ExaminationStorage.getInstance().GetAll();
            var selectedItem = dgDoctorExams.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            EditExam editExamWindow = new EditExam((Examination)selectedItem);
            editExamWindow.Show();
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            ExaminationStorage.getInstance().serialize();
            mainWindow.Show();
            this.Close();
        }

        private void doctorChanged(object sender, System.EventArgs e)
        {
            if (cbDoctor.SelectedItem == null) //PREIMENOVATI cb u comboBox
            {
                return;
            }
            collectionViewExamination.Filter = (exam) =>
            {
                Examination tempExam = exam as Examination;
                if (tempExam.Doctor == cbDoctor.SelectedItem)
                    return true;
                return false;
            };
            dgDoctorExams.ItemsSource = collectionViewExamination;
        }

        private void patientChanged(object sender, SelectionChangedEventArgs e)
        {
            PatientDetails patientDetails = new PatientDetails((Patient)cbPatient.SelectedItem);
            patientDetails.Show();
        }

        private void Button_Save_Anamnesis(object sender, RoutedEventArgs e)
        {
            Examination currExam = (Examination)dgDoctorExams.SelectedItem;
            Anamnesis newAnamnesis = new Anamnesis(currExam.Id, txtAnamnesis.Text,txtDiagnosis.Text);
            AnamnesisStorage.getInstance().Add(newAnamnesis);
            AnamnesisStorage.getInstance().serialize();
            ExaminationStorage.getInstance().Add(currExam);
            ExaminationStorage.getInstance().serialize();
            Appointment currApp = (Appointment)dgDoctorExams.SelectedItem;
            AppointmentStorage.getInstance().Delete(currApp);
            AppointmentStorage.getInstance().serialize();
            t0.Focus();
            tExam.Visibility = Visibility.Collapsed;
            //dgDoctorExams.Items.Remove(dgDoctorExams.SelectedItem);
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
            int prescID = PrescriptionStorage.getInstance().GenerateNewID();
            int patientId = currExam.Patient.Id;
            Prescription newPrescription = new Prescription(prescID, patientId,  currExam.Id, (Drug)cbDrug.SelectedItem, txtTaking.Text, currExam.Date);
            PrescriptionStorage.getInstance().Add(newPrescription);
            PrescriptionStorage.getInstance().serialize();
            t0.Focus();
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
    }
}
