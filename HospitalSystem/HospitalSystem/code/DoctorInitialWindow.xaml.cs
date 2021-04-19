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
   
            ObservableCollection<Examination> exams = ExaminationStorage.getInstance().GetAll();
            ObservableCollection<Doctor> doctors = DoctorStorage.getInstance().GetAll();
            ObservableCollection<Patient> patients = PatientsStorage.getInstance().GetAll();
            cbDoctor.ItemsSource = doctors;
           
            cbPatient.ItemsSource = patients;

            dgDoctorExams.ItemsSource = exams;

            Drug d1 = new Drug(1, "Bensedin");
            Drug d2 = new Drug(2, "Bromazepam");
            Drug d3 = new Drug(3, "Trodon");

            cbDrug.Items.Add(d1);
            cbDrug.Items.Add(d2);
            cbDrug.Items.Add(d3);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ExaminationStorage.getInstance().serialize();
            this.Close();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            NewExam ne = new NewExam();
           
            ne.Show();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgDoctorExams.SelectedItem;
            if (selectedItem != null)
            {
                ExaminationStorage.getInstance().Delete((Examination)selectedItem);
                //DataGridXAML.Items.Remove(selectedItem);
            }
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Examination> ExamList = ExaminationStorage.getInstance().GetAll();
            var selectedItem = dgDoctorExams.SelectedItem;
            if (selectedItem != null)
            {
                EditExam ee = new EditExam((Examination)selectedItem);
                ee.Show();
                //DataGridXAML.Items.Remove(selectedItem);
            }
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            ExaminationStorage.getInstance().serialize();
            mw.Show();
            this.Close();
        }

        //private void doctorChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}
        private void doctorChanged(object sender, System.EventArgs e)
        {
            if (cbDoctor.SelectedItem != null)
            {
                collectionViewExamination.Filter = (e) =>
                {
                    Examination temp = e as Examination;
                    if (temp.Doctor == cbDoctor.SelectedItem)
                        return true;
                    return false;
                };
                dgDoctorExams.ItemsSource = collectionViewExamination;
            }
        }

        private void patientChanged(object sender, SelectionChangedEventArgs e)
        {
            PatientDetails patientDetails = new PatientDetails((Patient)cbPatient.SelectedItem);
            patientDetails.Show();
        }

        private void Button_View(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Save_Prescription(object sender, RoutedEventArgs e)
        {
            Examination currExam = (Examination)dgDoctorExams.SelectedItem;
            int prescID = PrescriptionStorage.getInstance().GenerateNewID();
            Patient p = (Patient)cbPatient.SelectedItem;
            Prescription newPrescription = new Prescription(prescID, p.Id,  currExam.Id, (Drug)cbDrug.SelectedItem, txtTaking.Text, currExam.Date);
            PrescriptionStorage.getInstance().Add(newPrescription);
            PrescriptionStorage.getInstance().serialize();
        }
    }
}
