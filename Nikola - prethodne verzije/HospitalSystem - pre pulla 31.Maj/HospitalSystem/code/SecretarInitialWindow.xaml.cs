using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    /// Interaction logic for SecretarInitialWindow.xaml
    /// </summary>
    public partial class SecretarInitialWindow : Window
    {
        public SecretarInitialWindow()
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();
            
            dg.ItemsSource = PatientsStorage.getInstance().GetAll();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            PatientsStorage.getInstance().serialize();
            this.Close();
        }

        private void txbBack_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            PatientsStorage.getInstance().serialize();
            this.Close();
        }
        private void txbUrgentPatient_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            UrgentPatient urgentPatient = new UrgentPatient();
            urgentPatient.Show();
        }

        private void txbAdd_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewPatient newPatient = new NewPatient();
            newPatient.Show();
        }

        private void txbEdit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            EditPatient editPatient = new EditPatient((Patient)dg.SelectedItem);
            editPatient.Show();
            (dg.ItemContainerGenerator.ContainerFromItem(dg.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan pacijent
        }

        private void txbDelete_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PatientsStorage.getInstance().Delete((Patient)dg.SelectedItem);
        }

        private void txbAnnouncement_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AnnouncementWindow announcementWindow = new AnnouncementWindow();
            announcementWindow.Show();
        }      


        private void Patients_Click(object sender, RoutedEventArgs e)
        {
            MainMI.Header = "< Patients >";
            SecretarInitialWindow secretarInitialWindow = new SecretarInitialWindow();
            secretarInitialWindow.Show();
            this.Close();
        }

        private void Doctors_Click(object sender, RoutedEventArgs e)
        {
            MainMI.Header = "< Doctors >";
            DoctorsWindow doctorsWindow = new DoctorsWindow();
            doctorsWindow.Show();
            this.Close();
        }
        private void Appointments_Click(object sender, RoutedEventArgs e)
        {
            MainMI.Header = "< Appointments >";
        }
        private void Operations_Click(object sender, RoutedEventArgs e)
        {
            MainMI.Header = "< Operations >";
        }        
        private void FrontPage_Click(object sender, RoutedEventArgs e)
        {
            MainMI.Header = "< Front page >";
        }
    }
}
