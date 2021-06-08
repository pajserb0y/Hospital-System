using HospitalSystem.code.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HospitalSystem.code
{
    /// <summary>
    /// Interaction logic for DoctorsWindow.xaml
    /// </summary>
    public partial class DoctorsWindow : Window
    {
        public DoctorsWindow()
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();

            dataGridDoctors.ItemsSource = DoctorStorage.getInstance().GetAll();
            buttonHelp.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/help.jpg"))));
            imageArrow.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/arrow.jpg")));
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            DoctorStorage.getInstance().serialize();
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
                        DoctorStorage.getInstance().serialize();
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

        private void txbAdd_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewDoctor newDoctor = new NewDoctor();
            newDoctor.ShowDialog();
        }

        private void txbEdit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dataGridDoctors.SelectedItem != null)
            {
                EditDoctor editDoctor = new EditDoctor((Doctor)dataGridDoctors.SelectedItem);
                editDoctor.ShowDialog();
                (dataGridDoctors.ItemContainerGenerator.ContainerFromItem(dataGridDoctors.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan pacijent
            }
            else
                MessageBox.Show("You have to select doctor first.");            
        }

        private void txbDelete_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dataGridDoctors.SelectedItem != null)
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure that you want to delete this doctor?", "Permanently deleting", btnMessageBox, icnMessageBox);
                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        {
                            DoctorStorage.getInstance().Delete((Doctor)dataGridDoctors.SelectedItem);
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
                MessageBox.Show("You have to select doctor first.");            
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
            filterDoctors(search.Text.ToLower());
        }

        private void filterDoctors(string search)
        {
            ListCollectionView doctorCollection = new ListCollectionView(DoctorStorage.getInstance().GetAll());
            doctorCollection.Filter = (doctor) =>
            {
                Doctor tempDoctor = doctor as Doctor;
                if (tempDoctor.Adress.ToLower().Contains(search) || tempDoctor.Specialization.ToLower().Contains(search) || tempDoctor.FirstName.ToLower().Contains(search) || 
                    tempDoctor.Jmbg.ToString().ToLower().Contains(search) || tempDoctor.LastName.ToLower().Contains(search) || tempDoctor.Telephone.ToString().ToLower().Contains(search))
                    return true;
                return false;
            };
            dataGridDoctors.ItemsSource = doctorCollection;
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



        private void txbDemo_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show("For demo ending you can press any keyboard key at any time! Do you want to continue and watch demo?", "Demo rules", btnMessageBox, icnMessageBox);
            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    {
                        Demo demo = new Demo();
                        demo.ShowDialog();
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
    }
}
