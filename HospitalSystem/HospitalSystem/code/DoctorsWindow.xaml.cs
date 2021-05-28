using System;
using System.Collections.Generic;
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
    /// Interaction logic for DoctorsWindow.xaml
    /// </summary>
    public partial class DoctorsWindow : Window
    {
        public DoctorsWindow()
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();

            dgDoctors.ItemsSource = DoctorStorage.getInstance().GetAll();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DoctorStorage.getInstance().serialize();
            this.Close();
        }

        private void txbBack_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SecretarInitialWindow mainWindow = new SecretarInitialWindow();
            mainWindow.Show();
            DoctorStorage.getInstance().serialize();
            this.Close();
        }

        private void txbAdd_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewDoctor newDoctor = new NewDoctor();
            newDoctor.Show();
        }

        private void txbEdit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            EditDoctor editDoctor = new EditDoctor((Doctor)dgDoctors.SelectedItem);
            editDoctor.Show();
            (dgDoctors.ItemContainerGenerator.ContainerFromItem(dgDoctors.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan pacijent
        }

        private void txbDelete_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DoctorStorage.getInstance().Delete((Doctor)dgDoctors.SelectedItem);
        }

    }
}
