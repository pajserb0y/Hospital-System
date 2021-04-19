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
        ListCollectionView collectionView = new ListCollectionView(AppointmentStorage.getInstance().GetAll());

        public PatientInitialWindow()
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();

            ObservableCollection<Appointment> appts = AppointmentStorage.getInstance().GetAll();
            dgAppointment.ItemsSource = appts;

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
            if (cbPatient.SelectedItem != null)
            {
                NewAppointment newAppt = new NewAppointment((Patient)cbPatient.SelectedItem);
                newAppt.Show();
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (dgAppointment.SelectedItem != null)
            {
                AppointmentStorage.getInstance().Delete((Appointment)dgAppointment.SelectedItem);            
            }
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

        private void patientChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPatient.SelectedItem != null)
            {
                collectionView.Filter = (e) =>
                {
                    Appointment temp = e as Appointment;
                    if (temp.Patient == cbPatient.SelectedItem)
                        return true;
                    return false;
                };
                dgAppointment.ItemsSource = collectionView;
            }
        }
    }
}
