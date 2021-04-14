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
        public PatientInitialWindow()
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();

            ObservableCollection<Appointment> appts = AppointmentStorage.getInstance().GetAll();
            DataGridXAML.ItemsSource = appts;
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            AppointmentStorage.getInstance().serialize();
            this.Close();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            NewAppointment newAppt = new NewAppointment();
            newAppt.Show();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var selectedItem = DataGridXAML.SelectedItem;
            if (selectedItem != null)
            {
                AppointmentStorage.getInstance().Delete((Appointment)selectedItem);            
            }
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            
            var selectedItem = DataGridXAML.SelectedItem;
            if (selectedItem != null)
            {
                EditAppointment editAppt = new EditAppointment((Appointment)selectedItem);
                editAppt.Show();             
            }
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            AppointmentStorage.getInstance().serialize();
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
