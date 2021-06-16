using HospitalSystem.code.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HospitalSystem.code
{
    /// <summary>
    /// Interaction logic for AppointmentsWindow.xaml
    /// </summary>
    public partial class AppointmentsWindow : Window, ISelectionChecker, IDeleteChecker
    {
        List<Appointment> appointmentCollectionToday = new List<Appointment>();
        List<Appointment> appointmentCollectionWeekly = new List<Appointment>();
        public AppointmentsWindow()
        {
            InitializeComponent();
            dgAppAll.ItemsSource = AppointmentStorage.getInstance().GetAll();
            appointmentCollectionToday = filterTodayAppointments();
            appointmentCollectionWeekly = filterWeeklyAppointments();
            dgAppToday.ItemsSource = appointmentCollectionToday;
            dgAppWeekly.ItemsSource = appointmentCollectionWeekly;
            buttonHelp.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/help.jpg"))));
        }

        private List<Appointment> filterWeeklyAppointments()
        {
            List<Appointment> appointmentCollection = new List<Appointment>();
            foreach (Appointment app in AppointmentStorage.getInstance().GetAll())
                if (app.Date.AddDays(-1 * ((int)(app.Date.DayOfWeek + 6) % 7)) == DateTime.Now.Date.AddDays(-1 * ((int)(DateTime.Today.DayOfWeek + 6) % 7)))
                    appointmentCollection.Add(app);
            return appointmentCollection;
        }

        private List<Appointment> filterTodayAppointments()
        {
            List<Appointment> appointmentCollection = new List<Appointment>();
            foreach (Appointment app in AppointmentStorage.getInstance().GetAll())
                if (app.Date == DateTime.Now.Date)
                    appointmentCollection.Add(app);
            return appointmentCollection;
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
                        AppointmentStorage.getInstance().serialize();
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
        private void txbAddApp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SecretarNewAppointment secretarNewAppointment = new SecretarNewAppointment();
            secretarNewAppointment.ShowDialog();
        }
        private void txbEditApp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            if (tabToday.IsSelected)
            {
                if (ISelectionChecker.isSelected(dgAppToday.SelectedItem))
                {
                    var selectedApp = (Appointment)dgAppToday.SelectedItem;
                    if (selectedApp.Date >= DateTime.Now.Date)
                    {
                        SecretarEditAppointment secretarEditAppointment = new SecretarEditAppointment((Appointment)selectedApp);
                        secretarEditAppointment.ShowDialog();
                        (dgAppToday.ItemContainerGenerator.ContainerFromItem(dgAppToday.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan app      
                    }
                    else
                        MessageBox.Show("Changing past is not allowed!");
                }
            }
            else if (tabWeekly.IsSelected)
            {
                if (ISelectionChecker.isSelected(dgAppWeekly.SelectedItem))
                {
                    var selectedApp = (Appointment)dgAppWeekly.SelectedItem;
                    if (selectedApp.Date >= DateTime.Now.Date)
                    {
                        SecretarEditAppointment secretarEditAppointment = new SecretarEditAppointment((Appointment)selectedApp);
                        secretarEditAppointment.ShowDialog();
                        (dgAppWeekly.ItemContainerGenerator.ContainerFromItem(dgAppWeekly.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan app      
                    }
                    else
                        MessageBox.Show("Changing past is not allowed!");
                }
            }
            else
            {
                if (ISelectionChecker.isSelected(dgAppAll.SelectedItem))
                {
                    var selectedApp = (Appointment)dgAppAll.SelectedItem;
                    if (selectedApp.Date >= DateTime.Now.Date)
                    {
                        SecretarEditAppointment secretarEditAppointment = new SecretarEditAppointment((Appointment)selectedApp);
                        secretarEditAppointment.ShowDialog();
                        (dgAppAll.ItemContainerGenerator.ContainerFromItem(dgAppAll.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan app      
                    }
                    else
                        MessageBox.Show("Changing past is not allowed!");
                }
            }
        }

        private void txbDeleteApp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (tabToday.IsSelected)
            {
                if (ISelectionChecker.isSelected(dgAppToday.SelectedItem))
                {
                    IDeleteChecker foo = new AppointmentsWindow();
                    foo.surelyDeleting(dgAppToday.SelectedItem);
                }
            }
            else if (tabWeekly.IsSelected)
            {
                if (ISelectionChecker.isSelected(dgAppWeekly.SelectedItem))
                {
                    IDeleteChecker foo = new AppointmentsWindow();
                    foo.surelyDeleting(dgAppWeekly.SelectedItem);
                }
            }
            else
            {                
                if (ISelectionChecker.isSelected(dgAppAll.SelectedItem))
                {
                    IDeleteChecker foo = new AppointmentsWindow();
                    foo.surelyDeleting(dgAppAll.SelectedItem);
                }
            }
        }
        void IDeleteChecker.deleteObject(object selectedItem)
        {
            AppointmentStorage.getInstance().Delete((Appointment)selectedItem);
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
            filterAppointments(search.Text.ToLower());
        }
        private void filterAppointments(string search)
        {
            checkAllApp(search);
            checkWeekApp(search);
            checkTodayApp(search);
        }

        private void checkTodayApp(string search)
        {
            ListCollectionView appointmentCollection = new ListCollectionView(appointmentCollectionToday);
            appointmentCollection.Filter = (app) =>
            {
                Appointment tempApp = app as Appointment;
                if (tempApp.Doctor.FirstName.ToLower().Contains(search) || tempApp.Doctor.LastName.ToLower().Contains(search) || tempApp.Doctor.Specialization.ToLower().Contains(search) ||
                    tempApp.Patient.FirstName.ToLower().Contains(search) || tempApp.Patient.LastName.ToLower().Contains(search) || tempApp.Date.ToString("dd/MM/yyyy").ToLower().Contains(search) ||
                    tempApp.Room.Name.ToLower().Contains(search) || tempApp.Time.ToString().ToLower().Contains(search))
                    return true;
                return false;
            };
            dgAppToday.ItemsSource = appointmentCollection;
        }

        private void checkWeekApp(string search)
        {
            ListCollectionView appointmentCollection = new ListCollectionView(appointmentCollectionWeekly);
            appointmentCollection.Filter = (app) =>
            {
                Appointment tempApp = app as Appointment;
                if (tempApp.Doctor.FirstName.ToLower().Contains(search) || tempApp.Doctor.LastName.ToLower().Contains(search) || tempApp.Doctor.Specialization.ToLower().Contains(search) ||
                    tempApp.Patient.FirstName.ToLower().Contains(search) || tempApp.Patient.LastName.ToLower().Contains(search) || tempApp.Date.ToString("dd/MM/yyyy").ToLower().Contains(search) ||
                    tempApp.Room.Name.ToLower().Contains(search) || tempApp.Time.ToString().ToLower().Contains(search))
                    return true;
                return false;
            };
            dgAppWeekly.ItemsSource = appointmentCollection;
        }

        private void checkAllApp(string search)
        {
            ListCollectionView appointmentCollection = new ListCollectionView(AppointmentStorage.getInstance().GetAll());
            appointmentCollection.Filter = (app) =>
            {
                Appointment tempApp = app as Appointment;
                if (tempApp.Doctor.FirstName.ToLower().Contains(search) || tempApp.Doctor.LastName.ToLower().Contains(search) || tempApp.Doctor.Specialization.ToLower().Contains(search) ||
                    tempApp.Patient.FirstName.ToLower().Contains(search) || tempApp.Patient.LastName.ToLower().Contains(search) || tempApp.Date.ToString("dd/MM/yyyy").ToLower().Contains(search) ||
                    tempApp.Room.Name.ToLower().Contains(search) || tempApp.Time.ToString().ToLower().Contains(search))
                    return true;
                return false;
            };
            dgAppAll.ItemsSource = appointmentCollection;
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
            //MainMI.Header = "< Appointments >";
            //AppointmentsWindow appointmentsWindow = new AppointmentsWindow();
            //appointmentsWindow.Show();
            //this.Close();
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

        private void txbExportToPdf_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Appointment app = filterWeeklyAppointments()[0];
            FileStream fs = new FileStream("../../../Reports/Weekly report of appointments.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            Rectangle rec = new Rectangle(800, 1024);
            Document pdfDocument = new Document(rec);
            PdfWriter writer = PdfWriter.GetInstance(pdfDocument, fs);

            pdfDocument.Open();

            Paragraph title = new Paragraph(string.Format("All appointments and operations for {0} week", app.Date.AddDays(-1 * (int)(app.Date.DayOfWeek) + 1).Day.ToString() + "/" +
                                                                                                          app.Date.AddDays(-1 * (int)(app.Date.DayOfWeek) + 1).Month.ToString() + "/" +
                                                                                                          app.Date.AddDays(-1 * (int)(app.Date.DayOfWeek) + 1).Year.ToString()));
            title.Alignment = Element.ALIGN_CENTER;
            title.Font.Size = 22;
            pdfDocument.Add(title);

            pdfDocument.Add(Chunk.NEWLINE);

            PdfPTable table = new PdfPTable(7);
            float[] widths = new float[] { 0.5f, 2f, 2f, 1f, 1f, 1f, 0.7f };
            table.SetWidths(widths);
            table.DefaultCell.FixedHeight = 20f; //visina reda
            table.WidthPercentage = 100;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            for (int i = 0; i <= 6; i++)
                table.AddCell(dgAppWeekly.ColumnFromDisplayIndex(i).Header.ToString());

            foreach (Appointment appointment in appointmentCollectionWeekly.OrderBy(o => o.Date).ThenBy(o => o.Time.TimeOfDay).ToList())
            {
                table.AddCell(appointment.Id.ToString());
                table.AddCell(appointment.Patient.Id.ToString() + "  " + appointment.Patient.FirstName.ToString() + " " + appointment.Patient.LastName.ToString());
                table.AddCell(appointment.Doctor.FirstName.ToString() + " " + appointment.Doctor.LastName.ToString() + " - " + appointment.Doctor.Specialization.ToString());
                table.AddCell(appointment.Room.Id.ToString() + "  " + appointment.Room.Name.ToString());
                table.AddCell(appointment.Date.Day.ToString() + "/" + appointment.Date.Month.ToString() + "/" + appointment.Date.Year.ToString());
                table.AddCell(appointment.Time.TimeOfDay.Hours.ToString("D2") + ":" + appointment.Time.TimeOfDay.Minutes.ToString("D2"));

                if (appointment.IsOperation == true)
                    table.AddCell("YES");
                else
                    table.AddCell("NO");
            }
            pdfDocument.Add(table);

            pdfDocument.Close();

            MessageBox.Show("Report has been succesfuly saved in Reports by name 'Weekly report of appointments'!");

            var process = new Process();
            process.StartInfo = new ProcessStartInfo(Path.GetFullPath("../../../Reports/Weekly report of appointments.pdf"))       // da se pdf fajl odmah otvori
            {
                UseShellExecute = true
            };
            process.Start();
        }
    }
}
