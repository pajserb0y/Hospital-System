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
            buttonHelp.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/help.jpg"))));

            dataGridPatients.ItemsSource = PatientsStorage.getInstance().GetAll();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            PatientsStorage.getInstance().serialize();
            this.Close();
        }

        private void txbBack_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure that you want to log out from your account?", "Loging out", btnMessageBox, icnMessageBox);
            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        PatientsStorage.getInstance().serialize();
                        this.Close();
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
            if (dataGridPatients.SelectedItem != null)
            {
                EditPatient editPatient = new EditPatient((Patient)dataGridPatients.SelectedItem);
                editPatient.Show();
                (dataGridPatients.ItemContainerGenerator.ContainerFromItem(dataGridPatients.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan pacijent
            }
            else
                MessageBox.Show("You have to select patient first.");
        }

        private void txbDelete_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dataGridPatients.SelectedItem != null)
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure that you want to delete this patient?", "Permanently deleting", btnMessageBox, icnMessageBox);
                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        {
                            PatientsStorage.getInstance().Delete((Patient)dataGridPatients.SelectedItem);
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
                MessageBox.Show("You have to select patient first.");
        }

        private void txbAnnouncement_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AnnouncementWindow announcementWindow = new AnnouncementWindow();
            announcementWindow.Show();
        }

        public void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox search = (TextBox)sender;
            //filterPatients(search);
            search.Text = string.Empty;
            search.GotFocus -= txtSearch_GotFocus;            
        }

        private void searchChanged(object sender, TextChangedEventArgs e)
        {
            TextBox search = (TextBox)sender;
            filterPatients(search.Text.ToLower());
        }

        private void filterPatients(string search)
        {
            ListCollectionView patientCollection = new ListCollectionView(PatientsStorage.getInstance().GetAll());
            patientCollection.Filter = (patient) =>
            {
                Patient tempPatient = patient as Patient;
                if (tempPatient.Adress.ToLower().Contains(search) || tempPatient.City.ToLower().Contains(search) || tempPatient.Country.ToLower().Contains(search) || 
                    tempPatient.Email.ToLower().Contains(search) || tempPatient.FirstName.ToLower().Contains(search) || tempPatient.Jmbg.ToString().ToLower().Contains(search) ||
                    tempPatient.LastName.ToLower().Contains(search) || tempPatient.SocNumber.ToString().ToLower().Contains(search) ||
                    tempPatient.Telephone.ToString().ToLower().Contains(search) || tempPatient.Username.Contains(search))                    
                        return true;
                return false;
            };
            dataGridPatients.ItemsSource = patientCollection;
        }

        public void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            //TextBox tb = (TextBox)sender;
            //tb.Text = "Search...";
        }


        private void Patients_Click(object sender, RoutedEventArgs e)
        {
            //MainMI.Header = "< Patients >";
            //SecretarInitialWindow secretarInitialWindow = new SecretarInitialWindow();
            //secretarInitialWindow.Show();
            //this.Close();
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
            AppointmentsWindow appointmentsWindow = new AppointmentsWindow();
            appointmentsWindow.Show();
            this.Close();
        }     
        private void FrontPage_Click(object sender, RoutedEventArgs e)
        {
            MainMI.Header = "< Front page >";
        }

        private void buttonHelp_Click(object sender, RoutedEventArgs e)
        {
            if (txtHelp.Visibility == Visibility.Visible)
                txtHelp.Visibility = Visibility.Collapsed;
            else
                txtHelp.Visibility = Visibility.Visible;
        }
    }
}
