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

        public PatientInitialWindow()
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();

            ObservableCollection<Patient> patients = PatientsStorage.getInstance().GetAll();
            cbPatient.ItemsSource = patients;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            AppointmentStorage.getInstance().serialize();
            this.Close();
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

        private void Button_Medication(object sender, RoutedEventArgs e)
        {
            if (cbPatient.SelectedItem != null)
            {
                MedicationWindow mw = new MedicationWindow((Patient)cbPatient.SelectedItem);
                mw.Show();
            }
        }

        private void Button_Feedback(object sender, RoutedEventArgs e)
        {
            if (cbPatient.SelectedItem != null && dgExamination.SelectedItem != null)
            {

                FeedbackForm rateExamination = new FeedbackForm((Examination)dgExamination.SelectedItem, ((Patient)cbPatient.SelectedItem).Id);
                rateExamination.Show();
            }
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
                if (!newApptsMade.ContainsKey(((Patient)cbPatient.SelectedItem).Id))
                    newApptsMade.Add(((Patient)cbPatient.SelectedItem).Id, 0);


                allAppointments.Filter = (e) =>
                {
                    Appointment temp = e as Appointment;
                    if (temp.Patient == cbPatient.SelectedItem)
                        return true;
                    return false;
                };
                dgAppointment.ItemsSource = allAppointments;

                allExaminations.Filter = (e) =>
                {
                    Examination temp = e as Examination;
                    if (temp.Patient == cbPatient.SelectedItem)
                        return true;
                    return false;
                };
                dgExamination.ItemsSource = allExaminations;

                if (allExaminations.Count > 0 && allExaminations.Count % 3 == 0)
                    provideGeneralFeedback(((Patient)cbPatient.SelectedItem).Id);
            }
        }
    }
}
