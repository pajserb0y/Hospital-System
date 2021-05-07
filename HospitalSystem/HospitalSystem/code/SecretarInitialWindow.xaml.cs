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

            //this.DataContext = PatientsStorage.getInstance(); // DODAO
            dg.ItemsSource = PatientsStorage.getInstance().GetAll();
            // dg.Columns[0].Visibility = Visibility.Collapsed;
            // dg.ColumnWidth = dg.ColumnWidth * 1.5;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            PatientsStorage.getInstance().serialize();
            this.Close();
        }

        private void txbBack_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mp = new MainWindow();
            mp.Show();
            PatientsStorage.getInstance().serialize();
            this.Close();
        }
        private void txbUrgentPatient_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            UrgentPatient up = new UrgentPatient();
            up.Show();
        }

        private void txbAdd_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewPatient np = new NewPatient();
            np.Show();
        }

        private void txbEdit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            EditPatient ep = new EditPatient((Patient)dg.SelectedItem);
            ep.Show();
            (dg.ItemContainerGenerator.ContainerFromItem(dg.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan pacijent
            //(dg.ItemContainerGenerator.ContainerFromItem(dg.SelectedItem) as DataGridCell).IsSelected = false;
        }

        private void txbDelete_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PatientsStorage.getInstance().Delete((Patient)dg.SelectedItem);
        }

        private void txbAnnouncement_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AnnouncementWindow aw = new AnnouncementWindow();
            aw.Show();
        }
    }
}
