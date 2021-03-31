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
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NewPatient np = new NewPatient();
            np.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            PatientsStorage.getInstance().serialize();
            this.Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditPatient ep = new EditPatient((Patient)dg.SelectedItem);
            ep.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            PatientsStorage.getInstance().Delete((Patient)dg.SelectedItem);
        }

        private void txb_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mp = new MainWindow();
            mp.Show();
            PatientsStorage.getInstance().serialize();
            this.Close();
        }
    }
}
